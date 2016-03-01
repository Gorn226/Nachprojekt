using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    private int [] weaponInventory;
    private int [] shieldInventory;
    private int [] potInventory;
    private Item [] itemInventory;

    private int currentItem = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item") {
            itemInventory[currentItem] = col.GetComponent<Item>();
            currentItem++;
            Destroy (col);
        }
    }
}
