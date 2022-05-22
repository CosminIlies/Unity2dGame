
[System.Serializable]
public class EnemyStats {

    public Stats stats;
    public float attackRange, chaseRange;
    public ItemDrop drops;

    public void init()
    {
        stats.init();
    }

}
