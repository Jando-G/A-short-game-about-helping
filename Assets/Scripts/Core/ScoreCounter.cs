using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
     public TextMeshProUGUI scoreText;
     private float score;

     public void IncreaseScore(float amount)
     {
          score += amount;
          scoreText.text = "Score: " + score.ToString();
     }
}
