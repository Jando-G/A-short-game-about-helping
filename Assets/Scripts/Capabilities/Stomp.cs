using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controls
{
     [RequireComponent(typeof(Controller))]
     public class Stomp : MonoBehaviour
     {
          private Controller controller;
          private Rigidbody2D body;
          private CollisionDataReciever collisionData;
          public Tilemap tilemap;
          public TileBase brickTile;
          public GameObject breakEffect;
          private ScoreCounter scoreCounter;

          [SerializeField] private float stompVelocity = 8f;

          private bool onGround, isStomping;

          private void Awake()
          {
               body = GetComponent<Rigidbody2D>();
               collisionData = GetComponent<CollisionDataReciever>();
               controller = GetComponent<Controller>();
               scoreCounter = GetComponent<ScoreCounter>();
          }
          private void Update()
          {
               if (onGround)
               {
                    if(isStomping)
                    {
                         Vector3Int playerPos = tilemap.WorldToCell(transform.position);
                         Vector3Int tileBelow = new Vector3Int(playerPos.x, playerPos.y - 1, playerPos.z);

                         if (tilemap.GetTile(tileBelow) == brickTile)
                         {
                              tilemap.SetTile(tileBelow, null);
                              GameObject particle = Instantiate(breakEffect, transform.position, Quaternion.identity);
                              Destroy(particle, 2);
                              scoreCounter.IncreaseScore(1);
                              isStomping = true;
                         }
                         else
                         {
                              isStomping = false;
                         }
                    }
               }
               else if(controller.input.RetrieveStompInput() == true)
               {
                    isStomping = true;
               }
          }

          private void FixedUpdate()
          {
               onGround = collisionData.GetOnGround();
               if (!onGround && isStomping)
               {
                    StompAction();
               }
          }
          private void StompAction()
          {
               body.velocity = new Vector2(0f, -stompVelocity);
          }
     }
}

