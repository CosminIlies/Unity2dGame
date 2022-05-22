using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public int directionX, directionY;

    private BoxCollider2D coll;
    private AdvancedRoomGenerator room;

    public void init( int directionX, int directionY)
    {

        this.directionX = directionX;
        this.directionY = directionY;
        coll = gameObject.GetComponent<BoxCollider2D>();
        room = MapManager.instance.getActiveRoom();
    }

    private void Update()
    {
        if (room.isCleared)
        {
            coll.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && room.isCleared)
        {
            MapManager.instance.changeRoom(directionX,directionY);
        }
    }

}
