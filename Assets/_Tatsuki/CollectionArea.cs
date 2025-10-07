using System;
using UnityEngine;

public class CollectionArea : MonoBehaviour
{
    public event Action<GameObject> OnEnter;
    public event Action<GameObject> OnExit;
    [SerializeField] TatukiScore _score;

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            var score = other.gameObject.GetComponent<TatukiItem>();
            _score.SetScore(score.Score);
            _score.SetText(_score.nowScore.ToString());
            OnEnter?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            var score = other.gameObject.GetComponent<TatukiItem>();
            _score.SetScore(-score.Score);
            _score.SetText(_score.nowScore.ToString());
            OnExit?.Invoke(other.gameObject);
        }
    }
}
