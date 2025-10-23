using System;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// インゲームにつかう情報を取得するクラス
/// </summary>
/// このクラスはなくして直接スライダークラスとつなぐようにする予定
public class StatusNotifer : MonoBehaviour
{
    private LuggageGenerator _luggageGenerator;
    public int MaxItem = 0;
    public int MaxHp = 0;
    public int CurrentHp = 0;
    public int MaxRunGauge = 100;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
        // _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _luggageGenerator = FindAnyObjectByType<LuggageGenerator>();
        MaxItem = _luggageGenerator.GetTargetValue();


    }
    private void Start()
    {
       
        MaxHp = (int)_playerHealth.PlayerHP;
        CurrentHp = (int)_playerHealth.CurrentHP;
        Debug.Log($"{MaxHp}{CurrentHp}");

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
        Debug.Log(currentHp);
        CurrentHp = (int)currentHp;
    }
}
