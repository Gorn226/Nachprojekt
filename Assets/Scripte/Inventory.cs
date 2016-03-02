using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;

    private List<Item> weaponInventory;
    private List<Item> shieldInventory;
    private List<Item> potInventory;
    private List<Item> itemInventory;

    private int currentWeapon = 0;
    private int currentShield = 0;
    private int currentPot = 0;
    private int currentItem = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
            switch (col.GetComponent<Item>().type)
            {
                case Item.itemType.Weapon:
                    weaponInventory.Add(col.GetComponent<Item>());
                    break;
                case Item.itemType.Shield:
                    shieldInventory.Add(col.GetComponent<Item>());
                    break;
                case Item.itemType.Pot:
                    Slot4.transform.GetChild(0).GetComponent<Image>().sprite = col.GetComponent<Item>().itemIcon;
                    //potInventory.Add(col.gameObject.GetComponent<Item>());
                    potInventory.Add(col.gameObject.GetComponent<Item>());
                    break;
            }
        Destroy(col.gameObject);
    }
}
