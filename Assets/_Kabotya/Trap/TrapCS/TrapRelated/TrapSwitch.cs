using UnityEngine;

public class TrapSwitch : MonoBehaviour
{
    [Tooltip("トラップの攻撃範囲にいるのか判定")] public bool _isThormTrapped;
    private void OnTriggerEnter(Collider other)
    {
        _isThormTrapped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isThormTrapped = false;
    }
}
