using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IEnemyAttack
{
    public GameObject projectile;
    private int speed;
    private Vector2 position, rotation;

    public void setAttack(int speed, Vector2 position, Vector2 rotation)
    {
        this.speed = speed;
        this.position = position;
        this.rotation = rotation;
    }
    
    public void attack()
    {
        /*GameObject obj = Instantiate(projectile, position + projectile.statingPosition,
        Quaternion.Euler(rotation.x + projectile.statingRotation.x,
                        rotation.y + projectile.statingRotation.y,
                        rotation.z + projectile.statingRotation.z));

        obj.GetComponent<Projectile>().init(PlayerStats.instance.dmg, projectile.lifeTime, projectile.speed, projectile.affectTag);*/
    }


}
