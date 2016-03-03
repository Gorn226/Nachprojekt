using UnityEngine;
using System.Collections;

public class GroundSkript : MonoBehaviour {

    public BoxCollider2D bCol;
    public GameObject player;
    float pX;

	// Use this for initialization
	void Start () 
    {
        bCol.enabled = false;
        pX = player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
    {
        pX = player.transform.position.y;
	    
	}
}
