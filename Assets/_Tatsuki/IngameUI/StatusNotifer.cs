
using UnityEngine;

/// <summary>
/// インゲームにつかう情報を取得するクラス
/// </summary>
/// このクラスはなくして直接スライダークラスとつなぐようにする予定
public class StatusNotifer : MonoBehaviour
{
    private LuggageGenerator _luggageGenerator;
    public int MaxItem => _maxItem;
    public int MaxHp => _maxhp;
    public int CurrentHp => _currentHp;
    public int MaxRunGauge => _maxrunGauge;
    public int CurrentRunGauge => _currentRunGauge;
    [SerializeField] private int _maxItem = 0;
    [SerializeField] private int _maxhp = 0;
     [SerializeField] private int  _currentHp = 0;
    [SerializeField] private int _maxrunGauge = 0;
    [SerializeField] private int _currentRunGauge = 0;

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerSprint _playerSprint;

    private void Awake()
    {
       
        _luggageGenerator = FindAnyObjectByType<LuggageGenerator>();
       _maxItem = _luggageGenerator.GetTargetValue();


    }
    private void Start()
    {
       
        _maxhp = (int)_playerHealth.PlayerHP;
        _currentHp = (int)_playerHealth.CurrentHP;
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
        _currentHp = (int)currentHp;
    }

    public void SetRunGauge(float currentRunGauge)
    {
        Debug.Log(currentRunGauge);
        _currentRunGauge -= (int)currentRunGauge;
    }
}
