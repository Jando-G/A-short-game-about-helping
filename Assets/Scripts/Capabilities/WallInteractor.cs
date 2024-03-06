using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
     [RequireComponent(typeof(Controller))]
     public class WallInteractor : MonoBehaviour
     {
          public bool WallJumping { get; private set; }

          [Header("Wall Slide")]
          [SerializeField][Range(0.1f, 5f)] private float wallSlideMaxSpeed = 2f;
          [Header("Wall Jump")]
          [SerializeField] private Vector2 wallJumpClimb = new Vector2(4f, 12f);
          [SerializeField] private Vector2 wallJumpBounce = new Vector2(10.7f, 10f);
          [SerializeField] private Vector2 wallJumpLeap = new Vector2(14f, 12f);

          private CollisionDataReciever collisionDataReciever;
          private Rigidbody2D body;
          private Controller controler;

          private Vector2 velocity;

          private float wallDirectionX;

          private bool onWall, onGround, desiredJump;


          private void Awake()
          {
               collisionDataReciever = GetComponent<CollisionDataReciever>();
               body = GetComponent<Rigidbody2D>();
               controler  = GetComponent<Controller>();
          }

          private void Update()
          {
               if (onWall && !onGround)
               {
                    desiredJump |= controler.input.RetrieveJumpInput();
               }
          }

          private void FixedUpdate()
          {
               velocity = body.velocity;
               onWall = collisionDataReciever.OnWall;
               onGround = collisionDataReciever.OnGround;
               wallDirectionX = collisionDataReciever.ContactNormal.x;

               //wallslide
               if(onWall)
               {
                    if(velocity.y < -wallSlideMaxSpeed)
                    {
                         velocity.y = -wallSlideMaxSpeed;
                    }
               }

               //walljump
               if ((onWall && velocity.x == 0) || onGround)
               {
                    WallJumping = false;
               }
               if(desiredJump)
               {
                    if(-wallDirectionX == controler.input.RetrieveMoveInput())
                    {
                         velocity = new Vector2(wallJumpClimb.x * wallDirectionX, wallJumpClimb.y);
                         WallJumping = true;
                         desiredJump = false;
                    }
                    else if (controler.input.RetrieveMoveInput() == 0)
                    {
                         velocity = new Vector2(wallJumpBounce.x * wallDirectionX, wallJumpBounce.y);
                         WallJumping = true;
                         desiredJump = false;
                    }
                    else
                    {
                         velocity = new Vector2(wallJumpLeap.x * wallDirectionX, wallJumpLeap.y);
                         WallJumping = true;
                         desiredJump = false;
                    }
               }
               body.velocity = velocity;
          }
          private void OnCollisionEnter2D(Collision2D collision)
          {
               collisionDataReciever.EvaluateCollision(collision);
               if (collisionDataReciever.OnWall && !collisionDataReciever.OnGround && WallJumping)
               {
                    body.velocity = Vector2.zero;
               }
          }
     }
}

