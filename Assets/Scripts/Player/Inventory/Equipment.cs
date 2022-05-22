using UnityEngine;

[CreateAssetMenu(fileName ="new Equipment", menuName ="Items/newEquipment")]

public class Equipment : Item
{
    public float maxHelth;
    public float movementSpeed;
    public float attackSpeed;
    public float damage;
    public EquipmentType type;
}

public enum EquipmentType
{
    Book,
    Ring,
    Necklace,
    Earring
}
