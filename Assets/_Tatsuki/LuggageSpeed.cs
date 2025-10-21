using System;
using UnityEngine;

public class LuggageSpeed : MonoBehaviour
{
    private Vector3 prevPosition;
    private Vector3 currentPosition;
    private Vector3 luggagevelocity;
    private float  totalSpeed  ;
  [SerializeField]  private bool Ishave = false; //アイテムをもっているときだけ計算する
    [SerializeField] private Rigidbody _rb;
    private void Start()
    {
        prevPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(Ishave)TakeLuggageSpeed();
        totalSpeed = luggagevelocity.magnitude + _rb.linearVelocity.magnitude;

    }

    public void TakeLuggageSpeed()
    {
        
        if (Mathf.Approximately(Time.deltaTime, 0)) 
            return;
        
        currentPosition = transform.position;
        
        luggagevelocity  = (currentPosition - prevPosition)/Time.deltaTime;
        
        Debug.Log(luggagevelocity.magnitude);
        prevPosition = currentPosition; 
    }
 

   
    public float GetTotalSpeed()=> totalSpeed;
   
}
