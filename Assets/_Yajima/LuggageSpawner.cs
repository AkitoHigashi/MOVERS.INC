using UnityEngine;
using System.Collections.Generic;

public class LuggageSpawner : MonoBehaviour
{
    [System.Serializable]
    class LuggageData
    {
        [SerializeField] GameObject _luggage;
        [SerializeField] int _spawnRate = 1;

        public GameObject Luggage => _luggage;
        public int SpawnRate => _spawnRate;
    }

    [SerializeField] List<LuggageData> _luggageList;
    [SerializeField] List<Vector3> _spawnPosition;

    /// <summary>
    /// 重み付き確率を採用した荷物自動生成関数
    /// </summary>
    void SpawnLuggage()
    {
        //重みの総和を計算
        int maxValue = 0;
        foreach (var luggage in _luggageList)
        {
            maxValue += luggage.SpawnRate;
        }

        //生成する荷物の決定
        int randValue = Random.Range(0, maxValue);
        int currentValue = 0;
        foreach (var luggage in _luggageList)
        {
            //重みの蓄積
            currentValue += luggage.SpawnRate;
            if (randValue <= currentValue)
            {
                //乱数値よりも蓄積された重みの方が大きくなった時
                int randPosition = Random.Range(0, _spawnPosition.Count);
                Instantiate(luggage.Luggage, _spawnPosition[randPosition], Quaternion.identity);
                _spawnPosition.RemoveAt(randPosition);
                break;
            }
        }
    }
}

