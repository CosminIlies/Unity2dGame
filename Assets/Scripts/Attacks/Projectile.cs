using UnityEngine;

public class Projectile : MonoBehaviour
{


    private float speed;
    public ApplyDmg applyDmg;


    public void init(float dmg, float lifeTime, float speed, string affectTag)
    {

        this.speed = speed;
        applyDmg.init( lifeTime, dmg, affectTag =="Player"? true : false);
;
    }

    private void Update()
    {

        if(applyDmg.isFinished()){
            Destroy(this.gameObject);
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(applyDmg.affectPlayer){
            PlayerManager manager = collision.gameObject.GetComponent<PlayerManager>();
            if(manager != null){
                applyDmg.applyDmg( collision );
                Destroy(this.gameObject);
            }
        }else{
            EnemyManager manager = collision.gameObject.GetComponent<EnemyManager>();
            if(manager != null){
                applyDmg.applyDmg( collision );
                Destroy(this.gameObject);
            }
        }

        
    }
}
