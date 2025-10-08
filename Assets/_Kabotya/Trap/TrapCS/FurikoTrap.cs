using DG.Tweening;
using UnityEngine;

public class FurikoTrap : MonoBehaviour
{

    [Tooltip("下げたらスピードが上がる")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("-1ならループ、その他の整数を入れるとその回数ループ")]
    [SerializeField] private int _loopNumber　= -1;

    [Tooltip("何度回転するか")]
    private float _swingAngle = 180f;

    private void Start()
    {
        RotateFuriko();
    }

    private void RotateFuriko() 
    {
        transform.DORotate(new Vector3(0, 0, _swingAngle), _duration)
         .SetLoops(_loopNumber, LoopType.Yoyo)
         .SetEase(Ease.InOutQuad);
    }
}