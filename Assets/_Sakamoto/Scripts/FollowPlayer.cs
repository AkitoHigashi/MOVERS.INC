<<<<<<< HEAD
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    private void Update()
    {
        transform.position = _playerPosition.position;
=======
﻿using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraFollowPosition;
    private void Update()
    {
        transform.position = _cameraFollowPosition.position;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    }
}
