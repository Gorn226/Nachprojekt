using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    enum state {normal, invincible}
    private state st = state.normal;
    public float enemySpeedstart=1; 
    private float enemySpeed;
    public int turnSpeed = 1;
    public int lives = 3;
    public float wasHitForce = 5000;
    public float enemyHitSpeed =4;
	// Use this for initialization
    void Awake()
    {
        enemySpeed = enemySpeedstart;
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
        if (otherObject.gameObject.tag == "Sword" && lives > 0&&st ==state.normal)
        {
            lives--;
            enemySpeed = enemyHitSpeed;
            StartCoroutine(invincible());
            
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
    IEnumerator invincible()
    {
        st = state.invincible;
        yield return new WaitForSeconds(1f);
        st = state.normal;
    }
    IEnumerator turn()
    {
        yield return new WaitForSeconds(1.5f);
        turnSpeed = 1;
    }
}
