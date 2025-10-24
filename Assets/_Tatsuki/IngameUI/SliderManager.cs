using UnityEngine;
using DG.Tweening;


public class SliderManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _hpslider;
    [SerializeField] private UnityEngine.UI.Slider _runhsliders;
    [SerializeField] private UnityEngine.UI.Slider _luggagehsviders;
    [SerializeField] private UnityEngine.UI.Slider _throwGauge;　　

  
    [SerializeField] private CollectionArea _collectionArea;
    private StatusNotifer _statusNotifer;
    private PlayerThrow _playerThrow;
    

    private void Start()
    {
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
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
    private void Update()
    {
        HpSetSlider();
        SetThrowGauge();
    }
    /// <summary>
    /// スローゲージスライダーの更新をする
    /// </summary>
    private void SetThrowGauge()
    {
        float max = _playerThrow.ThrowableTime;
        float current = _playerThrow.ThrowTime;　
        float a = current / max;
        _throwGauge.DOValue(a, 1f).SetEase(Ease.OutCubic);

    }
    /// <summary>
    /// ヘルススライダーの更新をする
    /// </summary>

    public void HpSetSlider()
    {
        // Debug.Log(StatusNotifer.CurrentHp / StatusNotifer.UImaxHp);
        float targetvalue = (float)_statusNotifer.CurrentHp / _statusNotifer.MaxHp;
        _hpslider.DOValue(targetvalue, 1f).SetEase(Ease.OutCubic);
    }

    public void RunSetSlider(float sliderValue)
    {
        // runhsliders.value = sliderValue / StatusNotifer.UImaxRunGauge;
    }

    /// <summary>
    /// 指定荷物のカウントスライダーの更新をする
    /// </summary>
    /// <param name="sliderValue"></param>
    public void LuggageNum(int sliderValue)
    {

        _luggagehsviders.value = (float)sliderValue / _statusNotifer.MaxItem;
    }
}