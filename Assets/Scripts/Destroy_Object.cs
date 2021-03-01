using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Object : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(destroy_object());
    }
    public IEnumerator destroy_object()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
