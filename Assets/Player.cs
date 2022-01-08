using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI uiInventory;

    private void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(20, 0, 20), new Item { itemType = Item.ItemType.itemOne, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-20, 0, 20), new Item { itemType = Item.ItemType.itemTwo, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(20, 0, -20), new Item { itemType = Item.ItemType.itemThree, amount = 1 });
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            itemWorld.MoveTowardTarget(this.transform);
            inventory.AddItem(itemWorld.GetItem());
        }
    }
}
