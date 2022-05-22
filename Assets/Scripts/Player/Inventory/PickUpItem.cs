using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    [SerializeField] private Item item;
    [SerializeField] private int count;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        updateGui();
    }

    public void spawnItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
        updateGui();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pickup();
        }
    }

    private void pickup()
    {
        if (InventorySystem.instance.addItem(item, count))
        {
            Destroy(this.gameObject);
        }
        
    }

    public void updateGui()
    {
        spriteRenderer.sprite = item.icon;
    }
}
