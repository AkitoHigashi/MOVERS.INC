
using UnityEngine;


/// <summary>
/// 集金をするoriを起動させるボタンクラス
/// </summary>
public class RedButton : InteractBase
{
   private Fall _redButton;
   private Animator _animator;
   InGameTextUIManager  _textUImanager;

   private void Start()
   {
      _redButton = FindAnyObjectByType<Fall>();
      _animator = GetComponentInChildren<Animator>();
      _textUImanager = FindAnyObjectByType<InGameTextUIManager>();
      _textUImanager.LuggageSetText();
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
