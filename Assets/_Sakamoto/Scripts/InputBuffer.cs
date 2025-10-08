using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    private const string MOVE_ACTION = "Move";
    private const string JUMP_ACTION = "Jump";
    private const string SPRINT_ACTION = "Sprint";
    private const string CROUCH_ACTION = "Crouch";
    private const string CARRY_ACTION = "Carry";
    private const string THROW_ACTION = "Throw";
    private const string INTERACT_ACTION = "Interact";
    private const string ITEMUSE_ACTION= "ItemUse";

    public InputAction PlayerMove => _playerMove;
    public InputAction PlayerJump => _playerJump;
    public InputAction PlayerSprint => _playerSprint;
    public InputAction PlayerCrouch => _playerCrouch;
    public InputAction PlayerCarry => _playerCarry;
    public InputAction PlayerThrow => _playerThrow;
    public InputAction PlayerInteract => _playerInteract;
    public InputAction PlayerItemUse => _playerItemUse;


    private InputAction _playerMove;
    private InputAction _playerJump;
    private InputAction _playerSprint;
    private InputAction _playerCrouch;
    private InputAction _playerCarry;
    private InputAction _playerThrow;
    private InputAction _playerInteract;
    private InputAction _playerItemUse;

    private void Awake()
    {
        if(TryGetComponent<PlayerInput>(out var playerInput))
        {
            _playerMove = playerInput.actions[MOVE_ACTION];
            _playerJump = playerInput.actions[JUMP_ACTION];
            _playerSprint = playerInput.actions[SPRINT_ACTION];
            _playerCrouch = playerInput.actions[CROUCH_ACTION];
            _playerCarry = playerInput.actions[CARRY_ACTION];
            _playerThrow = playerInput.actions[THROW_ACTION];
            _playerInteract = playerInput.actions[INTERACT_ACTION];
            _playerItemUse = playerInput.actions[ITEMUSE_ACTION];
        }
        else
        {
            Debug.LogError("PlayerInput component not found on the GameObject.");
        }
    }
}
