
using UnityEngine;


/// <summary>
/// 集金をするoriを起動させるボタンクラス
/// </summary>
public class RedButton : InteractBase
{
   private FallButton _redButton;
   private Animator _animator;

   private void Start()
   {
      _redButton = FindAnyObjectByType<FallButton>();
      _animator = GetComponentInChildren<Animator>();
   }

   public override void Interact()
   {
      _animator.SetTrigger("push");
      StartCoroutine(_redButton.FallAndReturn());
      Debug.Log("oriDown");
   }

   public override void DemolishedLuggage()
   {
      throw new System.NotImplementedException();
   }

   public override void PutLuggage(Collision collision)
   {
      throw new System.NotImplementedException();
   }
}
