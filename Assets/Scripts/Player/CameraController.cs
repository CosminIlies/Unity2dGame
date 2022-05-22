using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothFactor;
    void Start()
    {
        
    }

    
    void Update()
    {
        float interpolation = smoothFactor * Time.deltaTime;
        Vector3 position = this.transform.position;

        position.x = Mathf.Lerp(transform.position.x, player.position.x, smoothFactor * Time.deltaTime);
        position.y = Mathf.Lerp(transform.position.y, player.position.y, smoothFactor * Time.deltaTime);

        if(MapManager.instance != null){

        
            Vector2[] boundery = MapManager.instance.getActiveRoom().getCameraBoundery();
            position.x = Mathf.Clamp(position.x, boundery[0].x, boundery[1].x);
            position.y = Mathf.Clamp(position.y, boundery[0].y, boundery[1].y);

        }

        this.transform.position = position;
    }
}
