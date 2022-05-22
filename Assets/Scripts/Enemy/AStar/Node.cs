using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node:IHeapItem<Node>{

    public bool walkable;
    public Vector2 worldPosition;
    public int gridX, gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    int heapIdx;

    public Node(bool walkable, Vector2 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost{
        get {
            return gCost + hCost;
        }
    }

    public int heapIndex
    {
        get
        {
            return heapIdx;
        }
        set
        {
            heapIdx = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
