using System.Collections;
using UnityEngine;

public class PotionGarbageCollecter : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerCarry _playerCarry;
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
        //_playerCarry.IsMuscleItem(true);
        yield return new WaitForSeconds(time);
        //_playerCarry.IsMuscleItem(true);
        yield break;
    }

    public void HealingPotion(HealingPotion heal)
    {
        _playerHealth.Heal(heal.Heal);
    }
}
