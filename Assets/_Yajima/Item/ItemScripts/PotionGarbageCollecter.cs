using System.Collections;
using UnityEngine;

public class PotionGarbageCollecter : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerCarry;
    Coroutine _muscleCor;
    static PotionGarbageCollecter _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MusclePotion(MusclePotion muscle)
    {
        if (_muscleCor != null)
        {
            StopCoroutine(_muscleCor);
            _muscleCor = null;
        }

        if (_muscleCor == null)
        {
            _muscleCor = StartCoroutine(MusclePotionCoroutine(muscle.EffectiveTime));
        }
    }

    IEnumerator MusclePotionCoroutine(float time)
    {
        Debug.Log("筋力増強開始");
        _playerCarry.IsMuscleItem(true);
        yield return new WaitForSeconds(time);
        Debug.Log("筋力増強終了");
        _playerCarry.IsMuscleItem(false);
        yield break;
    }

    public void HealingPotion(HealingPotion heal)
    {
        Debug.Log($"HPを{heal.Heal}回復");
        _playerHealth.Heal(heal.Heal);
    }
}
