using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    [SerializeField] List<drop> drops;
    public Item drop()
    {
        float max = 0;
        for (int i =0; i< drops.Count; i++)
        {
            max += drops[i].chanse;
        }

        float rand = (int) Random.Range(0, max + 1);
        int idx = 0;
        while (rand > 0)
        {
            rand -= drops[idx].chanse;

            if(rand <= 0)
            {
                return drops[idx].item;
            }

            idx++;
        }
        return drops[drops.Count-1].item;
    }
}

[System.Serializable()]
class drop
{
    public Item item;
    public float chanse;
}
