using UnityEngine;
using UnityEngine.Playables;

public class FadeoutTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _fadeTimeline;
    [SerializeField] private GameObject targetObject;

    public void Start()
    {
        targetObject.SetActive(false);
    }

    public void OnFadeButtonPressed()
    {
        targetObject.SetActive(true);
        _fadeTimeline.Play();
    }
}