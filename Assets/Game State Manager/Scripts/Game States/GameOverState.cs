using UnityEngine;

public class GameOverState : GameState
{
    public override void OnEnter()
    {
        EnteredState();
    }

    public override void OnExit()
    {
        LeftState();
    }
}
