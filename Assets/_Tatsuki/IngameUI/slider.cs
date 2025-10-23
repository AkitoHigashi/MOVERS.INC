using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Net.NetworkInformation;

public class slider : MonoBehaviour
{
    [SerializeField] private Slider _hpslider;
    [SerializeField] private Slider _runhsliders;
    [SerializeField] private Slider _luggagehsviders;
    [SerializeField] private Slider _throwGauge;

    //  [SerializeField] private InvokeSystem InvokeSystem;
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
        // InvokeSystem.GetHp += HpSetSlider;
        // InvokeSystem.GetRunGauge += RunSetSlider;

        _collectionArea.OnEnterLuggage += LuggageNum;
        _collectionArea.OnExitLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        // InvokeSystem.GetHp -= HpSetSlider;
        // InvokeSystem.GetRunGauge -= RunSetSlider;
        _collectionArea.OnEnterLuggage -= LuggageNum;
        _collectionArea.OnExitLuggage -= LuggageNum;
    }
    private void Update()
    {
        HpSetSlider();
        SetThrowGauge();
    }
    private void SetThrowGauge()
    {
        float max = _playerThrow.ThrowableTime;
        float current = _playerThrow.ThrowTime;
        float a = current / max;
        _throwGauge.DOValue(a, 1f).SetEase(Ease.OutCubic);

    }

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

    public void LuggageNum(int sliderValue)
    {

        _luggagehsviders.value = (float)sliderValue / _statusNotifer.MaxItem;
    }
}