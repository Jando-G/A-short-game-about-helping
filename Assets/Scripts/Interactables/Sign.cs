using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Sign : MonoBehaviour
{
     public TextMeshProUGUI text;
     AudioSource audioSource;
     public AudioClip open;
     public AudioClip close;

     private void Start()
     {
          text.enabled = false; //cuz it has to be enabled first for this to work
          audioSource = GetComponent<AudioSource>();
     }
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player"))
          {
               text.enabled = true;
               audioSource.PlayOneShot(open, 0.5F);
          }
     }
     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player"))
          {
               text.enabled = false;
               audioSource.PlayOneShot(close, 0.6F);
          }
     }
}
