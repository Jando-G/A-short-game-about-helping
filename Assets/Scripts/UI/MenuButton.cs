using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuButton : MonoBehaviour
{
     private Vector2 newScale = Vector2.one;
     public float buttonScale = 1.1f;
     public float speed = 1f;
     public void PointerEnter()
     {
          newScale = new Vector2(buttonScale, buttonScale);
         
     }
     public void PointerExit()
     {
          newScale = Vector2.one;
     }
     private void Update()
     {
          transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed * Time.deltaTime);
     }
}