using UnityEngine;

public class TrapRange : MonoBehaviour
{
    public static TrapRange Instance;
    [SerializeField] private GameObject _trap;
    [SerializeField] public bool _deactivateWhenExit = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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