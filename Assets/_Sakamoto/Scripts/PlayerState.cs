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

    /// <summary>
    /// bool値によってStateを更新する
    /// </summary>
    /// <param name="isSprint"></param>
    /// <param name="isCrouch"></param>
    public void UpdateState(bool isSprint,bool isCrouch)
    {
        if (isSprint)
        {
            CurrentState = State.Sprinting;
        }
        else if (isCrouch)
        {
            CurrentState = State.Crouching;
        }
        else
        {
            CurrentState = State.Walking;
        }
    }
}
