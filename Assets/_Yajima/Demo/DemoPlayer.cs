using UnityEngine;

/// <summary>
/// プレイヤー（仮）
/// </summary>
public class DemoPlayer : MonoBehaviour
{
    [SerializeField] ItemBase _item;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem();
        }
    }

    void UseItem()
    {
        _item.UseItem();
    }
}
