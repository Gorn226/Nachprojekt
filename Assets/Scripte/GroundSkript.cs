using UnityEngine;
using System.Collections;

public class GroundSkript : MonoBehaviour {

    public BoxCollider2D bCol;
    private GameObject player;
    float pY;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bCol.enabled = false;
        pY = player.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
        pY = player.transform.position.y-player.transform.localScale.y/2f+0.1f;


        if (pY > ((bCol.bounds.center.y + bCol.bounds.extents.y / 2f)-1.5f))


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
