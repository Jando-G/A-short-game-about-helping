using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
     public TextMeshProUGUI text;

     private void Start()
     {
          text.enabled = false; //cuz it has to be enabled first for this to work
     }
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player"))
          {
               text.enabled = true;
          }
     }
     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player"))
          {
               text.enabled = false; 
          }
     }
}
