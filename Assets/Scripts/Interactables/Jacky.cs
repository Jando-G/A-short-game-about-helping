using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Jacky : MonoBehaviour
{
     public TextMeshProUGUI text;

     public int gameState = 0;

     public void UpdateJackyState(int newState)
     {
          gameState = newState;
          UpdateDialogue();
     }
     private void UpdateDialogue()
     {
          switch (gameState)
          {
               case 0:
                    // Oh thank god! I've been stuck here for days. Can you help me out? Just break the bricks that I couldn't reach
                    break;
               case 1:
                    // It must be the other bricks. break the other bricks.
                    break;
               case 2:
                    // Oh dang... Ummm. I- ummm uhh.
                    break;
          }
     }

}
