using UnityEngine;

public class ExplosionTest : MonoBehaviour
{
    [SerializeField] private TrapRange _trapRange;

    [SerializeField, Tooltip("力")] private float _explosionForce = 500f;
    [Tooltip("上方向に飛ぶように調整")] private float _liftPower = 5f;
    [SerializeField, Tooltip("範囲（半径）")] private float _explosionRadius = 5f;
    [Tooltip("オブジェクトの下の位置を計算するために使う")] private float _DounObjpos = 5f;
    [SerializeField, Tooltip("クールタイム")] private float _cooldownTime = 1f;
    [Tooltip("残り時間")] private float _cooldownTimer = 0f;

    private bool _gravityOnOf = false;
    private void Start()
    {
        //親オブジェクトと子オブジェクトから探す
        if (_trapRange == null)
        {
            _trapRange = GetComponentInParent<TrapRange>();
            if (_trapRange == null)
            {
                _trapRange = GetComponentInChildren<TrapRange>();
            }
        }
    }

    private void Update()
    {
        CoolTime();
        TrapCheck();
    }

    private void CoolTime()
    {

        //クールタイムまで待機
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        //クールタイムが終わっていたら通る
        if (_cooldownTimer <= 0f && _gravityOnOf)
        {
            Explode();
            // クールタイムをリセット
            _cooldownTimer = _cooldownTime;
        }
    }

    private void TrapCheck()
    {
        if (_trapRange._deactivateWhenExit)
        {
            _gravityOnOf = false;
        }
        else
        {
            _gravityOnOf = true;
        }
    }

    private void Explode()
    {
        // 爆心地
        Vector3 explosionPos = transform.position;

        //コライダーを取得
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                //（力,プレイヤーの下を中心とした爆発,範囲,どのくらい上に行くのか,一回だけ力を加えるらしい）
                rb.AddExplosionForce(_explosionForce, transform.position - Vector3.up * _DounObjpos, _explosionRadius, _liftPower, ForceMode.Impulse);


            }
        }
    }
}
