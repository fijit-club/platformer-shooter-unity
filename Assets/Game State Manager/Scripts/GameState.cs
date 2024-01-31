using UnityEngine;
using UnityEngine.Events;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;
    
    protected void EnteredState()
    {
        onEnter.Invoke();
    }

    protected void LeftState()
    {
        onExit.Invoke();
    }

    public abstract void OnEnter();
    public abstract void OnExit();
}
