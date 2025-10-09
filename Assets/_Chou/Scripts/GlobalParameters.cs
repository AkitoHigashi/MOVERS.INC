using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParameters : MonoBehaviour
{
    public static GlobalParameters Instance;
    
    [SerializeField] private int _dayCount; // 日数
    [SerializeField] private int _taxQuota; // 納税ノルマ
    [SerializeField] private int _money; // 所持金
    [SerializeField] private int _playerRank; // 評価値

    public int DayCount { get => _dayCount; private set => _dayCount = value; } // 日数
    public int TaxQuota { get => _taxQuota; private set => _taxQuota = value; } // 納税ノルマ
    public int Money { get => _money; private set => _money = value; } // 所持金
    public int PlayerRank { get => _playerRank; private set => _playerRank = value; } // 評価値

    private readonly int _taxDayInterval = 2; // 納税日間隔
    private readonly Dictionary<int, int> _taxQuotaByRank = new Dictionary<int, int> // 評価値毎の納税ノルマ
    {
        { 1, 1000 },
        { 2, 2000 },
        { 3, 3000 },
        { 4, 4000 },
        { 5, 5000 }
    };

    private void Awake()
    {
        // シングルトーン化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初期化処理
    /// ゲーム起動やシーン遷移の時に
    /// </summary>
    public void Init(int dayCount, int money, int playerRank)
    {
        DayCount = dayCount;
        TaxQuota = _taxQuotaByRank[playerRank];
        Money = money;
        PlayerRank = playerRank;
    }

    /// <summary>
    /// 日数を指定された値で更新する
    /// </summary>
    /// <param name="day">変動の量</param>
    public void ModifyDayCount(int day)
    {
        DayCount += day;
    }

    /// <summary>
    /// 所持金を指定された値で変動する
    /// </summary>
    /// <param name="amount">変動の量</param>
    public void ModifyMoney(int amount)
    {
        Money += amount;
    }

    /// <summary>
    /// 評価値を指定された値で更新する
    /// </summary>
    /// <param name="rank"></param>
    public void SetPlayerRank(int rank)
    {
        PlayerRank = rank;
    }

    /// <summary>
    /// 納税日であるか判定する
    /// </summary>
    /// <returns></returns>
    public bool IsTaxDay()
    {
        return DayCount % _taxDayInterval == 1 && DayCount > 1;
    }

    /// <summary>
    /// 納税額がノルマに達成したか判定する
    /// </summary>
    /// <param name="paidTax">納税額</param>
    /// <returns></returns>
    public bool IsTaxQuotaReached(int paidTax)
    {
        return paidTax >= TaxQuota;
    }
    /// <summary>
    /// 納税と伴う一連の処理
    /// </summary>
    /// <param name="amount">納税する金額</param>
    public void PayTax(int amount)
    {
        _money -= amount;
        JudgePlayerRankByTaxPaid(amount);
        JudgeTaxQuotaByPlayerRank();
    }
    
    /// <summary>
    /// 納税ノルマを設定する
    /// </summary>
    public void JudgeTaxQuotaByPlayerRank()
    {
        TaxQuota = _taxQuotaByRank[PlayerRank];
    }

    /// <summary>
    /// 納税した金額で評価値を判定する
    /// </summary>
    /// <param name="paidTax">納税した金額</param>
    public void JudgePlayerRankByTaxPaid(int paidTax)
    {
        if (IsTaxQuotaReached(paidTax))
        {
            ModifyPlayerRank(1);
        }
        else
        {
            ModifyPlayerRank(-1);
        }
    }

    /// <summary>
    /// 評価値を指定された値で変動する
    /// </summary>
    /// <param name="rank">評価値の変動量</param>
    public void ModifyPlayerRank(int rank)
    {
        if (PlayerRank + rank < 1 || PlayerRank + rank > 5) return;
        PlayerRank += rank;
    }
}