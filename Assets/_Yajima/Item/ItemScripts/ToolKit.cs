using System.Collections;
using UnityEngine;

public class ToolKit : ItemBase
{
    [SerializeField] GameObject _tool;

    protected override void Init()
    {
        base.Init();
        _tool.gameObject.SetActive(false);
    }

    [ContextMenu("a")]
    public override void ItemActivate()
    {
        if (!_tool.activeSelf)
        {
            _tool.transform.SetParent(transform.parent);
            _tool.SetActive(true);
            _tool.transform.position = _cameraTrans.position;
            _tool.transform.rotation = _cameraTrans.rotation;
            gameObject.SetActive(false);
        }
    }
}
