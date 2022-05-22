using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    AStar pathFinding;
    bool isProcessingPath;
    /*
    #region Singleton
    public static PathRequestManager instance;
    private void Awake()
    {
        instance = this;
        
    }
    #endregion*/

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;


    private void Update()
    {
        if(pathFinding == null)
            pathFinding = GetComponent<AStar>();
    }

    /*public static void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action <Vector2[], bool> callback)
    {
        if (instance.pathFinding != null)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }
    }*/

    public void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback)
    {
        if (pathFinding != null)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            pathRequestQueue.Enqueue(newRequest);
            TryProcessNext();
        }
    }

    void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector2[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector2[], bool> callback;
        public PathRequest(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback)
        {
            this.pathEnd = pathEnd;
            this.pathStart = pathStart;
            this.callback = callback;
        }
    }
}
