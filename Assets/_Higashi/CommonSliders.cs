using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CommonSliders : MonoBehaviour
{
    [Header("プレイヤーのゲージ")]
    [SerializeField] private Slider _hpGauge;
    [SerializeField] private Slider _runGauge;
    [SerializeField] private Slider _throwGauge;
    [SerializeField] private Image _interactFillGauge;

    [Header("入力中以外見せないゲージ")]
    [SerializeField] private GameObject _throwSliderObject;
    [SerializeField] private GameObject _interactGaugeObject;

    private StatusNotifer _statusNotifer;
    private PlayerThrow _playerThrow;
    private PlayerSprint _playerSprint;
    private PlayerCarry _playerCarry;
    private Interact _interact;

    private Tween _throwTween;

    private void Start()
    {
        _playerCarry = FindAnyObjectByType<PlayerCarry>();
        _playerThrow = FindAnyObjectByType<PlayerThrow>();
        _playerSprint = FindAnyObjectByType<PlayerSprint>();
        _statusNotifer = FindAnyObjectByType<StatusNotifer>();
        _interact = FindAnyObjectByType<Interact>();
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

}
