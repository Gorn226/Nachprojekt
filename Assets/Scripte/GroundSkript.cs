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
	void Update () 
    {   
        pY = player.transform.position.y-player.transform.localScale.y/2-0.5f;
        if (pY > (transform.position.y - transform.localScale.y / 2))
        {
            
            bCol.enabled = true;
        }
        else 
        {
            bCol.enabled = false;
        }
	    
	}
}
