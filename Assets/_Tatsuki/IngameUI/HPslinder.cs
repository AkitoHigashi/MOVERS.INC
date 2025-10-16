using UnityEngine;
using UnityEngine.UI;

public class HPslinder : MonoBehaviour
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private Slider runhsliders;
    [SerializeField] private Slider luggagehsviders;
    [SerializeField] private InvokeSystem InvokeSystem;


    private void OnEnable()
    {
        InvokeSystem.GetHp += HpSetSlider;
        InvokeSystem.GetRunGauge += RunSetSlider;
        InvokeSystem.GetLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        InvokeSystem.GetHp -= HpSetSlider;
        InvokeSystem.GetRunGauge -= RunSetSlider;
        InvokeSystem.GetLuggage -= LuggageNum;
    }


    public void HpSetSlider(float sliderValue)
    {
        hpslider.value = sliderValue / TestStatus.UImaxHp;
    }

    public void RunSetSlider(float sliderValue)
    {
        runhsliders.value = sliderValue / TestStatus.UImaxRunGauge;
    }

    public void LuggageNum(int sliderValue)
    {
        luggagehsviders.value = (float)sliderValue / TestStatus.maxItem;
    }
}