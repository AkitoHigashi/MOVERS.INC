using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Transform _cameraForward;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpAction;

    public Transform CameraForward => _cameraForward;
    public float MoveSpeed => _moveSpeed;
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float JumpAction => _jumpAction;
}
