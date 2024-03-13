using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
     public TextMeshProUGUI[] textElements;
     public float fadeInDuration = 1.0f; 
     public float fadeOutDuration = 1.0f; 
     public float pauseDuration = 1.0f;

     IEnumerator FadeTexts()
     {
          for (int i = 0; i < textElements.Length; i++)
          {
               yield return FadeIn(textElements[i]);

               if (i + 1 < textElements.Length)
               {
                    yield return new WaitForSeconds(pauseDuration);
                    yield return FadeIn(textElements[i + 1]);
               }

               yield return new WaitForSeconds(pauseDuration);

        
               if (i + 1 < textElements.Length)
               {
                    yield return FadeOut(textElements[i]);
                    yield return FadeOut(textElements[i + 1]);
               }
                   

               if (i + 1 < textElements.Length)
                    i++;
               else {
                    yield return new WaitForSeconds(2f);
                    yield return FadeOut(textElements[i]);
                    SaveMe[] dontDestroyObjects = FindObjectsByType<SaveMe>(FindObjectsSortMode.None);
                    foreach (SaveMe saveMe in dontDestroyObjects)
                    {
                         Destroy(saveMe.gameObject);
                    }
                    SceneManager.LoadScene(0);
               }
          }
     }

     IEnumerator FadeIn(TextMeshProUGUI textElement)
     {
          textElement.alpha = 0f;
          float elapsedTime = 0f;
          while (elapsedTime < fadeInDuration)
          {
               textElement.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
               elapsedTime += Time.deltaTime;
               yield return null;
          }
          textElement.alpha = 1f;
     }

     IEnumerator FadeOut(TextMeshProUGUI textElement)
     {
          textElement.alpha = 1f;

          float elapsedTime = 0f;
          while (elapsedTime < fadeOutDuration)
          {
               textElement.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
               elapsedTime += Time.deltaTime;
               yield return null;
          }
          textElement.alpha = 0f; 
     }

     internal void TriggerCredits()
     {
          StartCoroutine(FadeTexts());
     }
}
