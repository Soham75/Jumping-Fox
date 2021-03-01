using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Controls : MonoBehaviour
{
    private Rigidbody2D Player;
    private Animator anim;
    private enum State {idle, running, jumping, falling, hurt, climb};
    private State state = State.idle;
    private Collider2D Col;

    //Ladder Stuff
    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool top_L = false;
    [HideInInspector] public bool bottom_L = false;
    public Ladder ladder;
    [SerializeField] private float ClimbSpeed = 3.0f;
    //
        
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float Pushback = 5f;
    [SerializeField] private AudioSource Sound_FootSteps;
    [SerializeField] private AudioSource Sound_Cherries;
    [SerializeField] private AudioSource Sound_Jumping;
    [SerializeField] private AudioSource Sound_Gem;


    private void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Col = GetComponent<Collider2D>();
        Sound_FootSteps = GetComponent<AudioSource>();
        Permanent_UI.PUI.Health_Amount.text = Permanent_UI.PUI.Health.ToString();
        
    }

    private void Update()
    {
        if(state == State.climb)
        {
            Climb();
        }
        else if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Sound_Cherries.Play();
            Destroy(collision.gameObject);
            Permanent_UI.PUI.Cherries += 1;
            Permanent_UI.PUI.CText.text = Permanent_UI.PUI.Cherries.ToString();
        }
        if(collision.tag == "PowerUp")
        {
            Sound_Gem.Play();
            Destroy(collision.gameObject);
            JumpForce += 5f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Characters enemy = collision.gameObject.GetComponent<Characters>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                Handle_health();
                if (collision.gameObject.transform.position.x > transform.position.x) // Enemy pos greater than player
                {
                    Player.velocity = new Vector2(-Pushback, Player.velocity.y);
                }
                else  //Player pos > Enemy
                {
                    Player.velocity = new Vector2(+Pushback, Player.velocity.y);
                }
            }
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = collision.transform;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
    }

    private void Handle_health()
    {
        Permanent_UI.PUI.Health -= 1;
        Permanent_UI.PUI.Health_Amount.text = Permanent_UI.PUI.Health.ToString();
        if (Permanent_UI.PUI.Health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if(canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            state = State.climb;
            Player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, Player.position.y);
            Player.gravityScale = 0f;
        }
        if (hDirection < 0)
        {
            Player.velocity = new Vector2(-speed, Player.velocity.y);
            Player.transform.localScale = new Vector2(-1, 1);

        }
        else if (hDirection > 0)
        {
            Player.velocity = new Vector2(+speed, Player.velocity.y);
            Player.transform.localScale = new Vector2(1, 1);

        }
        else
        {

        }
        if (Input.GetButtonDown("Jump") && Col.IsTouchingLayers(Ground))
        {
            Jump();
            Sound_Jumping.Play();
        }
    }
    private void Jump()
    {
        Player.velocity = new Vector2(Player.velocity.x, +JumpForce);
        state = State.jumping;
    }
    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Player.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            Player.gravityScale = 1f;
            anim.speed = 1f;
            Jump();
            Sound_Jumping.Play();
            return;
        
        }
        float vDirection = Input.GetAxis("Vertical");
        if(vDirection > .1f && !top_L) //climb up
        {
            Player.velocity = new Vector2(0f, vDirection * ClimbSpeed);
            anim.speed = 1f;
        }
        else if(vDirection < -.1f && !bottom_L ) //climb down
        {
            Player.velocity = new Vector2(0f, vDirection * ClimbSpeed);
            anim.speed = 1f;
        }
        else
        {
            anim.speed = 0f;
            Player.velocity = Vector2.zero;
        }
    }

    private void AnimationState()
    {

       if(state == State.climb)
        {

        }
       else if(state == State.jumping)
        {
            if(Player.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        } 
       else if(state == State.falling)
        {
            if (Col.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }
       else if(state == State.hurt)
        {
            if(Mathf.Abs(Player.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
       else if(Mathf.Abs(Player.velocity.x) > 2f)
        {
            state = State.running;
        }
       else
        {
            state = State.idle;
        }
    }

    private void Sound()
    {
        Sound_FootSteps.Play();
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(15);
        JumpForce -= 5f;
        GetComponent<SpriteRenderer>().color = Color.white;
    } 

}
