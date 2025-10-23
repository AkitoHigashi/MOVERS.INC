using UnityEngine;
using UnityEngine.UI;

public class HPslinder : MonoBehaviour
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private Slider runhsliders;
    [SerializeField] private Slider luggagehsviders;
    [SerializeField] private InvokeSystem InvokeSystem;
    [SerializeField]private CollectionArea collectionArea;
 int count = 0;
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
    }


    public void HpSetSlider()
    {
        hpslider.value = UITestStatus.CurrentHp / UITestStatus.UImaxHp;
    }

    public void RunSetSlider(float sliderValue)
    {
        runhsliders.value = sliderValue / UITestStatus.UImaxRunGauge;
    }

    public void LuggageNum(int sliderValue)
    {
        count += sliderValue;
        luggagehsviders.value = (float)count/ UITestStatus.maxItem;
    }
}