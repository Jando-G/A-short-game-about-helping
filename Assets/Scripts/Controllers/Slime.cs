using Controls;
using UnityEngine;

public class Slime : MonoBehaviour
{
     [SerializeField] float movementSpeed = 2f;
     [SerializeField] float jumpHeightMax = 6f;
     [SerializeField] float jumpTimerMax = 3f;
     private float direction = 1f;
     private int desiredJump = 0;

     private float jumpTimer = 0;

     private float wallBuffer = 0;

     AudioSource audioSource;
     public AudioClip jumpSound;

     private CollisionDataReciever collisionDataReciever;
     private Rigidbody2D body;

     private void Start()
     {
          body = GetComponent<Rigidbody2D>();
          collisionDataReciever = GetComponent<CollisionDataReciever>();
          audioSource = GetComponent<AudioSource>();
     }
     void FixedUpdate()
     {
          bool onWall = collisionDataReciever.OnWall;
          Vector2 currentVelocity = new Vector2(0, 0);
          if (onWall && wallBuffer < 0)
          {
               direction *= -1;
               wallBuffer = 3f;
               FlipSprite();
          }
          else
          {
               wallBuffer -= Time.deltaTime;
          }
          if (collisionDataReciever.OnGround)
          {
               if (jumpTimer < 0)
               {
                    audioSource.PlayOneShot(jumpSound, 0.3f);
                    currentVelocity.y = Random.Range(0f, jumpHeightMax);
                    jumpTimer = Random.Range(1f, jumpTimer);
               }
               else
               {
                    jumpTimer -= Time.deltaTime;
                    desiredJump = 0;
               }
          }
          else if (collisionDataReciever.OnWall)
          {
               currentVelocity.y = -2f;
          }
          currentVelocity.x = movementSpeed * direction;
          body.velocity = currentVelocity;
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
}
