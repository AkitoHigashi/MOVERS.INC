using System.Collections;
using UnityEditor;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    [SerializeField, Header("爆発のエフェクトが起きるまでの時間")]
    private float _explosionTime = 1;
    private void Awake()
    {
        StartCoroutine(Sound());
    }
    private IEnumerator Sound()
    {
        yield return new WaitForSeconds(_explosionTime);
        SEManager.SEPlay("MonsterDead");
    }
}
