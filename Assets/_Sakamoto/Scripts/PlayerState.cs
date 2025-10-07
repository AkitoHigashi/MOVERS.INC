using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public State CurrentState { get; private set; } = State.Idle;
    public enum State
    {
        Walking,
        Sprinting,
        Crouching,
        Sliding,
        Carrying,
        Throwing,
        Idle
    }

    /// <summary>
    /// bool値によってStateを更新する
    /// </summary>
    /// <param name="isSprint"></param>
    /// <param name="isCrouch"></param>
    public void UpdateState(bool isSprint, bool isCrouch, bool isSliding)
    {
        if (isSliding)
        {
            CurrentState = State.Sliding;
        }
        else if (isCrouch)
        {
            CurrentState = State.Crouching;
        }
        else if (isSprint)
        {
            CurrentState = State.Sprinting;
        }
        else
        {
            CurrentState = State.Walking;
        }
    }
}
