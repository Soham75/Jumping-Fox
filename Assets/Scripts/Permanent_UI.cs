using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Permanent_UI : MonoBehaviour
{
     public int Cherries;
     public int Health;
     public TextMeshProUGUI CText;
     public TextMeshProUGUI Health_Amount;

    public static Permanent_UI PUI;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!PUI)
        {
            PUI = this;
        }
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        Cherries = 0;
        Health -= 1;
        CText.text = Cherries.ToString();
        Health_Amount.text = Health.ToString();
    }
}
