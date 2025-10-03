using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    private void Update()
    {
        transform.position = _playerPosition.position;
    }
}
