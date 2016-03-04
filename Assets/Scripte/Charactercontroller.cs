using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Charactercontroller : MonoBehaviour
{
    enum state { normal, invincible};

    private GameObject cam;
    state st = state.normal;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 100f;
    private Vector3 groundCheck;
    public int health = 3;
    public float invinTime =0.5f; // Zeit wie Lange man unverwuntbar ist
    public bool armed = false;
    public bool hasShield = false;
    public bool hasSword = true;
    public GameObject shield;
    bool shieldpresst = false;
    public GameObject sword;
    bool hitting = false;
    bool groundedRight = false;
    bool groundedLeft = false;
    Vector3 vec;
    public BoxCollider2D b2D;

    private bool grounded = false;
    Rigidbody2D rb2d;
    public float height;
    public float gravityPlus =0f;

    public float wasHitForce=5000;
    public float knightSpeed;
    Animator animator;

    public bool death = false;

    // Use this for initialization
    void Awake()
    {
        b2D = GetComponent<BoxCollider2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;

    }
    void Start()
    {
        
        shield.SetActive(false);
        sword.SetActive(false);
        animator = GetComponent<Animator>();
        groundCheck = new Vector3(0, b2D.bounds.extents.y+0.1f, 0);
        animator.SetBool("hitting", hitting);
        animator.SetBool("grounded", grounded);
        animator.SetBool("death", death);
        vec = new Vector3(b2D.bounds.extents.x,0,0);
    }

    // Update is called once per frame
    void Update()
    {

        knightSpeed = Mathf.Abs(rb2d.velocity.x);
        animator.SetFloat("knightSpeed", knightSpeed);
        groundedRight = Physics2D.Linecast(transform.position +vec, transform.position +vec - groundCheck, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(transform.position + vec, transform.position + vec - groundCheck);
        groundedLeft = Physics2D.Linecast(transform.position - vec, transform.position - vec - groundCheck, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(transform.position - vec, transform.position - vec - groundCheck);
        grounded = groundedLeft || groundedRight;
        // Debug.Log("Danach: " + grounded);
        animator.SetBool("grounded", grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Vector2 v = new Vector2(rb2d.velocity.x, 0);
            rb2d.velocity = v;
            jump = true;

        }
        if (Input.GetButtonDown("OP1") && Input.GetButtonDown("OP2"))
        {
            SceneManager.LoadScene("Sieg");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menü");
        }
        if (grounded)
        {
            rb2d.gravityScale = 1;
        }
        if (hasShield == true && Input.GetButton("Shield") && hitting == false)
        {
            armed = true;
            shieldpresst = true;
            shield.SetActive(true);
        }
        if (shieldpresst == true && !Input.GetButton("Shield"))
        {
            armed = false;
            shieldpresst = false;
            shield.SetActive(false);
        }
        if (Input.GetButtonDown("Sword") && shieldpresst == false && hitting == false&& hasSword)
        {

            StartCoroutine(swordBlow());
            
        }
        if (health <= 0)
        {
            andStayDead();
            
        }
        
    }
    public  void andStayDead()
    {
        death = true;
        animator.SetBool("death", death);
        StartCoroutine(deathdelay());
    }
    IEnumerator swordBlow()
    {
        armed = true;
        sword.SetActive(true);
        hitting = true;
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        transform.GetComponent<SpriteRenderer>().enabled = false;
        anim.SetBool("hitting", hitting);      
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(false);
        hitting = false;
        armed = false;
        anim.SetBool("hitting", hitting);
        yield return new WaitForSeconds(0.25f);
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator invincible()
    {
        st = state.invincible;
        yield return new WaitForSeconds(invinTime);
        st = state.normal;
    }

    IEnumerator deathdelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Endscreen");
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Enemy"&& st ==state.normal)
        {
            Enemy enemy = (Enemy)col.gameObject.GetComponent("Enemy");
           
            if (!hitShield(enemy))
            {
                health--;
                cam.gameObject.GetComponent<Heartskript>().setHearts(health);
                hitAway(enemy);
                StartCoroutine(invincible());
                
            }
        }
    }
    void hitAway(Enemy enemy)
    {
        float div =  enemy.transform.position.x-transform.position.x;
        if (div < 0)
        {
            rb2d.AddForce(Vector2.right * 1 * wasHitForce);
        }
        else
        {
            rb2d.AddForce(Vector2.right * (-1) * wasHitForce);
        }
    }
    bool hitShield(Enemy enemy) // or Sword
    {
        
        float div =  enemy.transform.position.x-transform.position.x;   
        // div neg => gegner links vom Spieler; div pos => Gegner rechts vom Spieler
        if ((facingRight && div < 0)||(!facingRight&&div>0))
        {
            // Gegner von hinten
            return false;
        }
        if (armed && enemy.transform.position.y + enemy.transform.localScale.y * 0.8f >= transform.position.y && enemy.transform.position.y - enemy.transform.localScale.y * 0.8f <= transform.position.y)
        {
            // Gegner in der richtigen Richtung aber mit Schwert
            return true;
        }
        // Gegner von vorne ohne Schwert.
        return false;

    }
    void FixedUpdate()
    {
        
        float h = Input.GetAxis("Horizontal");

        // Schneller fallen
        if (transform.position.y < height)
        {
            rb2d.gravityScale *= gravityPlus;
        }
        height = transform.position.y;

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);
        //maximale Geschwindigkeit
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        // Flipper
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        // Jump
        if (jump)
        {

            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
