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

    public void LuggageSetText(int text)
    {
        luggagetext.text = $"{text}/{TestStatus.maxItem}";
    }
}