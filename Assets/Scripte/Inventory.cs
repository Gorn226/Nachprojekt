using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public GameObject[] Slots = new GameObject[4];
    
    private List<Item> weaponInventory = new List<Item>();
    private List<Item> shieldInventory = new List<Item>();
    private List<Item> potInventory = new List<Item>();
    private List<Item> itemInventory = new List<Item>();

    private int currentWeapon = 0;
    private int currentShield = 0;
    private int currentPot = 0;
    private int currentItem = 0;
    private int currentPosition = 3;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
            switch (col.GetComponent<Item>().type)
            {
                case Item.itemType.Weapon:
                    weaponInventory.Add(col.GetComponent<Item>());
                    Slots[0].transform.GetChild(0).GetComponent<Image>().sprite = weaponInventory[currentWeapon].itemIcon;
                    break;
                case Item.itemType.Shield:
                    shieldInventory.Add(col.GetComponent<Item>());
                    Slots[1].transform.GetChild(0).GetComponent<Image>().sprite = shieldInventory[currentShield].itemIcon;
                    break;
                case Item.itemType.Pot:
                    
                    foreach (var pot in potInventory.TakeWhile(pot => pot.itemID == col.GetComponent<Item>().itemID))
                    {
                        pot.stacks++;
                    }
                    if (potInventory.All(pot => pot.itemID != col.GetComponent<Item>().itemID))
                    {
                        potInventory.Add(col.GetComponent<Item>());
                    }

                    //var match = potInventory.FirstOrDefault(pot => pot.itemID == col.GetComponent<Item>().itemID);
                    //if (match == null)
                    //{
                    //    Debug.Log("neu");
                    //    potInventory.Add(col.GetComponent<Item>());
                    //}
                    //else
                    //{
                    //    match.stacks++;
                    //    Debug.Log("vorhanden");
                    //}

                    Slots[2].transform.GetChild(0).GetComponent<Image>().sprite = potInventory[currentPot].itemIcon;
                    break;
            }
        Destroy(col.gameObject);
    }

    void Update()
    {
        UserInput();
        Debug.Log(currentPosition);
    }

    private void UserInput()
    {
        if (!Input.GetButtonDown("MenuHorizontal")) return;
        { 
        if (Input.GetAxisRaw("MenuHorizontal") < 0 && currentPosition > 0)
            currentPosition--;
        else if (Input.GetAxisRaw("MenuHorizontal") > 0 && currentPosition < 3)
            currentPosition++;
            DrawSelectedSlot();
        }
    }

    private void DrawSelectedSlot()
    {
        Slots[0].transform.GetComponent<Image>().color = Color.white;
        Slots[1].transform.GetComponent<Image>().color = Color.white;
        Slots[2].transform.GetComponent<Image>().color = Color.white;
        Slots[3].transform.GetComponent<Image>().color = Color.white;
        Slots[currentPosition].transform.GetComponent<Image>().color = Color.red;
    }
}
