using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class AdvancedRoomGenerator : MonoBehaviour
{
    [SerializeField] private int smooth;
    [Range(0, 100)]
    [SerializeField] private int randomFillPercent;

    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap lowerWallMap;

    [SerializeField] private TileBase wall;
    [SerializeField] private TileBase lowerWall;

    [SerializeField] private GameObject doorObj;

    [SerializeField] private List<SpawnInWord> enemies;
    [SerializeField] private Transform enemiesContainer;

    [SerializeField] private List<SpawnInWord> decorations;
    [SerializeField] private Transform decorationContainer;

    public List<Door> roomDoors;
    public bool isActive;
    public bool isCleared;

    private List<DoorEnum> doors;
    [HideInInspector]
    public int[,] room;
    private System.Random prng;

    private int width;
    private int height;


    private List<Vector2Int> mainCompartiments;
    [HideInInspector]
    public MiniMapRoom roomGui;


    private void Update()
    {
        if (isActive)
        {
            if(enemiesContainer.childCount == 0)
            {
                isCleared = true;
            }
        }
    }

    public void setRoomActive(bool val)
    {
        isActive = val;

        if(!isCleared && isActive)
            spawnEnemies(mainCompartiments);
    }

    public void GenerateRoom(System.Random prng, int width, int height,List<DoorEnum> doors)
    {
        this.prng = prng;
        this.doors = doors;
        this.width = width;
        this.height = height;


        room = new int[width, height];
        roomDoors = new List<Door>();
        RandomFillMap();

        for (int i = 0; i < smooth; i++)
        {
            smoothMap();
        }
        mainCompartiments = coverHoles(indentifyCompartiments());

        createDoors();
        spawnWalls();

    }

    private void RandomFillMap()
    {
        

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    room[x, y] = 1;
                }
                else
                {
                    room[x, y] = prng.Next(0, 100) < randomFillPercent ? 1 : 0;
                }
            }
        }
    }

    private void smoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(getSurroundingWallCount(x,y) > 4)
                {
                    room[x, y] = 1;
                }
                else if(getSurroundingWallCount(x, y) < 4)
                {
                    room[x, y] = 0;
                }

            }
        }
    }

    private void spawnWalls()
    {
        map.ClearAllTiles();
        lowerWallMap.ClearAllTiles();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (room[i, j] == 1)
                {
                    map.SetTile(new Vector3Int(i, j, 0), wall);
                    if (j < height - 1)
                    {
                        if (room[i, j + 1] == 1)
                        {
                            lowerWallMap.SetTile(new Vector3Int(i, j, 0), lowerWall);
                        }
                    }
                    
                }
                else if(room[i,j] == 0)
                {
                    if (j > 0 && j < height - 1) {

                        if (room[i, j + 1] == 1)
                        {
                            lowerWallMap.SetTile(new Vector3Int(i, j, 0), lowerWall);
                        }   
                    } 
                }
            }
        }
    }

    private List<List<Vector2Int>> indentifyCompartiments()
    {
        List<List<Vector2Int>> compartiments = new List<List<Vector2Int>>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(room[x,y] == 0)
                {
                    bool isNewCompartiment = true;
                    foreach(List<Vector2Int> compartiment in compartiments){
                        if (compartiment.Contains( new Vector2Int(x, y)))
                        {
                            isNewCompartiment = false;
                        }

                    }

                    if (isNewCompartiment)
                    {
                        compartiments.Add(getCompartiment(x,y));

                    }
                }
            }
        }

        return compartiments;
    }

    private List<Vector2Int> getCompartiment(int x, int y)
    {
        

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        List<Vector2Int> compartiment = new List<Vector2Int>();

        queue.Enqueue(new Vector2Int(x,y));
        int[,] mapFlags = new int[width, height];

        mapFlags[x, y] = 1;

        int loops = 0;
        while (queue.Count > 0)
        {

            loops++;
            if (loops > 5000)
            {
                Debug.Log("DONT WORK:(");
                break;
            }

            Vector2Int pos = queue.Dequeue();
            compartiment.Add(pos);
            for (int adjX = pos.x - 1; adjX <= pos.x + 1; adjX++)
            {
                for (int adjY = pos.y - 1; adjY <= pos.y + 1; adjY++)
                {
                    if (isInMapRange(adjX, adjY) && (adjY == pos.y || adjX == pos.x))
                    {
                        if (mapFlags[adjX, adjY] == 0 && room[adjX, adjY] == 0)
                        {
                            queue.Enqueue(new Vector2Int(adjX, adjY));
                            mapFlags[adjX, adjY] = 1;
                        }
                    }
                }
            }


        }

        return compartiment;
    }

    private List<Vector2Int> coverHoles(List<List<Vector2Int>> compartiments)
    {
        int biggestCompartiment = -1;
        int sizeOfBiggestCompartimet = 0;

        for (int i = 0; i < compartiments.Count; i++)
        {
            if (sizeOfBiggestCompartimet < compartiments[i].Count)
            {
                sizeOfBiggestCompartimet = compartiments[i].Count;
                biggestCompartiment = i;
            }
        }


        for (int i = 0; i < compartiments.Count; i++)
        { 
            if(i != biggestCompartiment)
            {
                foreach(Vector2Int pos in compartiments[i])
                {
                    room[pos.x, pos.y] = 1;
                }
            }


        }

        return compartiments[biggestCompartiment];
    }

    private void createDoors()
    {
        foreach (DoorEnum door in doors)
        {
            List<Vector2Int> makeDoor = new List<Vector2Int>();
            GameObject doorTrigger = null;
            int y = 0;
            int x = 0;
            int directionX = 0;
            int directionY = 0;
            int doorIdx = 0;
            switch (door)
            {
                case DoorEnum.Up:
                    y  = height - 1;
         
                    while( makeDoor.Count == 0)
                    {
                        
                        for (x = 0; x < width; x++)
                        {
                            if (room[x,y] == 0)
                            {
                                makeDoor.Add(new Vector2Int(x,y));
                            }
                        }
                        y--;
                    }

                    doorIdx = prng.Next(0, makeDoor.Count);

                    

                    for (int pathY = makeDoor[doorIdx].y; pathY < height; pathY++)
                    {
                        room[makeDoor[doorIdx].x, pathY] = 0;
                    }
                    doorTrigger = Instantiate(doorObj, transform.position + new Vector3(makeDoor[doorIdx].x + .5f, makeDoor[doorIdx].y + 2f + .5f, 0), transform.rotation, this.transform);
                    directionY = +1;

                    break;

                case DoorEnum.Down:
                    y = 0;

                    while (makeDoor.Count == 0)
                    {

                        for (x = 0; x < width; x++)
                        {
                            if (room[x, y] == 0)
                            {
                                makeDoor.Add(new Vector2Int(x, y));
                            }
                        }
                        y++;
                    }

                    doorIdx = prng.Next(0, makeDoor.Count);



                    for (int pathY = makeDoor[doorIdx].y; pathY >= 0; pathY--)
                    {
                        room[makeDoor[doorIdx].x, pathY] = 0;
                    }
                    doorTrigger = Instantiate(doorObj, transform.position + new Vector3(makeDoor[doorIdx].x + .5f, makeDoor[doorIdx].y-2f + .5f, 0), transform.rotation, this.transform);
                    directionY = -1;

                    break;

                case DoorEnum.Left:
                    x = 0;

                    while (makeDoor.Count == 0)
                    {

                        for (y = 0; y < height; y++)
                        {
                            if (room[x, y] == 0)
                            {
                                makeDoor.Add(new Vector2Int(x, y));
                            }
                        }
                        x++;
                    }

                    doorIdx = prng.Next(0, makeDoor.Count);



                    for (int pathX = makeDoor[doorIdx].x; pathX >= 0; pathX--)
                    {
                        room[pathX, makeDoor[doorIdx].y] = 0;
                    }
                    doorTrigger = Instantiate(doorObj, transform.position + new Vector3(makeDoor[doorIdx].x - 2f + .5f, makeDoor[doorIdx].y + .5f, 0), transform.rotation, this.transform);

                    directionX = -1;
                    break;

                case DoorEnum.Right:
                    x = width - 1;

                    while (makeDoor.Count == 0)
                    {

                        for (y = 0; y < height; y++)
                        {
                            if (room[x, y] == 0)
                            {
                                makeDoor.Add(new Vector2Int(x, y));
                            }
                        }
                        x--;
                    }
                    doorIdx = prng.Next(0, makeDoor.Count);

                    for (int pathX = makeDoor[doorIdx].x; pathX < width; pathX++)
                    {
                        room[pathX, makeDoor[doorIdx].y] = 0;
                    }
                    doorTrigger = Instantiate(doorObj, transform.position + new Vector3(makeDoor[doorIdx].x+2f + .5f, makeDoor[doorIdx].y + .5f, 0), transform.rotation, this.transform);
                    directionX = +1;
                    break;

            }

           
            Door doortrig = doorTrigger.GetComponent<Door>();
            doortrig.init(directionX, directionY);
            roomDoors.Add(doortrig);
        }



    }

    private void spawnEnemies(List<Vector2Int> compartiment)
    {
        foreach(SpawnInWord enemy in enemies)
        {
            int nrOfEnemyes = prng.Next(enemy.minInRoom, enemy.maxInRoom);

            for(int i = 0; i < nrOfEnemyes; i++)
            {
                int tileIdx = prng.Next(0, compartiment.Count);

                if (room[compartiment[tileIdx].x, compartiment[tileIdx].y + 1] == 0 && room[compartiment[tileIdx].x, compartiment[tileIdx].y] == 0)
                {
                    room[compartiment[tileIdx].x, compartiment[tileIdx].y] = 1;
                    Instantiate(enemy.Obj, transform.position + new Vector3(compartiment[tileIdx].x + 0.5f, compartiment[tileIdx].y + .5f, -2), transform.rotation, enemiesContainer);
                }
                else
                {
                    i--;
                }

                
                
            }

        }

        foreach (SpawnInWord decoration in decorations)
        {
            int nrOfEnemyes = prng.Next(decoration.minInRoom, decoration.maxInRoom);

            for (int i = 0; i < nrOfEnemyes; i++)
            {
                int tileIdx = prng.Next(0, compartiment.Count);
                if (room[compartiment[tileIdx].x, compartiment[tileIdx].y+1] == 0 && room[compartiment[tileIdx].x, compartiment[tileIdx].y] == 0)
                {
                    room[compartiment[tileIdx].x, compartiment[tileIdx].y] = 1;
                    Instantiate(decoration.Obj, transform.position + new Vector3(compartiment[tileIdx].x + 0.5f, compartiment[tileIdx].y + .5f, -2), transform.rotation, decorationContainer);
                }
                else
                {
                    i--;
                }
            }

        }
    }
    private int getSurroundingWallCount(int x, int y)
    {
        int wallCount = 0;

        for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX ++)
        {
            for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
            {
                if (isInMapRange(neighbourX,neighbourY))
                {

                    if (neighbourX != x || neighbourY != y)
                    {
                        wallCount += room[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
    private bool isInMapRange(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;

    }
    public Vector2[] getCameraBoundery()
    {
        Vector2[] boundery = new Vector2[2];

        boundery[0] = new Vector2(transform.position.x + 9.5f, transform.position.y + 5.5f);
        boundery[1] = new Vector2(transform.position.x + width - 9.5f, transform.position.y+height - 5.5f);

        return boundery;
    }
    public int[,] getGrid()
    {
        return room;
    }

    [System.Serializable]
    class SpawnInWord
    {
        public GameObject Obj;
        public int minInRoom;
        public int maxInRoom;
    } 
}

public enum DoorEnum
{
    Up,
    Down,
    Left,
    Right
}
