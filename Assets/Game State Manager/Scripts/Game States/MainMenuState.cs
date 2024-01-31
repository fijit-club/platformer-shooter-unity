public class MainMenuState : GameState
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
