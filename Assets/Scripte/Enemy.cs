using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    enum state { normal, invincible }
    private state st = state.normal;
    public float enemySpeedstart = 2;
    public float enemySpeed;
    public int turnSpeed = 1;
    public int lives = 3;
    public float wasHitForce = 5000;
    public float enemyHitSpeed = 4;
    private bool OnHit = false;
    Animator animator;
    public float speedLastFrame;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        enemySpeed = enemySpeedstart;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float amToMove = enemySpeed * Time.deltaTime * turnSpeed;
        animator.SetFloat("turnSpeed", turnSpeed);
        transform.Translate(Vector3.right * amToMove);
        animator.SetBool("OnHit", OnHit);
        animator.SetInteger("lives", lives);
        //LoseLife();

    }
    void LastUpdate()
    {
        speedLastFrame = rb2d.velocity.x;
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Grenze")
        {        
            turnSpeed = 0;
            StartCoroutine(turn());
            enemySpeed *= -1;
        }
        if (otherObject.gameObject.tag == "Sword" && lives > 0 && st == state.normal)
        {

            lives--;
            if (lives == 0)
            {
                turnSpeed = 0;
                transform.GetComponent<PolygonCollider2D>().enabled = false;
                //StartCoroutine(timeTillDeath());
                //Destroy(gameObject);
            }
            else
            {
                StartCoroutine(hit());
                enemySpeed = enemyHitSpeed;
                if (!playerLeft(otherObject))
                {
                    enemySpeed *= -1;
                }
                StartCoroutine(invincible());

            }
        }
    }

    //void LoseLife()
    //{
    //    StartCoroutine(timeTillDeath());
    //    if (lives <= 0)
    //    {
    //        turnSpeed = 0;
    //        transform.GetComponent<BoxCollider2D>().enabled = false;
    //    }
    //}

    private bool playerLeft(Collider2D ob)
    {
        float div = transform.position.x - ob.transform.position.x;
        if (div < 0)
        {
            return false;
        }
        return true;
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(enemySpeed) > enemySpeedstart)
        {
            if (enemySpeed <= 0)
                enemySpeed += 0.1f;
            else
                enemySpeed -= 0.1f; 
        }
    }
    IEnumerator hit()
    {
        OnHit = true;
        yield return new WaitForSeconds(50/((enemyHitSpeed-enemySpeedstart)/0.1f));
        OnHit = false;
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
        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y,
                                               transform.localScale.z);
        turnSpeed = 1;
    }

    //IEnumerator timeTillDeath()
    //{
    //    yield return new WaitForSeconds(2f);
    //    lives--;
    //}
}
