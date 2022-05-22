using UnityEngine;

public class Slash : MonoBehaviour
{

    public EffectAsset effectAsset;
    public ApplyDmg applyDmg;


    private void Start()
    {
;
        applyDmg.init();
    }

    private void Update()
    {

        if(applyDmg.isFinished()){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(applyDmg.affectPlayer){
            PlayerManager manager = collision.gameObject.GetComponent<PlayerManager>();
            if(manager != null){
                applyDmg.applyDmg( collision );
                manager.playerStats.stats.addEffect(new Bleed(effectAsset));
            }
        }else{
            EnemyManager manager = collision.gameObject.GetComponent<EnemyManager>();
            if(manager != null){
                applyDmg.applyDmg( collision );
                manager.enemyStats.stats.addEffect(new Bleed(effectAsset));
            }
        }

    }
}
