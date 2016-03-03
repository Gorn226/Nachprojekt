using UnityEngine;
using UnityEngine.UI;
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
    private int currentPosition = 0;

    private Sprite slotBasic;
    private Sprite slotSelected;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
            switch (col.GetComponent<Item>().type)
            {
                case Item.itemType.Weapon:
                    weaponInventory.Add(col.GetComponent<Item>());
                    gameObject.GetComponent<Charactercontroller>().hasSword = true;
                    break;
                case Item.itemType.Shield:
                    shieldInventory.Add(col.GetComponent<Item>());
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
                    break;
            }
        DrawSlots();
        Destroy(col.gameObject);
    }

    void Start()
    {
        //DontDestroyOnLoad()
        if (shieldInventory.Count < 1)
            gameObject.GetComponent<Charactercontroller>().hasShield = false;
        if (weaponInventory.Count < 1)
            gameObject.GetComponent<Charactercontroller>().hasSword = false;
        slotBasic = Resources.Load<Sprite>("3_item_select_slot1");
        slotSelected = Resources.Load<Sprite>("3_item_select_slot2");
        DrawSlots();
    }

    void Update()
    {
        UserInput();
    }

    private void UserInput()
    {
        if (Input.GetButtonDown("MenuHorizontal"))
        {
            if (Input.GetAxisRaw("MenuHorizontal") < 0 && currentPosition > 0)
                currentPosition--;
            else if (Input.GetAxisRaw("MenuHorizontal") > 0 && currentPosition < Slots.Length - 1)
                currentPosition++;
        }

        if (Input.GetButtonDown("MenuVertical"))
        {
            switch (currentPosition)
            {
                case 0:
                    Debug.Log("schwert");
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentWeapon > 0)
                        currentWeapon--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentWeapon < weaponInventory.Count - 1)
                        currentWeapon++;
                    break;
                case 1:
                    Debug.Log("Schild");
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentShield > 0)
                        currentShield--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentShield < shieldInventory.Count - 1)
                        currentShield++;
                    break;
                case 2:
                    Debug.Log("pot");
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentPot > 0)
                        currentPot--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentPot < potInventory.Count - 1)
                        currentPot++;
                    break;
                case 3:
                    Debug.Log("inv");
                    break;
            }
        }
        DrawSlots();
    }

    private void DrawSlots()
    {
        Slots[0].transform.GetComponent<Image>().sprite = slotBasic;
        if (weaponInventory.Count != 0)
            Slots[0].transform.GetChild(0).GetComponent<Image>().sprite = weaponInventory[currentWeapon].itemIcon;
        Slots[1].transform.GetComponent<Image>().sprite = slotBasic;
        if (shieldInventory.Count != 0)
            Slots[1].transform.GetChild(0).GetComponent<Image>().sprite = shieldInventory[currentShield].itemIcon;
        Slots[2].transform.GetComponent<Image>().sprite = slotBasic;
        if (potInventory.Count != 0)
            Slots[2].transform.GetChild(0).GetComponent<Image>().sprite = potInventory[currentPot].itemIcon;
        Slots[3].transform.GetComponent<Image>().sprite = slotBasic;
        Slots[currentPosition].transform.GetComponent<Image>().sprite = slotSelected;
    }
}
