using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    float movementSpeed;
    public Vector2[] path;
    int targetIndex;

    Timer timer;

    Vector2 currentWaypoint;
    PathRequestManager pathfinderManager;
    [HideInInspector] public bool canWalk = true;
    void Start()
    {
        timer = new Timer(0.5f);
    }

    private void Update()
    {
        timer.updateTimer();
        if (player == null)
            player = FindObjectOfType<PlayerManager>().transform;

        if (timer.isFinished)
        {
            if (pathfinderManager == null)
                pathfinderManager = FindObjectOfType<PathRequestManager>();

            if (pathfinderManager != null)
                pathfinderManager.RequestPath(transform.position, player.position, OnPathFound);

            timer.reset();

        }

        if (path != null)
        {
            if (new Vector2(transform.position.x, transform.position.y) == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex < path.Length)
                    currentWaypoint = path[targetIndex];
            }

            
        }
        if (canWalk)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
        }

    }
    public void OnPathFound(Vector2[] newPath, bool isSuccessful)
    {
        if (isSuccessful && newPath.Length > 0)
        {
            path = newPath;
            targetIndex = 0;
            currentWaypoint = path[0];
        }
    }


    public void setMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(0.1f,0.1f,0.1f));

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
