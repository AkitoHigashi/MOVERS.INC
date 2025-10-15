using System;
using UnityEngine;

public class InvokeSystem : MonoBehaviour
{
    public event Action<float> GetHp;
    public event Action<float> GetRunGauge;
    public event Action<int> GetLuggage;
 

    public  static int maxItem = 10;
    float time = 0f;
    float damage = 0.1f;
    int item = 1 ;
   
    
    
    private void Update()
    {
        
           time += Time.deltaTime;
        if (time > 1f)
        {
            GetHp?.Invoke(-damage);
            GetRunGauge?.Invoke(-damage);
            GetLuggage?.Invoke(item);
          
            time = 0f;
        }
    }
}
