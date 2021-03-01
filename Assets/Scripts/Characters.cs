using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D Enemy;
    protected Collider2D Col;
    protected AudioSource sound;
    protected virtual void Start()
    {
        Enemy = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        sound.Play();
        Enemy.velocity = Vector2.zero;
        Enemy.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }
}
