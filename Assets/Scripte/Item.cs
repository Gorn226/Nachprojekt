using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public enum itemType
    {
        Weapon,
        Shield,
        Pot,
        Special,
    }

    public int itemID;
    public itemType type;
    public Sprite itemIcon;
    public int stacks;
    public int value;
    
}
