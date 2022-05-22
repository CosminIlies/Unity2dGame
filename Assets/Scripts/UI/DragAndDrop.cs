using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    [SerializeField] private Canvas canvas;
    [SerializeField] private SlotType type;
    [SerializeField] private GameObject onHoldParent;

    private GameObject parent;
    private RectTransform rectTransform;
    private CanvasGroup group;
    private Vector3 startPos;

    private InventorySlot slotInventory;
    private EquipmentSlot slotEquipment;


    private void Awake()
    {
        parent = this.transform.parent.gameObject;
        rectTransform = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        if (type == SlotType.InventorySlot)
        {
            slotInventory = GetComponent<InventorySlot>();
        }
        else
        {
            slotEquipment = GetComponent<EquipmentSlot>();
        }
    }
    private void Start()
    {
        startPos = rectTransform.anchoredPosition;
    }




    public void OnBeginDrag(PointerEventData eventData)
    {
        if (type == SlotType.InventorySlot)
        {
            if (slotInventory.item == null)
                return;
        }
        else
        {
            if (slotEquipment.item == null)
                return;
        }

        gameObject.transform.parent = onHoldParent.transform;

        group.blocksRaycasts = false;
        group.alpha = 0.75f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (type == SlotType.InventorySlot)
        {
            if (slotInventory.item == null)
                return;
        }
        else
        {
            if (slotEquipment.item == null)
                return;
        }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.parent = parent.transform;

        group.blocksRaycasts = true;
        group.alpha = 1f;
        rectTransform.anchoredPosition =  startPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (type == SlotType.InventorySlot)
        {
            if (slotInventory.item == null)
                return;
        }
        else
        {
            if (slotEquipment.item == null)
                return;
        }
    }

    enum SlotType
    {
        InventorySlot,
        EquipmentSlot
    }

}
