using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
     [SerializeField] Credits credits;

     private void OnTriggerEnter2D(Collider2D collision)
     {
          credits.TriggerCredits();
     }
}
