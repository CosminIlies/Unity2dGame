using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    #region Singleton

    public static MapManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private GameObject room;
    [SerializeField] private int width, height;
    [SerializeField] private int sizeX, sizeY;
    [SerializeField] private int mapLength;


    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed;

    [SerializeField] private GameObject minimapRoom;
    [SerializeField] private GameObject minimap;
    [SerializeField] private Animator anim;

    private System.Random prng;
    private AdvancedRoomGenerator[,] map;
    private int[,] flagMap;
    private int playerX, playerY;
    private Timer changeRoomTimer;


    private bool needToChangeRoom;
    private int needToChangeDirX, needToChangeDirY;

    void Start()
    {
        map = new AdvancedRoomGenerator[width,height];
        flagMap = new int[width, height];
        changeRoomTimer = new Timer(0.2f);
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        prng = new System.Random(seed.GetHashCode());


        generateMap();
        spawnRooms();

        activateRoom();
    }

    public void changeRoom(int directionX,int directionY)
    {
        if (!map[playerX, playerY].isCleared)
            return;


        anim.SetTrigger("changedRoom");
        needToChangeDirX = directionX;
        needToChangeDirY = directionY;
        needToChangeRoom = true;
        changeRoomTimer.reset();
    }

    public void changeRoom()
    {
        playerX += needToChangeDirX;
        playerY += needToChangeDirY;
        needToChangeRoom = false;
        if (map[playerX, playerY] != null)
        {
            foreach (Door door in map[playerX, playerY].roomDoors)
            {
                if (door.directionX == -needToChangeDirX && door.directionY == -needToChangeDirY)
                {
                    GameObject player = PlayerManager.instance.gameObject;
                    player.transform.position = door.transform.position + new Vector3(needToChangeDirX * 2, needToChangeDirY * 2);
                    activateRoom();
                }
            }
        }

    }
    private void Update()
    {
        changeRoomTimer.updateTimer();

        if (changeRoomTimer.isFinished && needToChangeRoom)
        {
            changeRoom();
        }
    }

    private void generateMap()
    {
        playerX = width / 2;
        playerY = height / 2;
        
        int currentX = playerX, currentY = playerY;
        int index = mapLength;
        while(index > 0)
        {
            int direction =  prng.Next(0, 4);
            if(isInMapRange(currentX, currentY) && flagMap[currentX, currentY] != 1){
                flagMap[currentX, currentY] = 1;
                index--;
            }
            if (direction == 0 && currentX < width - 1)
            {
                currentX += 1;
            }else if (direction == 1 && currentX > 0)
            {
                currentX -= 1;
            }
            else if (direction == 2 && currentY < height - 1)
            {
                currentY += 1;
            }
            else if (direction == 3 && currentY > 0)
            {
                currentY -= 1;
            }

            
        }
        
    }

    private void spawnRooms()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (flagMap[i, j] == 1)
                {
                    spawnRoom(i, j);
                }
            }
        }
        playerX = width / 2;
        playerY = height / 2;
        AdvancedRoomGenerator room = getActiveRoom();
        if(room.room[sizeX/2,sizeY/2] == 1){
            Debug.Log("Spawn in wall");

            Vector2Int topLeftCursor = new Vector2Int(0,0);
            Vector2Int bottomRightCursor = new Vector2Int(sizeX-1,sizeY-1);
            Vector2Int lastFoundPlace = new Vector2Int(0,0);
            int iterations = 0;
            while(topLeftCursor.x < bottomRightCursor.x && topLeftCursor.y < bottomRightCursor.y){
                iterations++;
                if(iterations >= 1000){
                    break;
                }
                for(int i = topLeftCursor.x; i < bottomRightCursor.x; i++){
                    if(room.room[i,topLeftCursor.y] != 1){
                        lastFoundPlace = new Vector2Int(i, topLeftCursor.y);
                    }
                }

                for(int i = topLeftCursor.y; i < bottomRightCursor.y; i++){
                    if(room.room[bottomRightCursor.x, i] != 1){
                        lastFoundPlace = new Vector2Int(bottomRightCursor.x, i);
                    }
                }

                for(int i = bottomRightCursor.x; i > topLeftCursor.x; i--){
                    if(room.room[i,bottomRightCursor.y] != 1){
                        lastFoundPlace = new Vector2Int(i, bottomRightCursor.y);
                    }
                }
                
                for(int i = bottomRightCursor.y; i > topLeftCursor.y; i--){
                    if(room.room[topLeftCursor.x,i] != 1){
                        lastFoundPlace = new Vector2Int(topLeftCursor.x, i);
                    }
                }
                topLeftCursor += new Vector2Int(1,1);
                bottomRightCursor -= new Vector2Int(1,1);

            }
            FindObjectOfType<PlayerManager>().transform.position = new Vector2(playerX * sizeX + lastFoundPlace.x, playerY * sizeY + lastFoundPlace.y);
        
        }else{
            Debug.Log("Correctly spawned");
            FindObjectOfType<PlayerManager>().transform.position = new Vector2(playerX * sizeX + sizeX /2, playerY * sizeY + sizeY /2);
        }

    }

    private void spawnRoom(int x, int y)
    {
        if (!isInMapRange(x, y))
            return;


        List<DoorEnum> doors = new List<DoorEnum>();

        if (isInMapRange(x - 1, y))
        {
            if(flagMap[x-1,y] == 1)
                doors.Add(DoorEnum.Left);
        }
        if (isInMapRange(x + 1, y))
        {
            if (flagMap[x + 1, y] == 1)
                doors.Add(DoorEnum.Right);
        }
        if (isInMapRange(x, y+1))
        {
            if (flagMap[x, y+1] == 1)
                doors.Add(DoorEnum.Up);
        }
        if (isInMapRange(x, y-1))
        {
            if (flagMap[x, y-1] == 1)
                doors.Add(DoorEnum.Down);
        }

        map[x, y] = Instantiate(room, new Vector3(x * sizeX, y * sizeY, 0), transform.rotation, this.transform).GetComponent<AdvancedRoomGenerator>();
        playerX = x;
        playerY = y;
        map[x, y].GenerateRoom(prng, sizeX, sizeY, doors);
        map[x, y].roomGui = Instantiate(minimapRoom, new Vector3(25 * x, 25 * y, 0) + minimap.transform.position - new Vector3(25 * width / 2, 25 * height / 2, 0), transform.rotation, minimap.transform).GetComponent<MiniMapRoom>();
    }
    public AdvancedRoomGenerator getActiveRoom()
    {
        return map[playerX, playerY];
    }
    private bool isInMapRange(int x,int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }
    private void activateRoom()
    {
        print("ActivateRoom");
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (flagMap[i, j] == 1)
                {
                    map[i, j].setRoomActive(false);
                    map[i, j].gameObject.SetActive(false);
                    if (map[i, j].isCleared)
                    {
                        map[i, j].roomGui.isActive = false;
                        map[i, j].roomGui.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                    }
                    else
                        map[i, j].roomGui.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
                }
            }
        }
        map[playerX, playerY].gameObject.SetActive(true);
        map[playerX, playerY].setRoomActive(true);
        map[playerX, playerY].roomGui.isActive = true;
        map[playerX, playerY].roomGui.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);

        Grid grid = FindObjectOfType<Grid>();
        grid.createGrid();

    }
}
