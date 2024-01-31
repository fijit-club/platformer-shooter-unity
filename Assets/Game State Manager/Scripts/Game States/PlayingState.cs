public class PlayingState : GameState
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
