using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private MenuManager menuManager;
    private BallMovement ballScript;
    private PlayerControll playerControll;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    private int player1Score = 0;
    private int player2Score = 0;

    private void Start()
    {
        player1ScoreText.text = "0";
        player2ScoreText.text = "0";

        menuManager = FindAnyObjectByType<MenuManager>();
        ballScript = FindAnyObjectByType<BallMovement>();
        playerControll = FindAnyObjectByType<PlayerControll>();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScoreToPlayer(int playerNumber)
    {
        if(playerNumber == 1)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        player1ScoreText.text = player1Score.ToString();

        player2ScoreText.text = player2Score.ToString();
    }

    private void CountScore()
    {
        if(player1Score == 10 && player2Score < player1Score)
        {
            menuManager.winner1.SetActive(true);
            ResetGame();
        }
        else if(player2Score == 10 && player1Score < player2Score)
        {
            menuManager.winner2.SetActive(true);
            ResetGame();
        }
    }

    private void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;

        ballScript.startForce = 15f;

        menuManager.Start();

        player1ScoreText.text = "0";
        player2ScoreText.text = "0";

        playerControll.transform.position = playerControll.startPos;
    }

    private void Update()
    {
        CountScore();
    }

}
