using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Characters
{
    private enum State { idle, jumping, falling };
    private State state = State.idle;
    [SerializeField] private float JumpForce, JumpLength, LWP, RWP;
    [SerializeField] private LayerMask Ground;
    private bool FacingLeft = true;
    
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        AnimationState();
        anim.SetInteger("state", (int)state);
        
    }
    private void E_Movement()
    {
        if (FacingLeft)
        {
            if (transform.position.x > LWP)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (Col.IsTouchingLayers(Ground))
                {
                    Enemy.velocity = new Vector2(-JumpLength, JumpForce);
                    state = State.jumping;
                }

            }
            else
            {
                FacingLeft = false;
                state = State.idle;
            }
        }
        else
        {
            if (transform.position.x < RWP)
            {
                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (Col.IsTouchingLayers(Ground))
                {
                    Enemy.velocity = new Vector2(+JumpLength, JumpForce);
                    state = State.jumping;
                }

            }
            else
            {
                FacingLeft = true;
                state = State.idle;
            }
        }
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (Enemy.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (Col.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }
    }

    
}   
