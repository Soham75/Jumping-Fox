using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Objects : MonoBehaviour
{
    public GameObject PowerUp_TextPrefab;
    public float Destroy_Time = 3f;
    public void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Floating_Text();
        }
    }
    public void Floating_Text()
    {
        Instantiate(PowerUp_TextPrefab, transform.position, Quaternion.identity);
    }
}
