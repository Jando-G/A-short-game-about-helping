using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Controls
{
     [RequireComponent(typeof(Controller))]
     public class Move : MonoBehaviour
     {
          [SerializeField] private Animator animator;
          [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
          [SerializeField, Range(0f, 100f)] private float maxAccerleration = 35f;
          [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
          [SerializeField, Range(0.05f, 10f)] private float wallStickTime = 0.25f;

          private Controller controller; 
          private Vector2 direction;
          private Vector2 desiredVelocity;
          private Vector2 velocity;
          private Rigidbody2D body;
          private CollisionDataReciever collisionData;
          private WallInteractor wallInteractor;

          private float maxSpeedChange, acceleration, wallStickCounter;
          private bool onGround;

          private void Awake()
          {
               body = GetComponent<Rigidbody2D>();
               collisionData = GetComponent<CollisionDataReciever>();
               controller = GetComponent<Controller>();
               wallInteractor = GetComponent<WallInteractor>();
          }
          private void Update()
          {
               direction.x = controller.input.RetrieveMoveInput();
               desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - collisionData.GetFriction(), 0f);

               FlipSprite();
               animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
               animator.SetFloat("VertSpeed", body.velocity.y);
          }

          private void FlipSprite()
          {
               if (direction.x > 0)
               {
                    transform.localScale = new Vector3(1, 1, 1);
               }
               else if (direction.x < 0)
               {
                    transform.localScale = new Vector3(-1, 1, 1);
               }
          }

          private void FixedUpdate()
          {
               onGround = collisionData.GetOnGround();
               velocity = body.velocity;

               acceleration = onGround ? maxAccerleration : maxAirAcceleration;
               maxSpeedChange = acceleration * Time.deltaTime;
               velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
               //wall stick
               if (collisionData.OnWall && !collisionData.OnGround && !wallInteractor.WallJumping)
               {
                    velocity.x =  0;
                    if(wallStickCounter > 0)
                    {
                         velocity.x = 0;

                         if(controller.input.RetrieveMoveInput() == collisionData.ContactNormal.x)
                         {
                              wallStickCounter -= Time.deltaTime;
                         }
                         else
                         {
                              wallStickCounter = wallStickTime;
                         }
                    }
                    else
                    {
                         wallStickCounter = wallStickTime;
                    }
               }
               body.velocity = velocity;
          }
     }

}
