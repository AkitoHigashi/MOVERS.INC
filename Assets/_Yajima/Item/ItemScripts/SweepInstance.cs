using UnityEngine;

public class SweepInstance : MonoBehaviour
{
    [SerializeField] float _power;
    [SerializeField] GameObject _item;
    public float Power => _power;

    private void Awake()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void ActivateEnd()
    {
        _item.SetActive(true);
        gameObject.SetActive(false);
    }
}
