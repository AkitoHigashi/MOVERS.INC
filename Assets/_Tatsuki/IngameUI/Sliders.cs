using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [SerializeField] private Slider _hpslider;
    [SerializeField] private Slider _runhsliders;
    [SerializeField] private Slider _luggagehsviders;
    [SerializeField] private Slider _throwGauge;
    [SerializeField] private PlayerCarry _playerCarry;
    [SerializeField] private GameObject _throuSlider;
    
   
    [SerializeField] private CollectionArea _collectionArea;
    private StatusNotifer _statusNotifer;
    private PlayerThrow _playerThrow;
    private PlayerSprint _playerSprint;
    private Tween _gaugeTween;

    private void Start()
    {
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
        _playerSprint = FindAnyObjectByType<PlayerSprint>();
    
    }
    private void OnEnable()
    {
        _collectionArea.OnEnterLuggage += LuggageNum;
        _collectionArea.OnExitLuggage += LuggageNum;
    }

    private void OnDisable()
    {
        _collectionArea.OnEnterLuggage -= LuggageNum;
        _collectionArea.OnExitLuggage -= LuggageNum;
    }
    private void Update()
    {
        HpSetSlider();
        SetThrowGauge();
        RunSetSlider();
    }
    /// <summary>
    /// スローゲージスライダーの更新をする
    /// </summary>
    private void SetThrowGauge()
    {
        if (_playerThrow.IsThrowing&&_playerCarry.IsCarrying)
        {
            _throuSlider.SetActive(true);
            float max = _playerThrow.ThrowableTime;
            float current = _playerThrow.ThrowTime;
            float ratio = current / max;
            _gaugeTween?.Kill();

            // 新しいTweenをセット
            _gaugeTween = _throwGauge.DOValue(ratio, 0.2f).SetEase(Ease.OutCubic);
        }
        else
        {
            _gaugeTween?.Kill();
            _throwGauge.value = 0f;
            _throuSlider.SetActive(false);
        }
    }
    /// <summary>
    /// ヘルススライダーの更新をする
    /// </summary>

    public void HpSetSlider()
    {
        // Debug.Log(StatusNotifer.CurrentHp / StatusNotifer.UImaxHp);
        float targetvalue = (float)_statusNotifer.CurrentHp / _statusNotifer.MaxHp;
        _hpslider.DOValue(targetvalue, 1f).SetEase(Ease.OutCubic);
    }

    public void RunSetSlider()
    {
        float stamina = _playerSprint.Stamina/_playerSprint.StaminaMaxValue;
        _runhsliders.DOValue(stamina, 1f).SetEase(Ease.OutCubic);
     //   Debug.Log(stamina.ToString());
    }
  

    /// <summary>
    /// 指定荷物のカウントスライダーの更新をする
    /// </summary>
    /// <param name="sliderValue"></param>
    public void LuggageNum(int sliderValue)
    {

        _luggagehsviders.value = (float)sliderValue / _statusNotifer.MaxItem;
    }
}