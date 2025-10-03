using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Transform _cameraForward;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpAction;

    public Transform CameraForward => _cameraForward;
    public float MoveSpeed => _moveSpeed;
    public float JumpAction => _jumpAction;
}
