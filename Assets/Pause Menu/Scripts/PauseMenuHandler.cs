using ShooterGame;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public static bool Vibration = true;
    public static bool Volume = true;
    
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private GameOverState gameOverState;
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    
    public void TurnOffVibrate()
    {
        Vibration = false;
    }

    public void TurnOnVibrate()
    {
        Vibration = true;
    }

    public void TurnOffVolume()
    {
        foreach (var audioSource in audioSources)
            audioSource.volume = 0f;
        Volume = false;
    }

    public void TurnOnVolume()
    {
        foreach (var audioSource in audioSources)
            audioSource.volume = 1f;
        Volume = true;
    }

    public void ExitGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameOverState.addDelay = false;
        GameStateManager.ChangeState(gameOverState);
        gameOverState.addDelay = true;
    }
}
