using UnityEngine;

public class TextFloatIn : MonoBehaviour
{
     public RectTransform targetRectTransform;
     public Vector2 targetPosition;
     public float duration = 1.0f;

     private float elapsedTime = 0.0f;

     void Start()
     {
          targetRectTransform.anchoredPosition = new Vector2(targetPosition.x, -targetRectTransform.rect.height);
     }

     void Update()
     {
          if (elapsedTime < duration)
          {
               elapsedTime += Time.deltaTime;
               float t = Mathf.Clamp01(elapsedTime / duration);
               targetRectTransform.anchoredPosition = Vector2.Lerp(new Vector2(targetPosition.x, -targetRectTransform.rect.height), targetPosition, t);
          }
     }
}