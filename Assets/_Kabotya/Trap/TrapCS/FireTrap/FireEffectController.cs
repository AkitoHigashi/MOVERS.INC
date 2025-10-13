using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FireEffectController : MonoBehaviour
{
    [SerializeField]private BoxCollider _box;
    [SerializeField]private float _maxHit = 5;
    private float _sizeNum = 1f;
    private float _centerNum = 1f;

    private void Start()
    {
        _box = GetComponent<BoxCollider>();
        _box.isTrigger = true;
    }

    private void Update()
    {
        Firing();
    }

    //発砲
    private void Firing() 
    {
        Flame();
    }

    private void Flame()
    {
        if (_box.size.z < _maxHit)
        {
            Vector3 newSize = _box.size;
            Vector3 newCenter = _box.center;
            newSize.z += _sizeNum * Time.deltaTime;
            newCenter.z += _centerNum * Time.deltaTime / 2;
            _box.size = newSize;
            _box.center = newCenter;
        }
    }
}
