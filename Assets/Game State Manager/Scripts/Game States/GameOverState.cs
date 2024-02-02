using UnityEngine;

public class GameOverState : GameState
{
    public override void OnEnter()
    {
        if (!addDelay)
            EnteredState();
        else
            StartCoroutine(DelayedEnter());
    }

    public override void OnExit()
    {
        LeftState();
    }
}
