using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.itemOne, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.itemTwo, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.itemThree, amount = 1 });
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
