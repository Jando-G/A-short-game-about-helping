using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class JackyDialogue : MonoBehaviour
{
     [SerializeField] Tilemap tilemap;
     [SerializeField] TileBase specialBrick;
     [SerializeField] TextMeshPro dialogueText;
     [SerializeField] TextMeshProUGUI signText;

     private AudioSource BGM;
     Animator animator;

     public float textSpeed = 0.1f;

     private string[] introDialogue = {
        "Oh my gosh.. A person! Thank the good lord above :)",
        "I've been stuck here for ages!",
        "Can you please break these bricks above me?",
        "I can't reach them myself.. :\\",
        "I really thought I was going to die in here :(",
        "luckily a hero like you came to save me :)",
        "go ahead and stomp on those bricks",
        "...",
        "you can break them now",
        "...",
        "....",
        "erm.. are you going to break the bricks??",
        "...",
        "....",
        ".....",
        "It's alright you can take your time :)",
        "...",
        "....",
        ".....",
        "......",
        "You're not going to help? :(",
    };

     private string[] firstBlockDialogue =
       {
           "...Oh, try breaking the otherside!",
           "Yea, the otherside has to work!",
     };

     private string[] secondBlockDialogue =
     {
           "...",
           "Oh.. I see...",
           "well, thanks for trying i guess..."
     };

     private int state = 0;

     private void Start()
     {
          animator = GetComponent<Animator>();
          StartCoroutine(StartDialogue(introDialogue));
          GameObject musicObject = GameObject.FindWithTag("BGM");
          BGM = musicObject.GetComponent<AudioSource>();
     }

     private IEnumerator StartDialogue(string[] dialogue)
     {
          foreach (string line in dialogue)
          {
               dialogueText.text = "";
               foreach (char c in line)
               {
                    dialogueText.text += c;
                    yield return new WaitForSeconds(textSpeed);
               }
               yield return new WaitForSeconds(2f);
          }
     }

     private void Update()
     {
          switch (state) {
               case 0:
                    if (CountSpecialBricks() == 7)
                    {
                         StopAllCoroutines();
                         textSpeed = textSpeed / 2;
                         StartCoroutine(StartDialogue(firstBlockDialogue));
                         state += 1;
                    }
                    break;
               case 1:
                    if (CountSpecialBricks() == 6)
                    {
                         BGM.Pause();
                         StopAllCoroutines();
                         textSpeed *= 2;
                         animator.speed = 0f;
                         StartCoroutine(StartDialogue(secondBlockDialogue));
                         state += 1;
                    }
                    break;
               default:
                    break;
          }
     }
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player") && state == 2)
          {
               StopAllCoroutines();
               dialogueText.text = "";
               transform.Rotate(0f, 0f, 90f);
               transform.position = new Vector3(126.7f, -53f, 0f);
               BGM.Play();
               signText.text = "You tried your best.";
               state += 1;
          }
     }
     private int CountSpecialBricks()
     {
          int count = 0;
          BoundsInt bounds = tilemap.cellBounds;
          TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

          foreach (var tile in allTiles)
          {
               if (tile == specialBrick)
               {
                    count++;
               }
          }
          return count;
     }
}