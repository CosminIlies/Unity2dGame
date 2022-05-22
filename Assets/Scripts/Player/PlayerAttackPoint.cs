using UnityEngine;

public class PlayerAttackPoint : MonoBehaviour
{
    [SerializeField] private GameObject spawnOrbsPoint;
    [SerializeField] private float timeUntilShoot = 0.15f;
    [SerializeField] private Attack basicAttack;
   
    
    public Camera cam;
    private float smoothRotation = 7f;
    private bool shouldShoot = false;
    private Timer timer;
    private SpriteRenderer graphics;


    private void Start()
    {
        timer = new Timer(timeUntilShoot);
        if(cam == null)
            cam = Camera.main;
        graphics = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        

        timer.updateTimer();
        Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,rotZ), smoothRotation * Time.deltaTime) ;


        if(diff.x > 0)
        {
            graphics.flipY = false;
        }
        else
        {
            graphics.flipY = true;
        }

        if (shouldShoot == true && timer.isFinished)
        {
            foreach (Proj projectile in basicAttack.projectiles)
            {
                GameObject obj =  Instantiate(projectile.obj, spawnOrbsPoint.transform.position + projectile.statingPosition,
                    Quaternion.Euler(transform.rotation.eulerAngles.x + projectile.statingRotation.x,
                                        transform.rotation.eulerAngles.y + projectile.statingRotation.y,
                                        transform.rotation.eulerAngles.z + projectile.statingRotation.z));

                obj.GetComponent<Projectile>().init(PlayerStats.instance.stats.dmg, projectile.lifeTime, projectile.speed, projectile.affectTag);
            }

            
            shouldShoot = false;
        }

    }

    public void attack()
    {
        shouldShoot = true;
        timer.reset();
        
    }
}
