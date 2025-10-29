using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class LuggageGenerator : MonoBehaviour
{
    [System.Serializable]
    public class LuggageData
    {
        [SerializeField] int _generateValue;
        [SerializeField] int _targetValue;
        [SerializeField] LuggageList _luggageList;
        [SerializeField] List<Transform> _generatePos;

        public int GenerateValue => _generateValue;
        public int TargetValue => _targetValue;
        public LuggageList LuggageList => _luggageList;
        public List<Transform> GeneratePos => _generatePos;

        /// <summary>
        /// 荷物の生成についての不正を検知する関数
        /// </summary>
        /// <returns>不正かどうか</returns>
        public bool CheckIrregular()
        {
            if (_generateValue < _targetValue)
            {
                Debug.LogWarning($"{_luggageList}:生成数が少なすぎます");
                return false;
            }

            if (_generatePos.Count < _generateValue)
            {
                Debug.LogWarning($"{_luggageList}:生成ポイントが少なすぎます");
                return false;
            }

            return true;
        }
    }

    [SerializeField] LuggageData _smallLuggage;
    [SerializeField] LuggageData _bigLuggage;


    private void Start()
    {
        //不正を検知
        if (CheckPositionDupulicate(_smallLuggage, _bigLuggage))
        {
            LuggageGenerateAlgorithm(_smallLuggage);
            LuggageGenerateAlgorithm(_bigLuggage);
        }
    }

    public int GetTargetValue()
    {
        return _smallLuggage.TargetValue + _bigLuggage.GenerateValue;
    }

    /// <summary>
    /// 生成座標の被りがないかを確認する関数
    /// </summary>
    /// <param name="smallLug">小さい荷物のクラス</param>
    /// <param name="bigLug">大きい荷物のクラス</param>
    /// <returns>重複があるかどうか</returns>
    bool CheckPositionDupulicate(LuggageData smallLug, LuggageData bigLug)
    {
        //生成ポイントの数の多さによってリストの保持を分ける
        List<Transform> shortList = smallLug.GeneratePos.Count < bigLug.GeneratePos.Count ? smallLug.GeneratePos : bigLug.GeneratePos;
        List<Transform> largeList = smallLug.GeneratePos.Count < bigLug.GeneratePos.Count ? bigLug.GeneratePos : smallLug.GeneratePos;

        //結果
        bool result = true;
        foreach (var l in largeList)
        {
            foreach (var s in shortList)
            {
                if (l.position == s.position)
                {
                    Debug.LogWarning($"{l}が被っています");
                    result = false;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 指定の荷物を決定する関数
    /// </summary>
    /// <param name="luggage">荷物生成に関するクラス</param>
    /// <returns>重複なしの指定の荷物の決定リスト</returns>
    HashSet<int> MakeTargetNumbers(LuggageData luggage)
    {
        HashSet<int> result = new HashSet<int>();
        //指定の荷物を決定するまで繰り返す
        while (result.Count < luggage.TargetValue)
        {
            int rand = UnityEngine.Random.Range(0, luggage.GenerateValue);
            result.Add(rand);
        }

        return result;
    }

    /// <summary>
    /// 荷物生成アルゴリズムを行う関数
    /// すでに荷物を置いた座標と候補の座標の距離の平均が最大となる場所に配置
    /// </summary>
    /// <param name="luggage">荷物生成に関するクラス</param>
    void LuggageGenerateAlgorithm(LuggageData luggage)
    {
        //不正を検知
        if (!luggage.CheckIrregular()) return;

        //リストの初期化
        List<Vector3> setPosition = new List<Vector3>();

        //指定の荷物の決定を受け取る
        HashSet<int> targetHash = MakeTargetNumbers(luggage);

        //荷物生成を行う関数
        Action<int, bool> generate = (index, target) =>
        {
            int rand = UnityEngine.Random.Range(0, luggage.LuggageList.List.Count);
            var go = Instantiate(luggage.LuggageList.List[rand].Prefab, luggage.GeneratePos[index].position, Quaternion.identity);
            if (target)
            {
                //指定の荷物の時はパーティクルを子オブジェクトにする
                Instantiate(luggage.LuggageList.List[rand].Particle, go.transform);
                //go.transform.localScale = Vector3.one * 0.5f;
                Debug.Log("Particle");
            }
            //置いた場所を保存
            setPosition.Add(luggage.GeneratePos[index].position);
            luggage.GeneratePos.RemoveAt(index);
        };

        //一つ目の荷物を生成する場所を選ぶ
        int rand = UnityEngine.Random.Range(0, luggage.GeneratePos.Count);
        //指定の荷物かどうかを判定
        bool target = targetHash.Contains(0) ? true : false;
        //荷物生成
        generate(rand, target);

        //指定の生成回数繰り返す
        for (int i = 1; i < luggage.GenerateValue; i++)
        {
            //生成座標のインデックス
            int index = -1;
            //距離の平均の大きいものを保持する変数
            float graterDist = 0;

            for (int j = 0; j < luggage.GeneratePos.Count; j++)
            {
                //候補場所に対する距離の合計
                float distSum = 0;

                //距離の合計を求める
                foreach (var setPos in setPosition)
                {
                    distSum += Vector3.Distance(luggage.GeneratePos[j].position, setPos);
                }

                //候補場所に対して距離の平均を調べる
                distSum /= setPosition.Count;

                //保持した距離の平均よりも調べた距離の平均の方が大きいとき
                if (graterDist < distSum)
                {
                    //数値の更新
                    graterDist = distSum;
                    index = j;
                }
            }

            //インデックス外の時は飛ばす
            if (index == -1) continue;
            //指定の荷物かどうかを判定
            target = targetHash.Contains(i) ? true : false;
            //荷物生成
            generate(index, target);
        }
    }
}

