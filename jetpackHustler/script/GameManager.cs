using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public Image jetpackBar;
    public int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddMoneyScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.SetText(score.ToString());
    }
}
