using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class InvokeSystem : MonoBehaviour
{
    public event Action<float> GetHp;
    public event Action<float> GetRunGauge;
    public event Action<int> GetLuggage;


     private float time = 0f;   
     [Header("現在の体力")]
    [SerializeField,Min(0)] private int currentHp = 1;
     
     [Header("現在のランゲージ")]
    [SerializeField,Min(0)] private int currentRunGauge = 1;
     
    [Header("現在の取得アイテム数")]
    [SerializeField,Min(0)] private int item = 1;
    

    public int StatusValue
    {
        get => currentHp; 
        set => currentHp = Mathf.Max(0, value);
    }

    public int Item
    {
        get => item;
        set => item = Mathf.Max(0, value);
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            GetHp?.Invoke(currentHp);
            GetRunGauge?.Invoke(currentRunGauge);
            GetLuggage?.Invoke(item);

            time = 0f;
        }
    }
}