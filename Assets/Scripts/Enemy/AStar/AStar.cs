using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AStar : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector2 startPos, Vector2 endPos)
    {
        StartCoroutine(FindPath(startPos, endPos));
    }

    IEnumerator FindPath(Vector2 startPos, Vector2 endPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;

        Node startNode = grid.nodeFromWorldPoint(startPos);
        Node endNode = grid.nodeFromWorldPoint(endPos);

        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();

            closeSet.Add(currentNode);

            if (currentNode == endNode)
            {
                sw.Stop();
                //print("PathFound: " + sw.ElapsedMilliseconds + "ms. From "+startPos+"to"+endPos );
                pathSuccess = true;


                break;
            }


            foreach (Node neighbour in grid.getNeighbours(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                    continue;

                int newMovementContToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                if (newMovementContToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementContToNeighbour;
                    neighbour.hCost = getDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }

        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, endNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currnetNode = endNode;
        while (currnetNode != startNode)
        {
            path.Add(currnetNode);
            currnetNode = currnetNode.parent;
        }
        List<Vector2> waypoints = SimplifyPath(path);
        waypoints.Reverse();

        return waypoints.ToArray();
    }
    List<Vector2> SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX- path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
            //waypoints.Add(path[i].worldPosition);
        }

        return waypoints;
    }

    int getDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if(distX >distY)
            return 14 * distY + 10 * (distX - distY);
        else
            return 14 * distX + 10 * (distY - distX);
    }
}
