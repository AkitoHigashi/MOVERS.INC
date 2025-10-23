using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class StatusNotifer : MonoBehaviour
{
    private LuggageGenerator _luggageGenerator;
    public static int maxItem = 0;
    public static int UImaxHp = 0;
    public static int CurrentHp = 0;
    public static int UImaxRunGauge = 100;
   [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
       // _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _luggageGenerator = FindAnyObjectByType<LuggageGenerator>();
        maxItem = _luggageGenerator.GetTargetValue();
     

    }
    private void Start()
    {
        UImaxHp = (int)_playerHealth.PlayerHP;
        CurrentHp = (int)_playerHealth.CurrentHP;
        Debug.Log($"{UImaxHp}{CurrentHp}");
        
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
