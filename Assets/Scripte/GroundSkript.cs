using UnityEngine;
using System.Collections;

public class GroundSkript : MonoBehaviour {

    public BoxCollider2D bCol;
    public GameObject player;
    float pY;

	// Use this for initialization
	void Start () 
    {
        bCol.enabled = false;
        pY = player.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
        pY = player.transform.position.y-player.transform.localScale.y/2f+0.1f;
<<<<<<< HEAD
        if (pY > ((bCol.bounds.center.y + bCol.bounds.extents.y / 2f)-1.0f))
=======
        if (pY > ((bCol.bounds.center.y + bCol.bounds.extents.y / 2f)-1.5f))
>>>>>>> 4f8db975c68b8dd409cd416f3f73d292440dadbe
        {
            bCol.enabled = true;
        }
        else 
        {
           bCol.enabled = false;
        }
      //  Debug.Log(("PY " + pY + " Ground " + (bCol.transform.position.y + bCol.transform.localScale.y / 2)-0.1f) + " "+ bCol.enabled);
	    
	}
}
