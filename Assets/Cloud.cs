using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
     private float speed;
     public bool isFront = true;

     private void Start()
     {
          if(isFront)
          {
               speed = Random.Range(.01f, .02f);
          }
          else
          {
               speed = Random.Range(.003f, .006f);
          }
       
     }
     private void FixedUpdate()
     {
          if(transform.position.x < -20f)
          {
               transform.position = new Vector2(200f, transform.position.y);
          }
          else
          {
               transform.position = new Vector2(transform.position.x - speed, transform.position.y);
          }
     }
}
