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
    public int stacks;
    [SerializeField]
    private int value;
    
}
