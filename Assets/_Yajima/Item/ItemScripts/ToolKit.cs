using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ToolKit : ItemBase
{
    [SerializeField, Tooltip("効果発動時間")] float _effectiveTime = 1;
    BoxCollider _bc;
    Coroutine _coroutine;

    protected override void Init()
    {
        base.Init();
        _bc = GetComponent<BoxCollider>();
        _bc.enabled = false;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public override void ItemActivate()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CoolTimeCoroutine());
        }
    }

    IEnumerator CoolTimeCoroutine()
    {
        transform.position = _cameraTrans.position;
        transform.rotation = _cameraTrans.rotation;
        _bc.enabled = true;
        yield return new WaitForSeconds(_effectiveTime);
        _bc.enabled = false;
        _coroutine = null;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrapBase")
        {
            TrapDestroy(other.gameObject);
        }
    }

    void TrapDestroy(GameObject trap)
    {
        Destroy(trap);
    }
}
