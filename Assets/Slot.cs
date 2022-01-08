using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemProperty item;

    public int amount;

    [Header("Screne Referce")]
    public Image itemIcon;
   
    
    public void Init()
    {
        itemIcon.sprite = item.itemIcon;
    }
}
