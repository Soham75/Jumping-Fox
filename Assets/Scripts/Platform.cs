using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector2 P1, P2;
    [SerializeField] private float Platform_Speed;
    
    private void Update()
    {
        Platform_movement();
    }
   private void Platform_movement()
    {
        transform.position = Vector2.Lerp(P1,P2, Mathf.PingPong(Time.time * Platform_Speed, 1.0f));
    }

}
