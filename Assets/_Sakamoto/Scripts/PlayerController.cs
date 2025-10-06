using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerMove _playerMove;
    private Vector2 _currentInput = Vector2.zero;

    private void Awake()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _playerData = GetComponent<PlayerData>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.PlayerMove.canceled += OnInputMove;
        _playerMove?.StartSetVariables(_playerData);
    }

    private void Update()
    {
        
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _currentInput = input;
            _playerMove?.Move(input, _playerData);
        }
        else if (context.canceled)
        {
            _currentInput = Vector2.zero;
            _playerMove?.StopMove();
        }

    }
}
