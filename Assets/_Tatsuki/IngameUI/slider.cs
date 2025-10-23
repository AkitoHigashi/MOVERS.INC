using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class slider : MonoBehaviour
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private Slider runhsliders;
    [SerializeField] private Slider luggagehsviders;
    [SerializeField] private Slider ThrowGauge;
  //  [SerializeField] private InvokeSystem InvokeSystem;
    [SerializeField]private CollectionArea collectionArea;
    private PlayerThrow _playerThrow;
 int count = 0;

    private void Start()
    {
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
    }
    private void OnEnable()
    {
        // InvokeSystem.GetHp += HpSetSlider;
        // InvokeSystem.GetRunGauge += RunSetSlider;
        
        collectionArea.OnEnterLuggage += LuggageNum;
        collectionArea.OnExitLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        // InvokeSystem.GetHp -= HpSetSlider;
        // InvokeSystem.GetRunGauge -= RunSetSlider;
        collectionArea.OnEnterLuggage -= LuggageNum;
        collectionArea.OnExitLuggage -= LuggageNum;
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
        float  a = current / max;
        ThrowGauge.DOValue(a,1f).SetEase(Ease.OutCubic);

    }

    public void HpSetSlider()
    {
       // Debug.Log(StatusNotifer.CurrentHp / StatusNotifer.UImaxHp);
        float targetvalue = (float)StatusNotifer.CurrentHp / StatusNotifer.UImaxHp;
        hpslider.DOValue(targetvalue,1f).SetEase(Ease.OutCubic);
    }

    public void RunSetSlider(float sliderValue)
    {
       // runhsliders.value = sliderValue / StatusNotifer.UImaxRunGauge;
    }

    public void LuggageNum(int sliderValue)
    {
        
        luggagehsviders.value = (float)sliderValue/ StatusNotifer.maxItem;
    }
}