using System;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private LuggageGenerator _luggageGenerator;

    private void Start()
    {
        int i = _luggageGenerator.GetTargetValue();
    }
}
