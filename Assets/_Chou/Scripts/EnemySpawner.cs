using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private float _globalTimer = 0;
    private float _spawnTimer = 0;
    private float _frequencyTimer = 0;
    private float _weightTimer = 0;
    private int _monsterCount = 0;

    [Header("UI")]
    [SerializeField] private Text _uiTimer;
    [SerializeField] private Text _uiMonsterCount;
    [SerializeField] private GameObject _uiFrequencyUp;
    [SerializeField] private GameObject _uiDangerUp;

    [Header("パラメータ")]
    [SerializeField, Tooltip("モンスター最大数")] private int _monsterCountMax;
    [SerializeField, Tooltip("モンスター生成間隔初期値")] private float _spawnInterval;
    [SerializeField, Range(-60, 0), Tooltip("モンスター生成間隔減少量")] private float _spawnIntervalDecrement;
    [SerializeField, Tooltip("モンスター生成間隔最小値")] private float _spawnIntervalMin;
    [SerializeField, Tooltip("モンスター生成頻度上昇間隔")] private float _frequencyIncreaseInterval;
    [SerializeField, Tooltip("モンスター生成確率更新間隔")] private float _weightUpdateInterval;

    [Header("参照など")]
    [SerializeField, Tooltip("プレイヤー")] private Transform _player;
    [SerializeField, Tooltip("生成阻止区域半径")] private float _preventSpawnRadius;
    [SerializeField, Tooltip("生成位置リスト")] private List<SpawnPositionController> _spawnPoints;
    [SerializeField, Tooltip("生成モンスターリスト")] private List<TempMonsterData> _monsterData;

    #region LifeCycle
    private void Update()
    {
        _globalTimer += Time.deltaTime;
        _spawnTimer += Time.deltaTime;
        _frequencyTimer += Time.deltaTime;
        _weightTimer += Time.deltaTime;
        UpdateUITimer();

        // モンスター生成
        if(_spawnTimer >= _spawnInterval)
        {
            SpawnMonster();
            UpdateUIMonsterCount();
            _spawnTimer = 0;
        }

        // モンスター生成頻度上昇
        if(_frequencyTimer >= _frequencyIncreaseInterval)
        {
            UpdateSpawnInterval();
            _frequencyTimer = 0;
        }

        // モンスター生成危険度上昇
        if(_weightTimer >= _weightUpdateInterval)
        {
            UpdateMonsterRandomWeight();
            _weightTimer = 0;
        }

        // 右クリックでモンスター数を減らす
        if (Input.GetMouseButtonDown(1))
        {
            _monsterCount -= 1;
            if (_monsterCount < 0)
            {
                _monsterCount = 0;
            }
            UpdateUIMonsterCount();
        }
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    /// <summary>
    /// 時間UIを更新する
    /// ※一時的。UI更新ここで書きたくない
    /// </summary>
    private void UpdateUITimer()
    {
        _uiTimer.text = string.Format("{0:D2}:{1:D2}",
            (int)(_globalTimer / 60),  // 分
            (int)(_globalTimer % 60)); // 秒
    }

    /// <summary>
    /// UIモンスター数の表示を更新
    /// ※一時的。テスト用
    /// </summary>
    private void UpdateUIMonsterCount()
    {
        _uiMonsterCount.text = "Monster: " + _monsterCount + "/" + _monsterCountMax;
    }

    /// <summary>
    /// 生成頻度上昇メッセージを表示する
    /// ※一時的。テスト用
    /// </summary>
    private IEnumerator FrequencyUpNotification()
    {
        _uiFrequencyUp.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _uiFrequencyUp.SetActive(false);
    }
    /// <summary>
    /// 危険度上昇メッセージを表示する
    /// ※一時的。テスト用
    /// </summary>
    private IEnumerator DangerUpNotification()
    {
        _uiDangerUp.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _uiDangerUp.SetActive(false);
    }
    /// <summary>
    /// モンスター生成処理
    /// </summary>
    private void SpawnMonster()
    {
        if (_monsterCount >= _monsterCountMax)
        {
            Debug.Log("モンスター最大数を達しているため、モンスターは生成しない。");
            return;
        }

        // モンスター生成位置を取得
        SpawnPositionController pos = GetSpawnPosition();
        if (pos == null)
        {
            Debug.LogWarning("モンスター生成位置を取得できなかったため、モンスターは生成しない。");
            return;
        }

        // モンスター生成処理
        bool spawnResult = pos.SpawnMonster(GetRandomMonster());
        if (spawnResult)
        {
            _monsterCount++;
        }
    }

    /// <summary>
    /// 指定された生成位置の付近にプレイヤーが居るか判定する
    /// </summary>
    /// <param name="spawnPos">生成位置</param>
    /// <param name="playerPos">プレイヤー</param>
    /// <returns></returns>
    private bool IsPlayerNearby(SpawnPositionController spawnPos, Transform playerPos)
    {
        if(Vector3.Distance(spawnPos.transform.position, playerPos.position) < _preventSpawnRadius)
        {
            Debug.Log("プレイヤーは近くにいる。改めて生成位置を選定する。");
            spawnPos.SkipSpawn();
            return true;
        }
        return false;
    }

    private SpawnPositionController GetSpawnPosition()
    {
        // 無限ループ防止のためループカウンターを設ける
        int loopCount = 0;
        int loopMax = 1000;
        while (true)
        {
            // 生成位置を選定する
            int index = Random.Range(0, _spawnPoints.Count);
            SpawnPositionController pos = _spawnPoints[index];
            // プレイヤーが近くにいるか判定する
            bool isPlayerNearby = IsPlayerNearby(pos, _player);
            if (!isPlayerNearby)
            {
                // 近くにプレイヤーがいない場合、生成位置を決定する
                return pos;
            }
            // ループ回数をカウントする。最大値を超えたらループ終了
            if(loopCount < loopMax)
            {
                loopCount++;
            }
            else
            {
                Debug.LogWarning("ループ回数に異常を検出した！！");
                return null;
            }
        }
    }
    /// <summary>
    /// ランダムで生成するモンスターを取得する
    /// モンスターが各自持っている重みに基づいた乱数生成アルゴリズムあり
    /// どう解説するか悩んで禿げたので諦めた。解説はこちら：https://ekifurulab.com/weighted/
    /// </summary>
    /// <returns>選定されたモンスター</returns>
    private TempMonsterData GetRandomMonster()
    {
        float totalWeight = 0;
        foreach(TempMonsterData monster in _monsterData)
        {
            totalWeight += monster.RandomWeight;
        }

        float randomWeight = Random.Range(0, totalWeight);
        float currentWeight = 0;
        foreach(TempMonsterData monster in _monsterData)
        {
            currentWeight += monster.RandomWeight;
            if (currentWeight > randomWeight) return monster;
        }
        return null;
    }

    /// <summary>
    /// 生成モンスターの乱数重みを更新する
    /// </summary>
    private void UpdateMonsterRandomWeight()
    {
        StartCoroutine(DangerUpNotification());
        foreach (TempMonsterData monster in _monsterData)
        {
            monster.IncreaseRandomWeight();
        }
    }
    /// <summary>
    /// モンスター生成頻度の更新
    /// </summary>
    private void UpdateSpawnInterval()
    {
        if (_spawnInterval <= _spawnIntervalMin) return;
        StartCoroutine(FrequencyUpNotification());
        _spawnInterval += _spawnIntervalDecrement;
    }

    #endregion
}
/// <summary>
/// 一時的なMonsterData
/// </summary>
[Serializable]
public class TempMonsterData
{
    /// <summary>乱数重み</summary>
    public float RandomWeight;
    /// <summary>重み更新するごとの加算値</summary>
    public float WeightIncrement;
    public Material Material;

    /// <summary>
    /// 乱数重みを加算する
    /// </summary>
    public void IncreaseRandomWeight()
    {
        RandomWeight += WeightIncrement;
    }
}
