using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class InvokeSystem : MonoBehaviour
{


    public event Action<float> Gettimer;


     private float time = 0f;   //test用変数
     
     [Header("現在の体力")]
    [SerializeField,Min(0)] private int currentHp = 1;
     
     [Header("現在のランゲージ")]
    [SerializeField,Min(0)] private int currentRunGauge = 1;
     
    [Header("現在の取得アイテム数")]
    [SerializeField,Min(0)] private int item = 1;

    [Header("インゲーム経過時間")]
    [SerializeField, Min(0)] private float timer = 0f;
    

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


   
}