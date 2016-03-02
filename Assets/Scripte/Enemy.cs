using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float enemySpeedstart=1; 
    private float enemySpeed;
    public int turnSpeed = 1;
    public int lives = 3;
    public float wasHitForce = 5000;
    Rigidbody2D rb2d;
    public float enemyHitSpeed =4;
	// Use this for initialization
    void Awake()
    {
        enemySpeed = enemySpeedstart;
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float amToMove = enemySpeed * Time.deltaTime * turnSpeed;
        transform.Translate(Vector3.right * amToMove);

       
	}

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Grenze")
        {
            turnSpeed = 0;
            StartCoroutine(turn());
            enemySpeed *= -1;
        }
        if (otherObject.gameObject.tag == "Sword" && lives > 0)
        {
            lives--;
            enemySpeed = enemyHitSpeed;
            
        }
        else if (lives == 0)
            Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (enemySpeed > enemySpeedstart)
        {
            enemySpeed -= 0.1f;
        }
    }

    IEnumerator turn()
    {
        yield return new WaitForSeconds(1.5f);
        turnSpeed = 1;
    }
}
