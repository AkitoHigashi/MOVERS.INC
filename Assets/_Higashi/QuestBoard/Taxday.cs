using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Taxday : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text _currentMoneyText;//ŠŽ‹à
    [SerializeField] private Text _taxAmountText;//–Ú•WŠz
    [SerializeField] private GameObject _taxOK;//”[Å¬Œ÷Žž‚ÌZ
    [SerializeField] private GameObject _taxNOT;//”[ÅŽ¸”sŽž‚Ì~

    [SerializeField] private Text _taxAmountData;//“à•”‚Ì–Ú•WŠz
    [SerializeField] private Text _currentMoneyData;//“à•”‚ÌŠŽ‹à‚ÌƒeƒLƒXƒg

    [SerializeField] private Text _CompanyPoint;//‰ïŽÐ‚Ì•]‰¿’l

    [Header("Values")]
    [SerializeField, Header("¡‚ÌŠŽ‹à")] private int _currentValue = 2000;//“à•”‚ÌŠŽ‹à
    [SerializeField, Header("¡‚Ì–Ú•WŠz")] private int _taxValue = 1800;//“à•”‚Ì–Ú•WŠz

    void Start()
    {
        // UI‰Šú•\Ž¦
        _currentMoneyText.text = _currentValue.ToString();
        _taxAmountText.text = _taxValue.ToString();
        _currentMoneyData.text = $"ŠŽ‹à:{_currentValue.ToString()}";//Œ»Ý‚ÌŠŽ‹à‚ðo—Í
        _taxAmountData.text = $"–Ú•WŠz:{_taxValue.ToString()}";//Œ»Ý‚ÌŠŽ‹à‚ðo—Í
    }
    public void Judg()
    {
        int Pay = Mathf.Min(_taxValue, _currentValue);
        int Money = _currentValue - Pay;
        int Tax = _taxValue - Pay;
        bool isSuccess = (_currentValue >= _taxValue);//”äŠr‰‰ŽZŽq
        DOTween.To(() => _currentValue, x =>//ŠŽ‹à‚ð–{“–‚ÌŠŽ‹à‚Ü‚Å•Ï‰»
        {
            _currentValue = x;
            _currentMoneyText.text = _currentValue.ToString();

        }, Money, 5f)
            .OnComplete(() => _currentMoneyData.text = $"ŠŽ‹à:{_currentValue.ToString()}");//Œ»Ý‚ÌŠŽ‹à‚ðo—Í
        DOTween.To(() => _taxValue, x =>
        {
            _taxValue = x;
            _taxAmountText.text = _taxValue.ToString();
        }, Tax, 5f)
            .OnComplete(() => {
                _taxAmountData.text = $"–Ú•WŠz:{_taxValue.ToString()}";
                _taxOK.SetActive(isSuccess);
                _taxNOT.SetActive(!isSuccess);
            });
    }
}
