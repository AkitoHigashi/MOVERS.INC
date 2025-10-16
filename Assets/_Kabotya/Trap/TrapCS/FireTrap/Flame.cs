using UnityEngine;

public class Fire : TrapBase
{
    private ParticleSystem _fireEffect;
    //初期値は８
    [SerializeField, Tooltip("パーティクルの速度")] private float _paticularSpeed = 8f;
    //初期値は1.25
    [SerializeField, Tooltip("パーティクルが消える時間（秒）")] private float _paticularDisappearTime = 1.25f;

    private void Start()
    {
        _fireEffect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        EffectUpdate();
    }

    private void EffectUpdate()
    {
        if (_fireEffect.IsAlive())
        {
            var main = _fireEffect.main;
            main.startSpeed = _paticularSpeed;
            main.startLifetime = _paticularDisappearTime;
        }
        else 
        {
            Debug.Log("エフェクトが終わった！");
        }


    }
}
