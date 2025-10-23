using System;
using UnityEngine;

public class UITestStatus : MonoBehaviour
{
    private LuggageGenerator _luggageGenerator;
    public static int maxItem = 10;
    public static int UImaxHp = 100;
    public static int UImaxRunGauge = 100;

    private void Start()
    {
        _luggageGenerator = FindAnyObjectByType<LuggageGenerator>();
        maxItem = _luggageGenerator.GetTargetValue();
        Debug.Log(maxItem);
    }
}
