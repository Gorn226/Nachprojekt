using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Charactercontroller : MonoBehaviour
{
    enum state { normal, invincible};
    state st = state.normal;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 100f;
    public Vector3 groundCheck;
    public int health = 3;
    public float invinTime =0.5f;

    public bool hasShield = true;
    public GameObject shield;
    bool shieldpresst = false;

    public GameObject sword;
    bool hitting = false;

    private bool grounded = false;
    Rigidbody2D rb2d;
    public float height;
    public float gravityPlus =0f;
    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;

    }
    void Start()
    {
        groundCheck = new Vector3(0, transform.localScale.y * 0.5f + 0.1f, 0);
        shield.SetActive(false);
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

      

        grounded = Physics2D.Linecast(transform.position, transform.position - groundCheck, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;

        }
        if (grounded)
        {
            rb2d.gravityScale = 1;
        }
        if (hasShield == true && Input.GetButton("Shield") && hitting == false)
        {
            shieldpresst = true;
            shield.SetActive(true);
        }
        if (shieldpresst == true && !Input.GetButton("Shield"))
        {
            shieldpresst = false;
            shield.SetActive(false);
        }
        if (Input.GetButtonDown("Sword") && shieldpresst == false && hitting == false)
        {

            StartCoroutine(swordBlow());
        }
        if (health <= 0)
        {
            Application.LoadLevel("Endscreen");
        }
        
    }
    IEnumerator swordBlow()
    {
        sword.SetActive(true);
        hitting = true;
        yield return new WaitForSeconds(1.5f);
        sword.SetActive(false);
        hitting = false;
    }
    IEnumerator invincible()
    {
        st = state.invincible;
        yield return new WaitForSeconds(invinTime);
        st = state.normal;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy"&& st ==state.normal)
        {
            health--;
            StartCoroutine(invincible());
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (transform.position.y < height)
        {
            rb2d.gravityScale *= gravityPlus;

        }
        height = transform.position.y;

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

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
