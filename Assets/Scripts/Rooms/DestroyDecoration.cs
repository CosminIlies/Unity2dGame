using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDecoration : MonoBehaviour
{
    public int health = 5;
    [SerializeField()] private GameObject itemHolder;
    [SerializeField()]private Item item;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Attack"){
            health--;
            
        }
        if(health <= 0){
            PickUpItem spawnedItem = Instantiate(itemHolder, transform.position, Quaternion.identity).GetComponent<PickUpItem>();
            spawnedItem.spawnItem(item, 1);
            Destroy(this.gameObject);
        }
    }
}
