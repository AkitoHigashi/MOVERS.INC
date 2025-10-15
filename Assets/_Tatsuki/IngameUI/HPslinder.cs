using UnityEngine;
using UnityEngine.UI;
public class HPslinder : MonoBehaviour
{
    [SerializeField]  private Slider hpslider;
    [SerializeField] private Slider hsliders;
    [SerializeField] private InvokeSystem InvokeSystem;

    private void OnEnable()
    {
        InvokeSystem.GetHp += hpSetSlider;
        InvokeSystem.GetRunGauge += runSetSlider;
    }

    private void OnDisable()
    {
        InvokeSystem.GetHp -= hpSetSlider;
        InvokeSystem.GetRunGauge -= runSetSlider;
    }

    public void hpSetSlider(float sliderValue)
    {
        hpslider.value += sliderValue;
    }
    public void runSetSlider(float sliderValue)
    {
        hsliders.value += sliderValue;
    }




}
