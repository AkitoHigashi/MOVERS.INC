using UnityEngine;
using UnityEngine.UI;

public class HPslinder : MonoBehaviour
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private Slider runhsliders;
    [SerializeField] private Slider luggagehsviders;
    [SerializeField] private InvokeSystem InvokeSystem;
    [SerializeField]private CollectionArea collectionArea;

    private void OnEnable()
    {
        InvokeSystem.GetHp += HpSetSlider;
        InvokeSystem.GetRunGauge += RunSetSlider;
        
        collectionArea.OnEnterLuggage += LuggageNum;
        collectionArea.OnExitLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        InvokeSystem.GetHp -= HpSetSlider;
        InvokeSystem.GetRunGauge -= RunSetSlider;
        collectionArea.OnEnterLuggage -= LuggageNum;
        collectionArea.OnExitLuggage -= LuggageNum;
    }


    public void HpSetSlider(float sliderValue)
    {
        hpslider.value = sliderValue / UITestStatus.UImaxHp;
    }

    public void RunSetSlider(float sliderValue)
    {
        runhsliders.value = sliderValue / UITestStatus.UImaxRunGauge;
    }

    public void LuggageNum(int sliderValue)
    {
        luggagehsviders.value = (float)sliderValue / UITestStatus.maxItem;
    }
}