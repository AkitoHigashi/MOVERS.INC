using System.Collections;
using UnityEngine;

public class FlameEffectColController : MonoBehaviour
{
    [SerializeField] private BoxCollider _box;
    [SerializeField] private float _maxHitSize = 5;
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private TrapRange _trapRange;

    [Tooltip("炎が打てるかどうか")]private bool _flameTrigger = false;
    [SerializeField,Tooltip("炎が消えるまでの時間")]private float _fireDuratopn = 5;
    [Tooltip("経っている時間")]private float _playbackTime;
    [Tooltip("サイズの初期値")]private float _sizeNum = 1f;
    [Tooltip("位置の初期値")]private float _centerNum = 1f;
    [Tooltip("位置をオブジェクトから離れないようにするための値")]private float _speedFactor = 2;


    private void Awake()
    {
        _fireEffect.Stop();
        _fireEffect.Clear();
    }
    private void Start()
    {
        if (_trapRange == null)
            _trapRange = FindScript.FindInParentOrChildren<TrapRange>(gameObject);

        //消えるまでの時間を代入
        var main = _fireEffect.main;
        main.duration = _fireDuratopn;
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
        if (_flameTrigger == false && !_trapRange._deactivateWhenExit) 
        {
            StartCoroutine(FireFlame());
        }
    }

    private void Flame()
    {
        if (_fireDuratopn > _fireEffect.time)
        {
            //ここにif文でコライダーがちじまる動作と炎が止まる動作を書く
            if (_box.size.z < _maxHitSize)
            {
                Debug.Log("コライダーは動いてるよー");
                Vector3 newSize = _box.size;
                Vector3 newCenter = _box.center;
                newSize.z += _sizeNum * Time.deltaTime;
                newCenter.z += _centerNum * Time.deltaTime / _speedFactor;
                _box.size = newSize;
                _box.center = newCenter;
            }
        }
        else
        {
            _box.center = Vector3.zero;
            _box.size = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    private IEnumerator FireFlame() 
    {
        _flameTrigger = true;
        _fireEffect.Play();

        while (!_trapRange._deactivateWhenExit) 
        {
            Flame();
            yield return null;
        }
        _flameTrigger = false;
    }

}
