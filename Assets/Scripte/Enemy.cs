using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int enemySpeed = 0;
    public int turnSpeed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float amToMove = enemySpeed * Time.deltaTime * turnSpeed;
        transform.Translate(Vector3.right * amToMove);
	}

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("Test: " + otherObject.gameObject.name);
        if (otherObject.gameObject.tag == "Grenze")
        {
            turnSpeed = 0;
            StartCoroutine(turn());
            Debug.Log("Davor: " + enemySpeed);
            enemySpeed *= -1;
            Debug.Log("Danach: " + enemySpeed);
        }
    }

    IEnumerator turn()
    {
        yield return new WaitForSeconds(1.5f);
        turnSpeed = 1;
    }
}
