using Controls;
using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
     [SerializeField] float movementSpeed = 2f;
     [SerializeField] float jumpHeightMax = 6f;
     [SerializeField] float jumpTimerMin = 3f;
     [SerializeField] float jumpTimerMax = 10f;
     private float direction = 1f;
     private Vector2 velocity;

     public AudioSource audioSource;
     public AudioSource cryingSource;
     public AudioClip jumpSound;

     public float minDist = 1;
     public float maxDist = 400;

     private CollisionDataReciever collisionDataReciever;
     private Rigidbody2D body;

     private GameObject player;

     bool megaSlime = false;

     private void Start()
     {
          body = GetComponent<Rigidbody2D>();
          collisionDataReciever = GetComponent<CollisionDataReciever>();
          StartCoroutine(RandomJump());
          player = GameObject.FindGameObjectWithTag("Player");
     }
     private void Update()
     {
          float dist = Vector2.Distance(transform.position, player.transform.position);
          if (dist < minDist)
          {
               audioSource.volume = .4f;
          }
          else if (dist > maxDist)
          {
               audioSource.volume = 0;
          }
          else
          {
               audioSource.volume = .4f - (((dist - minDist) / (maxDist - minDist)) / 2.5f);
          }
          if (collisionDataReciever.OnWall) {
               direction *= -1f;
               FlipSprite();
          }
          velocity = body.velocity;
          velocity.x = direction * movementSpeed;
          body.velocity = velocity;
     }

     void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.gameObject.CompareTag("Player") && !megaSlime)
          {
               transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1f);
               cryingSource.Play();
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.gameObject.CompareTag("Player") && !megaSlime)
          {
               transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
               cryingSource.Pause();
          }
     }
     private void FlipSprite()
     {
         transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 1f);
     }
     private IEnumerator RandomJump()
     {
          while (true)
          {
               yield return new WaitForSeconds(Random.Range(jumpTimerMin, jumpTimerMax));
               audioSource.PlayOneShot(jumpSound, 0.2f);
               body.AddForce(Vector2.up * Mathf.Sqrt(2f * Mathf.Abs(Physics2D.gravity.y) * jumpHeightMax), ForceMode2D.Impulse);
          }
     }
     public void SetMega()
     {
          megaSlime = true;
          jumpHeightMax *= 2;
          jumpHeightMax *= 2;
     }
}
