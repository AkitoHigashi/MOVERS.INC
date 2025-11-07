using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [Header("荷物のゲージ")]
    [SerializeField] private Slider _luggagehsviders;
    [Header("参照するスクリプト")]
    [SerializeField] private CollectionArea _collectionArea;

    private float _luggageSlider = 0f;
    private StatusNotifer _statusNotifer;

    private Tween _throwTween;

    private void Start()
    {
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
    }
    private void OnEnable()
    {
        _collectionArea.OnEnterLuggage += LuggageNum;
        _collectionArea.OnExitLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        _collectionArea.OnEnterLuggage -= LuggageNum;
        _collectionArea.OnExitLuggage -= LuggageNum;
    }
    /// <summary>
    /// 指定荷物のカウントスライダーの更新をする
    /// </summary>
    /// <param name="sliderValue"></param>
    public void LuggageNum(int sliderValue)
    {
        _luggageSlider += (float)sliderValue / _statusNotifer.MaxItem;
    }

    public void LuggageSliderUpdate()
    {
        Debug.Log(_luggageSlider);
        _luggagehsviders.value = _luggageSlider;
    }
    /// <summary>
    /// 指定荷物の運んだ割合
    /// </summary>
    /// <returns></returns>
    public float GetLuggage()
    {
        return _luggageSlider;
    }
}