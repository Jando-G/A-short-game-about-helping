using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Controls
{
     [RequireComponent(typeof(Controller))]
     public class Stomp : MonoBehaviour
     {
          private Controller controller;
          private Rigidbody2D body;
          private CollisionDataReciever collisionData;
          private Tilemap tilemap;
          private ScoreCounter scoreCounter;
          private AudioSource audioSource;

          [SerializeField] TileBase brickTile;
          [SerializeField] TileBase specialBrick;
          [SerializeField] TileBase solidBrick;

          [SerializeField] private GameObject breakEffect;
          [SerializeField] private float stompVelocity = 8f;
          [SerializeField] private float pitchStep = 0.1f;
          [SerializeField] private float pitchResetTime = 1f;
          [SerializeField] private AudioClip crush;
          [SerializeField] private AudioClip bonk;

          private bool onGround;
          private bool isStomping;
          private float pitchTimer;

          private void Awake()
          {
               body = GetComponent<Rigidbody2D>();
               collisionData = GetComponent<CollisionDataReciever>();
               controller = GetComponent<Controller>();
               scoreCounter = GetComponent<ScoreCounter>();
               audioSource = GetComponent<AudioSource>();
               tilemap = FindFirstObjectByType<Tilemap>();
          }
          private void Start()
          {
               SceneManager.sceneLoaded += OnSceneLoaded;
          }

          private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
          {
               tilemap = FindFirstObjectByType<Tilemap>();
               transform.position = Vector3.zero;
               
          }
          
          private void Update()
          {
               Vector3Int playerPos = tilemap.WorldToCell(transform.position);
               Vector3Int tileBelow = new Vector3Int(playerPos.x, playerPos.y - 1, playerPos.z);
               Vector3Int tileBelowLeft = new Vector3Int(playerPos.x - 1, playerPos.y - 1, playerPos.z);
               Vector3Int tileBelowRight = new Vector3Int(playerPos.x + 1, playerPos.y - 1, playerPos.z);
               Vector3Int tileAbove = new Vector3Int(playerPos.x, playerPos.y + 1, playerPos.z);

               if (onGround)
               {
                    if (isStomping)
                    {
                         if (tilemap.GetTile(tileBelow) == brickTile || tilemap.GetTile(tileBelowLeft) == brickTile || tilemap.GetTile(tileBelowRight) == brickTile || tilemap.GetTile(tileBelow) == specialBrick)
                         {
                              if (tilemap.GetTile(tileBelow) == brickTile)
                                   BreakBrick(tileBelow);
                              else if (tilemap.GetTile(tileBelow) == specialBrick)
                              {
                                   tilemap.SetTile(tileBelow, solidBrick);
                                   audioSource.PlayOneShot(bonk, 0.5F);
                              }
                                 

                              if (tilemap.GetTile(tileBelowLeft) == brickTile)
                                   BreakBrick(tileBelowLeft);
                              if (tilemap.GetTile(tileBelowRight) == brickTile)
                                   BreakBrick(tileBelowRight);

                              isStomping = true;
                         }
                         else
                         {
                              isStomping = false;
                         }
                    }
               }
               else
               {
                    if(controller.input.RetrieveStompInput())
                    {
                         isStomping = true;
                    }
                    if (tilemap.GetTile(tileAbove) == brickTile)
                    {
                         BreakBrick(tileAbove);
                    }
                    else if (tilemap.GetTile(tileAbove) == specialBrick)
                    {
                         tilemap.SetTile(tileAbove, solidBrick);
                         audioSource.PlayOneShot(bonk, 0.5F);
                    }
               }

               pitchTimer -= Time.deltaTime;

               if (pitchTimer < 0)
               {
                    audioSource.pitch = 1;
               }
          }

          private void BreakBrick(Vector3Int tile)
          {
               tilemap.SetTile(tile, null);
               GameObject particle = Instantiate(breakEffect, transform.position , Quaternion.identity);
               Destroy(particle, 2);
               scoreCounter.IncreaseScore(1);
               audioSource.PlayOneShot(crush, 0.5F);
               audioSource.pitch += pitchStep;
               pitchTimer = pitchResetTime;
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

          private void OnDestroy()
          {
               SceneManager.sceneLoaded -= OnSceneLoaded;
          }
     }
}

