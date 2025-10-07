using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    private void Update()
    {
        transform.position = _playerPosition.position;
    }
}
