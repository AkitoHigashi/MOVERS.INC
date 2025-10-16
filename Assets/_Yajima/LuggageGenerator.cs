using UnityEngine;
using System.Collections.Generic;
using System;

public class LuggageGenerator : MonoBehaviour
{
    [System.Serializable]
    class LuggageData
    {
        [SerializeField] int _generateValue;
        [SerializeField] LuggageList _luggageList;
        [SerializeField] List<Vector3> _generatePos;

        public int GenerateValue => _generateValue;
        public LuggageList LuggageList => _luggageList;
        public List<Vector3> GeneratePos => _generatePos;
    }

    [SerializeField] LuggageData _bigLuggage;
    [SerializeField] LuggageData _smallLuggage;
    List<Vector3> _setPos;

    private void Start()
    {
        LuggageGenerateAlgorithm(_smallLuggage);
        LuggageGenerateAlgorithm(_bigLuggage);
    }

    /// <summary>
    /// 荷物生成アルゴリズムを行う関数
    /// すでに荷物を置いた座標と候補の座標の距離の平均が最大となる場所に配置
    /// </summary>
    /// <param name="luggage">荷物生成に関するクラス</param>
    void LuggageGenerateAlgorithm(LuggageData luggage)
    {
        //荷物生成を行う関数
        Action<int> generate = index =>
        {
            int rand = UnityEngine.Random.Range(0, luggage.LuggageList.List.Count);
            Instantiate(luggage.LuggageList.List[rand].Prefab, luggage.GeneratePos[index], Quaternion.identity);
            //置いた場所を保存
            _setPos.Add(luggage.GeneratePos[index]);
            luggage.GeneratePos.RemoveAt(index);
        };

        //リストの初期化
        _setPos = new List<Vector3>();

        //一つ目の荷物は任意の場所を選んで生成
        int rand = UnityEngine.Random.Range(0, luggage.GeneratePos.Count);
        generate(rand);

        //指定の生成回数繰り返す
        for (int i = 0; i < luggage.GenerateValue; i++)
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
                foreach (var setPos in _setPos)
                {
                    distSum += Vector3.Distance(luggage.GeneratePos[j], setPos);
                }

                //候補場所に対して距離の平均を調べる
                distSum /= _setPos.Count;

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
            //荷物生成
            generate(index);
        }
    }
}

