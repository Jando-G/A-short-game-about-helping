using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
     [SerializeField] TextMeshProUGUI levelText;
     private void Start()
     {
          StartCoroutine(FadeInAndOut());
     }

     private IEnumerator FadeInAndOut()
     {
          levelText.color = new Color(1f, 1f, 1f, 0);
          while (levelText.color.a < 1.0f)
          {
               levelText.color = new Color(1f, 1f, 1f, levelText.color.a + (Time.deltaTime / 4));
               yield return null;
          }
          yield return new WaitForSeconds(2f);
          while (levelText.color.a > 0)
          {
               levelText.color = new Color(1f, 1f, 1f, levelText.color.a - (Time.deltaTime / 2));
               yield return null;

          }
     }
}
