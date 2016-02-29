using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int enemySpeed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float amToMove = enemySpeed * Time.deltaTime;
        transform.Translate(Vector3.right * amToMove);
	}
}
