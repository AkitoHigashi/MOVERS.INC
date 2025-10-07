using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public State CurrentState { get; private set; } = State.Idle;
    public enum State
    {
        Walking,
        Sprinting,
        Crouching,
        Carrying,
        Throwing,
        Idle
    }

    public void UpdateState(bool isSprint)
    {
        if (isSprint)
        {
            CurrentState = State.Sprinting;
        }
        else
        {
            CurrentState = State.Walking;
        }
    }
}
