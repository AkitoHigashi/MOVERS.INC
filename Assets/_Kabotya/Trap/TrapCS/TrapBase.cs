using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public float TrapDamage=>_trapDamage;
    [SerializeField] private float _trapDamage = 0;
}
