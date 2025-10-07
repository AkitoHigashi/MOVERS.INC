<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905

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

<<<<<<< HEAD
    public void UpdateState(bool isSprint)
=======
    /// <summary>
    /// bool値によってStateを更新する
    /// </summary>
    /// <param name="isSprint"></param>
    /// <param name="isCrouch"></param>
    public void UpdateState(bool isSprint,bool isCrouch)
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    {
        if (isSprint)
        {
            CurrentState = State.Sprinting;
        }
<<<<<<< HEAD
=======
        else if (isCrouch)
        {
            CurrentState = State.Crouching;
        }
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
        else
        {
            CurrentState = State.Walking;
        }
    }
}
