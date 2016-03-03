using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    enum state { normal, invincible }
    private state st = state.normal;
    public float enemySpeedstart = 2;
    public float enemySpeed;
    public float actualSpeed;
    public int turnSpeed = 1;
    public int lives = 3;
    public float wasHitForce = 5000;
    public float enemyHitSpeed = 4;
    private bool OnHit = false;
    Animator animator;
    private float speedLastFrame;
    Rigidbody2D rb2d;
    private bool faceRight = true;

    // Use this for initialization
    void Awake()
    {
        enemySpeed = enemySpeedstart;
        actualSpeed = enemySpeedstart;
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
        // speedLastFrame= rb2d.
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Grenze")
        {
            turnSpeed = 0;
            StartCoroutine(turn());
            enemySpeed = -1 * actualSpeed;
            actualSpeed *= -1;
            faceRight = !faceRight;

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

        if (lives >0 && ((actualSpeed > 0 && actualSpeed > enemySpeed) || (actualSpeed < 0 && actualSpeed > enemySpeed)))
        {
            enemySpeed += 0.1f;
        }

        if (lives > 0 && ((actualSpeed > 0 && actualSpeed < enemySpeed) || (actualSpeed < 0 && actualSpeed < enemySpeed)))
        {
            enemySpeed -= 0.1f;
        }

    }
    IEnumerator hit()
    {
        OnHit = true;
        yield return new WaitForSeconds(50 / ((enemyHitSpeed - enemySpeedstart) / 0.1f));
        if (faceRight)
        {
            enemySpeed = enemySpeedstart;
        }
        else
        {
            enemySpeed = -1 * enemySpeedstart;
        }
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
