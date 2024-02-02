using SpaceEscape;
using TMPro;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public static int Score { get; private set; }
    public static int Coins { get; private set; }

    [SerializeField] private TMP_Text mainMenuCoins;
    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private TMP_Text[] coinsTexts;
    [SerializeField] private TMP_Text[] highScoreTexts;

    public void UpdateScore(int increment)
    {
        Score += increment;
        foreach (var scoreText in scoreTexts)
            scoreText.text = Score.ToString();
        
        Debug.ClearDeveloperConsole();
    }

    public void UpdateCoins(int increment)
    {
        Coins += increment;
        foreach (var coinsText in coinsTexts)
            coinsText.text = Coins.ToString();
    }

    public void UpdateCoins()
    {
        mainMenuCoins.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
        
    }

    public void UpdateHighScore()
    {
        foreach (var highScoreText in highScoreTexts)
        {
            highScoreText.text = Bridge.GetInstance().thisPlayerInfo.highScore.ToString();
        }
    }

    public void SendScore()
    {
        Bridge.GetInstance().UpdateCoins(Coins);
        Bridge.GetInstance().SendScore(Score);
        
        foreach (var coinsText in coinsTexts)
            coinsText.text = Coins.ToString();
        foreach (var scoreText in scoreTexts)
            scoreText.text = Score.ToString();
        
        Score = 0;
        Coins = 0;
    }
}
