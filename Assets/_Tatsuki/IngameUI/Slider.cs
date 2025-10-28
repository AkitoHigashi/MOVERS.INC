using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Net.NetworkInformation;

public class Slider : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _hpslider;
    [SerializeField] private UnityEngine.UI.Slider _runhsliders;
    [SerializeField] private UnityEngine.UI.Slider _luggagehsviders;
    [SerializeField] private UnityEngine.UI.Slider _throwGauge;
    
   
    [SerializeField] private CollectionArea _collectionArea;
    private StatusNotifer _statusNotifer;
    private PlayerThrow _playerThrow;
    private PlayerSprint _playerSprint;
    

    private void Start()
    {
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
        _playerSprint = FindAnyObjectByType<PlayerSprint>();
    
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
        RunSetSlider();
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

    public void RunSetSlider()
    {
        float stamina = _playerSprint.Stamina/_playerSprint.StaminaMaxValue;
        _runhsliders.DOValue(stamina, 1f).SetEase(Ease.OutCubic);
        Debug.Log(stamina.ToString());
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