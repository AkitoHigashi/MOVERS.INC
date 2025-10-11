using System;
using DG.Tweening;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    private GlobalParameters _gp;

    [Header("各ダイアログの親GameObject")]
    [SerializeField] private GameObject _taxPaymentDialog;
    [SerializeField] private GameObject _rankNotificationDialog;
    [SerializeField] private GameObject _notTaxDayNotificationDialog;
    
    [Header("UI要素参照")]
    [SerializeField] private Text _uiMoney;
    [SerializeField] private Text _uiDay;
    [SerializeField] private Text _uiRank;
    [Header("納税ダイアログの要素参照")]
    [SerializeField] private Text _tpbRank;
    [SerializeField] private Text _tpbQuota;
    [SerializeField] private Text _tpbErrMsg;
    [SerializeField] private Text _tpbTaxPaid;
    [Header("納税結果ダイアログの要素参照")]
    [SerializeField] private Text _resultReached;
    [SerializeField] private Text _resultRankChange;
    [SerializeField] private Text _resultRank;

    private int _taxPaid = 0; // 納税額保持用
    private void Start()
    {
        _gp = GlobalParameters.Instance;
        _gp.ModifyDayCount(1);

        UpdateUI();
    }

    /// <summary>
    /// UI項目表示を更新する
    /// </summary>
    private void UpdateUI()
    {
        _uiMoney.text = "所持金:" + _gp.Money;
        _uiDay.text = "日数:" + _gp.DayCount;
        _uiRank.text = "評価値:" + _gp.PlayerRank;
    }
    
    /// <summary>
    /// 納税ダイアログの入力値をチェックする処理
    /// </summary>
    /// <returns></returns>
    private bool CheckTaxPaymentInput()
    {
        int paid;
        if (!int.TryParse(_tpbTaxPaid.text, out paid))
        {
            ShowTaxPaymentCheckError("整数を書け。数学ぐらいわかるだろう。");
            return false;
        }

        if (paid > _gp.Money)
        {
            ShowTaxPaymentCheckError("そんなに金持っておらんぞ。");
            return false;
        }

        if (paid < 0)
        {
            ShowTaxPaymentCheckError("なに、余が払えとでも？");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 納税ダイアログのエラーメッセージを表示する
    /// </summary>
    /// <param name="msg">メッセージ内容</param>
    private void ShowTaxPaymentCheckError(string msg)
    {
        _tpbErrMsg.gameObject.SetActive(true);
        _tpbErrMsg.text = msg;
    }

    /// <summary>
    /// 納税処理の実施
    /// </summary>
    private void DoTaxPayment()
    {
        bool quotaReached = _gp.IsTaxQuotaReached(_taxPaid);
        _gp.PayTax(_taxPaid);
        UpdateUI();
        ShowPaymentResultDialog(quotaReached);
    }
    /// <summary>
    /// 納税結果ダイアログを表示する
    /// </summary>
    private void ShowPaymentResultDialog(bool quotaReached)
    {
        _rankNotificationDialog.SetActive(true);
        _rankNotificationDialog.transform.localScale = Vector3.zero;

        _resultReached.text = quotaReached ? "納税ノルマ達成" : "納税ノルマ未達成";
        _resultRankChange.text = quotaReached ? "評価値が上がった" : "納税ノルマ下がった";
        _resultRank.text = "評価値が" + _gp.PlayerRank + "になった";
        
        _rankNotificationDialog.transform.DOScale(Vector3.one, 0.2f);
    }

    /// <summary>
    /// 納税日でないダイアログを表示する
    /// </summary>
    private void ShowNotTaxDayNotification()
    {
        _notTaxDayNotificationDialog.SetActive(true);
        _notTaxDayNotificationDialog.transform.localScale = Vector3.zero;
        _notTaxDayNotificationDialog.transform.DOScale(Vector3.one, 0.2f);
    }
    
    /// <summary>
    /// 納税ボタン（画面左下）押下時処理
    /// </summary>
    public void OnPayTaxButtonClick()
    {
        // 納税日ではない場合
        if (!_gp.IsTaxDay())
        {
            ShowNotTaxDayNotification();
            return;
        }
        _taxPaymentDialog.SetActive(true);
        _taxPaymentDialog.transform.localScale = Vector3.zero;

        _tpbRank.text = "あなたの評価値は：" + _gp.PlayerRank;
        _tpbQuota.text = "よって、今回の納税ノルマは：" + _gp.TaxQuota;
        
        _taxPaymentDialog.transform.DOScale(Vector3.one, 0.2f);
    }
    
    /// <summary>
    /// 納税ダイアログのOKボタン押下時処理
    /// </summary>
    public void OnPaymentOKButtonClick()
    {
        _tpbErrMsg.gameObject.SetActive(false);
        if (!CheckTaxPaymentInput()) return;

        _taxPaid = int.Parse(_tpbTaxPaid.text);
        _taxPaymentDialog.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            _taxPaymentDialog.SetActive(false);
            DoTaxPayment();
        });
    }
    
    /// <summary>
    /// 指定されたダイアログ親GameObjectを閉じる
    /// </summary>
    /// <param name="go">対象GameObject</param>
    public void CloseDialog(GameObject go)
    {
        go.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => go.SetActive(false));
    }
}