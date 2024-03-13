using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawner : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other)
     {
         other.gameObject.transform.position = Vector2.zero;
         if(other.CompareTag("Slime"))
          {
               other.gameObject.transform.localScale = new Vector2(other.gameObject.transform.localScale.x * 2, other.gameObject.transform.localScale.y * 2);
               other.GetComponent<AudioSource>().pitch = 0.5f;
               other.GetComponent<Slime>().SetMega();
          }
     }
}
