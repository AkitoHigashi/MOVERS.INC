using UnityEngine;
using UnityEngine.UI;
public class HPslinder : MonoBehaviour
{
    [SerializeField]  private Slider hpslider;
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

    //値はすべて代入でやる予定
    //hpとスタミナもmax値で割った値を入れるべき
    public void HpSetSlider(float sliderValue)
    {
        hpslider.value += sliderValue;
    }
    public void RunSetSlider(float sliderValue)
    {
        runhsliders.value += sliderValue;
    }
    public void LuggageNum(int sliderValue)
    {
        float sum = ((float)sliderValue / InvokeSystem.maxItem);
        luggagehsviders.value += sum;

    }
 





}
