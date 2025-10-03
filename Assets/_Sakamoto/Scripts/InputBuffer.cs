using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    private const string MOVE_ACTION = "Move";
    private const string JUMP_ACTION = "Jump";
    private const string SPRINT_ACTION = "Sprint";
    private InputAction MoveAction => _moveAction;
    private InputAction JumpAction => _jumpAction;
    private InputAction SprintAction => _sprintAction;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            _moveAction = playerInput.actions[MOVE_ACTION];
            _jumpAction = playerInput.actions[JUMP_ACTION];
            _sprintAction = playerInput.actions[SPRINT_ACTION];
        }
    }
}
