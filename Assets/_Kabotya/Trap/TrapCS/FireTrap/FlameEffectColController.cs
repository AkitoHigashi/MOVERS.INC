using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FlameEffectColController : MonoBehaviour
{
    [SerializeField]private BoxCollider _box;
    [SerializeField]private float _maxHit = 5;
    [Tooltip("サイズの初期値")]
    private float _sizeNum = 1f;
    [Tooltip("位置の初期値")]
    private float _centerNum = 1f;
    [Tooltip("位置をオブジェクトから離れないようにするための値")]
    private float _speedFactor = 2;

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
        //ここに判定を一回挟む
        Flame();
    }

    private void Flame()
    {
        //ここにif文でコライダーがちじまる動作と炎が止まる動作を書く
        if (_box.size.z < _maxHit)
        {
            Vector3 newSize = _box.size;
            Vector3 newCenter = _box.center;
            newSize.z += _sizeNum * Time.deltaTime;
            newCenter.z += _centerNum * Time.deltaTime / _speedFactor;
            _box.size = newSize;
            _box.center = newCenter;
        }
    }
}
