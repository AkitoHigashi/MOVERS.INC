using DG.Tweening;
using UnityEngine;

public class FurikoTrap : MonoBehaviour
{
    [SerializeField] private float swingAngle = 90f;
    [SerializeField] private float duration = 1f;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, swingAngle), duration)
                 .SetLoops(-1, LoopType.Yoyo) 
                 .SetEase(Ease.InOutSine);
    }
}