using System.Collections;
using UnityEngine;

public class FlameEffectColController : MonoBehaviour
{
    [SerializeField] private BoxCollider _box;
    [SerializeField] private float _maxHitSize = 5;
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private TrapRange _trapRange;
    [SerializeField,Tooltip("boxの膨張スピード")] private float _boxSpeed = 0.1f;
    [SerializeField, Tooltip("炎のクールダウン")] private float _flameCoolDown = 10f;
    [Tooltip("炎が打てるかどうか")] private bool _flameTrigger = false;
    [SerializeField, Tooltip("炎が消えるまでの時間")] private float _fireDuratopn = 5;
    [Tooltip("サイズの初期値")] private float _sizeNum = 1f;
    [Tooltip("位置の初期値")] private float _centerNum = 1f;
    [Tooltip("位置をオブジェクトから離れないようにするための値")] private float _speedFactor = 2;

    //Boxの初期値
    private Vector3 _inItialBoxSize;
    private Vector3 _initialBoxCenter;

    private void Awake()
    {
        _fireEffect.Stop();
        _fireEffect.Clear();
    }

    private void Start()
    {
        if (_trapRange == null)
            _trapRange = FindScript.FindInParentOrChildren<TrapRange>(gameObject);

        var main = _fireEffect.main;
        main.duration = _fireDuratopn;

        _box = GetComponent<BoxCollider>();
        _box.isTrigger = true;

        // 初期値を保存
        _inItialBoxSize = _box.size;
        _initialBoxCenter = _box.center;

        //クールタイムと炎を出す時間を調整
        _flameCoolDown += _fireDuratopn;
    }

    private void Update()
    {
        Firing();
    }

    private void Firing()
    {
        if (_flameTrigger == false && !_trapRange._deactivateWhenExit)
        {
            StartCoroutine(FireFlame());
        }
    }
    private IEnumerator FireFlame()
    {
        _flameTrigger = true;
        _fireEffect.Play();

        while (!_trapRange._deactivateWhenExit && _fireEffect.isPlaying)
        {
            Flame();
            yield return null;
        }

        // 炎が停止したらコライダーを初期値に戻す
        ResetCollider();

        // クールダウン
        yield return new WaitForSeconds(_flameCoolDown);

        _flameTrigger = false;
    }

    private void Flame()
    {
        // ParticleSystemがまだ再生中かチェック
        if (_fireEffect.isPlaying && _box.size.z < _maxHitSize)
        {
            Debug.Log("コライダーは動いてるよー");
            Vector3 newSize = _box.size;
            Vector3 newCenter = _box.center;
            newSize.z += _sizeNum * _boxSpeed;
            newCenter.z += _centerNum / _speedFactor * _boxSpeed;
            _box.size = newSize;
            _box.center = newCenter;
        }
    }

    private void ResetCollider()
    {
        Debug.Log("コライダーが元に戻った");
        _box.size = _inItialBoxSize;
        _box.center = _initialBoxCenter;
    }
}