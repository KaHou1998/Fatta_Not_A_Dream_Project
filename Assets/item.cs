using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        itemOne,
        itemTwo,
        itemThree
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.itemOne: return ItemAssets.Instance.itemOneSprite;
            case ItemType.itemTwo: return ItemAssets.Instance.itemTwoSprite;
            case ItemType.itemThree: return ItemAssets.Instance.itemThreeSprite;
        }
    }

    public bool IsStackable()
    {
        switch(itemType)
        {
            default:
            case ItemType.itemOne:
            case ItemType.itemTwo:
                return false;
            case ItemType.itemThree:
                return true;
        }
    }

    
}
