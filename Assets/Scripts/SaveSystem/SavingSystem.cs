using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
#region Singleton
    public static SavingSystem instance;
    private void Awake() {
        instance = this;
    }
#endregion


    void Start(){
        Load();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.O)){
            Save();
        }
        if(Input.GetKeyDown(KeyCode.P)){
            Load();
        }

    }

    public void Save(){
        PlayerManager manager = PlayerManager.instance;
        Vector3 playerPos = new Vector3();
        float money = PlayerStats.instance.money;

        if(manager != null)
            playerPos = manager.gameObject.transform.position;
        List<InventorySlot> slots = InventorySystem.instance.slots;
        int[] items = new int[slots.Count];
        int[] count = new int[slots.Count];

        for(int i = 0; i < slots.Count; i++){
            items[i] = ItemsDataBase.instance.GetItemId(slots[i].item);
            count[i] = slots[i].count;
        }



        SaveObject saveObject = new SaveObject{
            playerPos = playerPos,
            money = money,
            items = items,
            count = count,
            book = ItemsDataBase.instance.GetItemId(PlayerStats.instance.book.item),
            earring = ItemsDataBase.instance.GetItemId(PlayerStats.instance.earring.item),
            necklace = ItemsDataBase.instance.GetItemId(PlayerStats.instance.necklace.item),
            ring = ItemsDataBase.instance.GetItemId(PlayerStats.instance.ring.item),
        };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
        Debug.Log(ItemsDataBase.instance.GetItemId(slots[0].item));

    }

    public void Load(){
        if(File.Exists(Application.dataPath + "/save.txt")){
            string json = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObject loadedSaveobject = JsonUtility.FromJson<SaveObject>(json);
            PlayerManager manager = PlayerManager.instance;
            if(loadedSaveobject != null){
                //manager.gameObject.transform.position = loadedSaveobject.playerPos; 
                manager.playerStats.money = loadedSaveobject.money;

                if(loadedSaveobject.book != -1){
                    PlayerStats.instance.book.item = (Equipment)ItemsDataBase.instance.items[loadedSaveobject.book];
                    PlayerStats.instance.book.updateGui();
                }
                if(loadedSaveobject.earring != -1){
                    PlayerStats.instance.earring.item = (Equipment)ItemsDataBase.instance.items[loadedSaveobject.earring];
                    PlayerStats.instance.earring.updateGui();
                }
                if(loadedSaveobject.necklace != -1){
                    PlayerStats.instance.necklace.item = (Equipment)ItemsDataBase.instance.items[loadedSaveobject.necklace];
                    PlayerStats.instance.necklace.updateGui();
                }
                if(loadedSaveobject.ring != -1){
                    PlayerStats.instance.ring.item = (Equipment)ItemsDataBase.instance.items[loadedSaveobject.ring];
                    PlayerStats.instance.ring.updateGui();
                }


                for(int i = 0; i < InventorySystem.instance.slots.Count; i++){
                    if(loadedSaveobject.items[i] != -1){
                        InventorySystem.instance.slots[i].item = ItemsDataBase.instance.items[loadedSaveobject.items[i]];
                        InventorySystem.instance.slots[i].count = loadedSaveobject.count[i];
                    }else{
                        InventorySystem.instance.slots[i].item = null;
                        InventorySystem.instance.slots[i].count = 0;
                    }

                    InventorySystem.instance.slots[i].updateGui();

                }
                
            }

            PlayerStats.instance.updateStats();
        }
    }

    public class SaveObject{
        public float health;
        public float money;
        public Vector3 playerPos;

        public int[] items;
        public int[] count;

        public int book;
        public int earring;
        public int necklace;
        public int ring;

    }
}
