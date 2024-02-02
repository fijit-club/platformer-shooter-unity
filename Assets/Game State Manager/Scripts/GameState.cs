using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] public bool addDelay;
    [SerializeField] protected float delayAmount;
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

    protected IEnumerator DelayedEnter()
    {
        yield return new WaitForSeconds(delayAmount);
        EnteredState();
    }

    public abstract void OnEnter();
    public abstract void OnExit();
}
