using UnityEngine;

[System.Serializable]
public class ApplyDmg 
{

    [SerializeField] private float dmg;
    [SerializeField] public bool affectPlayer;
    [SerializeField] private float lifeTime;
    
    
    private Timer timer;
    private bool dmgWasApplied = false;

    public void init()
    {
        timer = new Timer(lifeTime);
    }
    public void init(float lifeTime, float dmg)
    {
        this.lifeTime = lifeTime;
        timer = new Timer(lifeTime);
    }

    public void init(float lifeTime, float dmg, bool affectPlayer)
    {
        this.lifeTime = lifeTime;
        this.dmg = dmg;
        this.affectPlayer = affectPlayer;
        timer = new Timer(lifeTime);
    }

    public bool isFinished()
    {
        timer.updateTimer();
        if (timer.isFinished)
        {
            return true;
        }
        return false;
    }

    public void applyDmg(Collider2D collision)
    {
        if(affectPlayer && !dmgWasApplied)
        {
            PlayerManager manager = collision.gameObject.GetComponent<PlayerManager>();
            manager.getHit(dmg);
        }

        if(!affectPlayer && !dmgWasApplied){
            EnemyManager manager = collision.gameObject.GetComponent<EnemyManager>();
            manager.getHit(dmg);
        }
  
        dmgWasApplied = true;

    }
}
