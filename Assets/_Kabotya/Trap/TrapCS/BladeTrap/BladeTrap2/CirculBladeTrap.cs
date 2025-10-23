using UnityEngine;

public class CirculBladeTrap : TrapBase
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _circulBladeTrapXSpeed = 10f;

    void Update()
    {
        _gameObject.transform.Rotate(0, _circulBladeTrapXSpeed, 0);
    }
}
