using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public enum itemType
    {
        Weapon,
        Shield,
        Pot,
    }

    public int itemID;
    public itemType type;
    public Sprite itemIcon;
    [SerializeField]
    private int value;
    [SerializeField]
    private bool stackable;

    //public Item(itemType type, Sprite icon, int val, bool stack)
    //{
    //    this.type = type;
    //    itemIcon = icon;
    //    value = val;
    //    stackable = stack;
    //}

    public Item()
    {

    }
}
