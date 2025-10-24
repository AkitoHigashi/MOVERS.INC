using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NewHpBar : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private Image _image;
    private float _hp = 0f;
    private float _maxhp = 0f;
    private bool _setHp = false;

    private void Awake()
    {
        
       _playerHealth = FindAnyObjectByType<PlayerHealth>();
    }
    private void Start()
    {
        _image = GetComponent<Image>();
       
    }
    private void OnEnable()
    {
        _playerHealth.PlayerHealthChanged += inputPlayerHealth;
    }
    private void OnDisable()
    {
        _playerHealth.PlayerHealthChanged -= inputPlayerHealth;
    }
    private void inputPlayerHealth(float health)
    {
        if (!_setHp)
        {
            _maxhp = health;
        }
            _hp = health;
        _image.DOFillAmount(_hp/_maxhp,1f).SetEase(Ease.OutCubic);
    }

    

}
