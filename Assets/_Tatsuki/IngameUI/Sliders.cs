using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [Header("プレイヤーのゲージ")]
    [SerializeField] private Slider _hpGauge;
    [SerializeField] private Slider _runGauge;
    [SerializeField] private Slider _luggagehsviders;
    [SerializeField] private Slider _throwGauge;
    [SerializeField] private Image _interactFillGauge;
    [Header("入力中以外見せないゲージ")]
    [SerializeField] private GameObject _throwSliderObject;
    [SerializeField] private GameObject _interactGaugeObject;
    [Header("参照するスクリプト")]
    [SerializeField] private PlayerCarry _playerCarry;
    [SerializeField] private CollectionArea _collectionArea;


    private StatusNotifer _statusNotifer;
    private PlayerThrow _playerThrow;
    private PlayerSprint _playerSprint;
    private Interact _interact;

    private Tween _throwTween;
    private Tween _interactTween;


    private void Start()
    {
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
        _playerSprint = FindAnyObjectByType<PlayerSprint>();
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
        _interact = FindAnyObjectByType<Interact>();

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
        RunSetSlider();
        SetThrowGauge();
        SetInteractGauge();
    }
    /// <summary>
    /// スローゲージスライダーの更新をする
    /// </summary>
    private void SetThrowGauge()
    {
        if (_playerThrow.IsThrowing && _playerCarry.IsCarrying)
        {
            _throwSliderObject.SetActive(true);
            float max = _playerThrow.ThrowableTime;
            float current = _playerThrow.ThrowTime;
            float ratio = current / max;
            _throwTween?.Kill();

            // 新しいTweenをセット
            _throwTween = _throwGauge.DOValue(ratio, 0.2f).SetEase(Ease.OutCubic);
        }
        else
        {
            _throwTween?.Kill();
            _throwGauge.value = 0f;
            _throwSliderObject.SetActive(false);
        }
    }
    /// <summary>
    /// インタラクトゲージスライダーの更新をする
    /// </summary>
    private void SetInteractGauge()
    {
        if (_interact.IsInteracting)
        {
            _interactGaugeObject.SetActive(true);
            //_interactFillGauge.fillAmount = _interact.InteractProgress;
            _interactFillGauge.fillAmount = Mathf.Clamp01((Time.time - _interact.InteractCurrentTime) / _interact.InteractTime);
        }
        else
        {
            _interactFillGauge.fillAmount = 0f;
            _interactGaugeObject.SetActive(false);
        }
    }
    /// <summary>
    /// ヘルススライダーの更新をする
    /// </summary>

    public void HpSetSlider()
    {
        // Debug.Log(StatusNotifer.CurrentHp / StatusNotifer.UImaxHp);
        float targetvalue = (float)_statusNotifer.CurrentHp / _statusNotifer.MaxHp;
        _hpGauge.DOValue(targetvalue, 1f).SetEase(Ease.OutCubic);
    }

    public void RunSetSlider()
    {
        float stamina = _playerSprint.Stamina / _playerSprint.StaminaMaxValue;
        _runGauge.DOValue(stamina, 1f).SetEase(Ease.OutCubic);
        //   Debug.Log(stamina.ToString());
    }


    /// <summary>
    /// 指定荷物のカウントスライダーの更新をする
    /// </summary>
    /// <param name="sliderValue"></param>
    public void LuggageNum(int sliderValue)
    {
        _luggagehsviders.value += (float)sliderValue / _statusNotifer.MaxItem;
    }
}