using System;
using UnityEngine;

public class InvokeSystem : MonoBehaviour
{
    public event Action<float> GetHp;
    public event Action<float> GetRunGauge;

    float time = 0f;
    float damage = 0.1f;
    
    private void Update()
    {
        
           time += Time.deltaTime;
        if (time > 1f)
        {
            GetHp?.Invoke(-damage);
            GetRunGauge?.Invoke(-damage);
            time = 0f;
        }
    }
}
