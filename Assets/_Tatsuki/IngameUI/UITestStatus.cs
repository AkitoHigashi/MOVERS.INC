using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class UITestStatus : MonoBehaviour
{
    private LuggageGenerator _luggageGenerator;
    public static int maxItem = 10;
    public static int UImaxHp = 100;
    public static int CurrentHp = 100;
    public static int UImaxRunGauge = 100;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _luggageGenerator = FindAnyObjectByType<LuggageGenerator>();
        maxItem = _luggageGenerator.GetTargetValue();
        Debug.Log(maxItem);
        UImaxHp = (int)_playerHealth.PlayerHP;
        CurrentHp = (int)_playerHealth.CurrentHP;

    }
    private void OnEnable()
    {
        _playerHealth.PlayerHealthChanged += SetHealth;
    }
    private void OnDisable()
    {
        _playerHealth.PlayerHealthChanged -= SetHealth;
    }

    public void SetHealth(float currentHp)
    {
        CurrentHp = (int)currentHp;
    }
}
