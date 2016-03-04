using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    //Array für die Slots
    public GameObject[] Slots = new GameObject[4];

    //4 verschiedene Inventare für je ein Itemtyp
    private List<Item> weaponInventory = new List<Item>();
    private List<Item> shieldInventory = new List<Item>();
    private List<Item> potInventory = new List<Item>();
    private List<Item> itemInventory = new List<Item>();

    //aktuelle position des jeweiligen inventars/slots
    private int currentWeapon = 0;
    private int currentShield = 0;
    private int currentPot = 0;
    private int currentItem = 0;
    private int currentPosition = 0;

    //Inventarslots
    private Sprite slotBasic;
    private Sprite slotSelected;

    void OnTriggerEnter2D(Collider2D col)
    {
        //prüft ob ein Item aufgesammelt werden kann, pots können stacken
        if (col.tag == "Item")
            switch (col.GetComponent<Item>().type)
            {
                case Item.itemType.Weapon:
                    weaponInventory.Add(col.GetComponent<Item>());
                    gameObject.GetComponent<Charactercontroller>().hasSword = true;
                    break;
                case Item.itemType.Shield:
                    shieldInventory.Add(col.GetComponent<Item>());
                    gameObject.GetComponent<Charactercontroller>().hasShield = true;
                    break;
                case Item.itemType.Pot:
                    foreach (var pot in potInventory.Where(pot => pot.itemID == col.GetComponent<Item>().itemID))
                    {
                        pot.stacks++;
                    }
                    if (potInventory.All(pot => pot.itemID != col.GetComponent<Item>().itemID))
                    {
                        potInventory.Add(col.GetComponent<Item>());
                    }
                    break;
                case Item.itemType.Special:
                    itemInventory.Add(col.GetComponent<Item>());
                    break;
            }
        //Zeichne das Inventar neu sobald ein Item aufgesammelt wurde, und zerstöre das Item
        DrawSlots();
        Destroy(col.gameObject);
    }

    void Start()
    {
        //TODO Inventar in die nächste szene übernehmen
        DontDestroyOnLoad(gameObject);

        //testet ob der spieler ein schild bzw. schwert besitzt
        if (shieldInventory.Count < 1)
            gameObject.GetComponent<Charactercontroller>().hasShield = false;
        if (weaponInventory.Count < 1)
            gameObject.GetComponent<Charactercontroller>().hasSword = false;

        //zeichnet das Inventar
        slotBasic = Resources.Load<Sprite>("3_item_select_slot1");
        slotSelected = Resources.Load<Sprite>("3_item_select_slot2");
        DrawSlots();
    }

    void Update()
    {
        //ruft die Benutzereingabe auf
        InventoryControl();
        UseItem();
    }

    private void UseItem()
    {
        if (Input.GetButtonDown("Pot"))
        {
            //falls man keinen pot besitzt so passiert nichts
            if (potInventory.Count == 0 || gameObject.GetComponent<Charactercontroller>().health > 5) return;
            //wenn der aktuelle pot mehr als einen stack hat, so wird dieser nur um 1 verringert
            else if (potInventory[currentPot].stacks > 1)
                potInventory[currentPot].stacks--;
            //wenn er nur einen stack hat,
            else
            {
                //und der oberste pot ist, es aber noch pots unter ihm gibt
                //so wird der pot unter ihm ausgewählt und der darüber gelöscht
                if (currentPot == potInventory.Count - 1 && potInventory.Count > 1)
                {
                    currentPot -= 1;
                    potInventory.Remove(potInventory[currentPot + 1]);
                }
                //es ein pot ist bei dem noch nachruchtschen kann, 
                //oder der letzte pot ist dann wird er gelöscht
                else
                {
                    potInventory.Remove(potInventory[currentPot]);
                }
            }
            if (gameObject.GetComponent<Charactercontroller>().health > 3)
                gameObject.GetComponent<Charactercontroller>().health = 6;
            else
            {
                gameObject.GetComponent<Charactercontroller>().health += 2;
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Heartskript>().setHearts(gameObject.GetComponent<Charactercontroller>().health);
            DrawSlots();
        }
    }

    private void InventoryControl()
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
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentWeapon > 0)
                        currentWeapon--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentWeapon < weaponInventory.Count - 1)
                        currentWeapon++;
                    break;
                case 1:
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentShield > 0)
                        currentShield--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentShield < shieldInventory.Count - 1)
                        currentShield++;
                    break;
                case 2:
                    if (Input.GetAxisRaw("MenuVertical") < 0 && currentPot > 0)
                        currentPot--;
                    else if (Input.GetAxisRaw("MenuVertical") > 0 && currentPot < potInventory.Count - 1)
                        currentPot++;
                    break;
                case 3:
                    break;
            }
        }
        DrawSlots();
    }

    private void DrawSlots()
    {
        Slots[0].transform.GetComponent<Image>().sprite = slotBasic;
        if (weaponInventory.Count != 0)
        {
            Slots[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
            Slots[0].transform.GetChild(0).GetComponent<Image>().sprite = weaponInventory[currentWeapon].itemIcon;
        }
        else
            Slots[0].transform.GetChild(0).GetComponent<Image>().enabled = false;
        Slots[1].transform.GetComponent<Image>().sprite = slotBasic;
        if (shieldInventory.Count != 0)
        {
            Slots[1].transform.GetChild(0).GetComponent<Image>().enabled = true;
            Slots[1].transform.GetChild(0).GetComponent<Image>().sprite = shieldInventory[currentShield].itemIcon;
        }
        else
            Slots[1].transform.GetChild(0).GetComponent<Image>().enabled = false;
        Slots[2].transform.GetComponent<Image>().sprite = slotBasic;
        if (potInventory.Count != 0)
        {
            Slots[2].transform.GetChild(0).GetComponent<Image>().enabled = true;
            Slots[2].transform.GetChild(1).GetComponent<Text>().enabled = true;
            Slots[2].transform.GetChild(0).GetComponent<Image>().sprite = potInventory[currentPot].itemIcon;
            Slots[2].transform.GetChild(1).GetComponent<Text>().text = "" + potInventory[currentPot].stacks;
        }
        else
        {
            Slots[2].transform.GetChild(0).GetComponent<Image>().enabled = false;
            Slots[2].transform.GetChild(1).GetComponent<Text>().enabled = false;
        }
        Slots[3].transform.GetComponent<Image>().sprite = slotBasic;
        if (itemInventory.Count != 0)
        {
            Slots[3].transform.GetChild(0).GetComponent<Image>().enabled = true;
            Slots[3].transform.GetChild(0).GetComponent<Image>().sprite = itemInventory[currentItem].itemIcon;
        }
        else
            Slots[3].transform.GetChild(0).GetComponent<Image>().enabled = false;
        Slots[currentPosition].transform.GetComponent<Image>().sprite = slotSelected;
    }
}
