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

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("Test: " + otherObject.gameObject.name);
        if (otherObject.gameObject.tag == "Grenze")
        {
            enemySpeed *= -1;
            //StartCoroutine(turn());
        }
    }

    //IEnumerator turn()
    //{
    //    yield return new WaitForSeconds(10.5f);
    //}
}
