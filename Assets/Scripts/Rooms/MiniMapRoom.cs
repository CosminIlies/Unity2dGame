using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapRoom : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private Sprite active, inActive;

    private void Update()
    {
            gameObject.GetComponent<Image>().sprite = isActive? inActive: active;

    }
}
