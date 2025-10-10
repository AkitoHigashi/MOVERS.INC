using UnityEngine;

public class TrapRange : MonoBehaviour
{
    [SerializeField] private GameObject _trap;
    public bool _deactivateWhenExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _deactivateWhenExit = false;
            Debug.Log("トラップを開始");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _deactivateWhenExit = true;
            Debug.Log("トラップをストップ");
        }
    }
}