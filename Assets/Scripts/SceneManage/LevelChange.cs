using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
     [SerializeField, Range(0,1)] int sceneNum = 0;
  
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.gameObject.CompareTag("Player"))
          {
               SceneManager.LoadScene(sceneNum);
          }
     }
}
