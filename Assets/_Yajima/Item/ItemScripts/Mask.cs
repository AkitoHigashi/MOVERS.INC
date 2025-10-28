using System.Collections;
using UnityEngine;

public class Mask : ItemBase
{
    [SerializeField] float _effectiveTime = 120;
    bool _isActivate;
    bool _activeEnd;
    IEnumerator _coroutine;
    PlayerData _player;

    protected override void Init()
    {
        base.Init();
        _player = FindFirstObjectByType<PlayerData>();
    }

    public override void ItemActivate(LuggageData luggage)
    {
        if (_isActivate)
        {
            //被ってるとき
            if (_coroutine == null)
            {
                _coroutine = EffectiveCoroutine();
            }
            StartCoroutine(_coroutine);
            _player.gameObject.tag = null;
        }
        else
        {
            //被ってないとき
            StopCoroutine(_coroutine);
            _player.gameObject.tag = "Player";
        }
        _isActivate = !_isActivate;
    }

    IEnumerator EffectiveCoroutine()
    {
        if (!_activeEnd)
        {
            float delta = 0;
            while (true)
            {
                if (delta >= _effectiveTime)
                {
                    _activeEnd = true;
                    yield break;
                }

                delta += Time.deltaTime;
                yield return null;
            }
        }
    }
}
