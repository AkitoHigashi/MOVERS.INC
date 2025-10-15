using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class LuggageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text luggagetext;
    [SerializeField] private InvokeSystem invokeSystem;

    int total = 0;

    private void OnEnable()
    {
        invokeSystem.GetLuggage += LuggageSetText;
    }

    private void OnDisable()
    {
        invokeSystem.GetLuggage -= LuggageSetText;
    }

    public void LuggageSetText(int  text)
    {
        
        if(total < InvokeSystem.maxItem) 
        total += text;  //テスト用に加算にしてる　本当は代入
        luggagetext.text =$"{total}/{InvokeSystem.maxItem}";
    }

}
