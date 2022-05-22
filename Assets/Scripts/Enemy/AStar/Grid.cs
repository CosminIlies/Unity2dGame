using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public bool displayGridGizmos =true;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public int maxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        createGrid();
    }

    public void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = new Vector2 (transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y/2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x,y);
            }
        }
    }

    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                
                if(checkX >= 0 && checkX < gridSizeX-1 && checkY >= 0 && checkY < gridSizeY-1)
                {
                    neighbours.Add(grid[checkX,checkY]);
                    
                }
            }
        }

        return neighbours;
    }

    public Node nodeFromWorldPoint(Vector2 worldPosition)
    {

        worldPosition -= new Vector2(transform.position.x, transform.position.y);
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        if(displayGridGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

            if (grid != null)
            {
                Node playerNode = nodeFromWorldPoint(PlayerManager.instance.gameObject.transform.position);
                foreach (Node node in grid)
                {
                    Gizmos.color = node.walkable ? Color.green : Color.red;
                    if (playerNode == node)
                    {
                        Gizmos.color = Color.cyan;
                    }

                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
