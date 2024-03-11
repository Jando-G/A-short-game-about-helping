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


     AudioSource audioSource;
     public AudioClip jumpSound;

     private CollisionDataReciever collisionDataReciever;
     private Rigidbody2D body;

     private void Start()
     {
          body = GetComponent<Rigidbody2D>();
          collisionDataReciever = GetComponent<CollisionDataReciever>();
          audioSource = GetComponent<AudioSource>();
          StartCoroutine(RandomJump());
     }
     private void Update()
     {
          if (collisionDataReciever.OnWall) {
               direction *= -1f;
               FlipSprite();
          }
          velocity = body.velocity;
          velocity.x = direction * movementSpeed;
          body.velocity = velocity;
     }
     private void FlipSprite()
     {
          if (direction > 0)
          {
               transform.localScale = new Vector3(1, 1, 1);
          }
          else if (direction < 0)
          {
               transform.localScale = new Vector3(-1, 1, 1);
          }
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
}
