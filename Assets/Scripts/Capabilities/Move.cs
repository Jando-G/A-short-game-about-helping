using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
     [SerializeField] private InputController input = null;
     [SerializeField] private Animator animator;
     [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
     [SerializeField, Range(0f, 100f)] private float maxAccerleration = 35f;
     [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

     private Vector2 direction;
     private Vector2 desiredVelocity;
     private Vector2 velocity;
     private Rigidbody2D body;
     private Ground ground;

     private float maxSpeedChange;
     private float acceleration;
     private bool onGround;

     private void Awake()
     {
          body = GetComponent<Rigidbody2D>();
          ground = GetComponent<Ground>();
     }
     private void Update()
     {
          direction.x = input.RetrieveMoveInput();
          desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
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
          onGround = ground.GetOnGround();
          velocity = body.velocity;

          acceleration = onGround ? maxAccerleration : maxAirAcceleration;
          maxSpeedChange = acceleration * Time.deltaTime;
          velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
          
          body.velocity = velocity;
     }
}
