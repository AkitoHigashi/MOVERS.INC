using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraFollowPosition;
    private void Update()
    {
        transform.position = _cameraFollowPosition.position;
    }
}
