using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = FindObjectOfType<MainMenuState>();
        CurrentState.OnEnter();
    }

    public static void ChangeState(GameState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}