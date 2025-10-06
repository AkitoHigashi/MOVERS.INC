using UnityEngine;
using UnityEngine.Playables;

public class FadeoutTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _fadeTimeline;

    public void OnFadeButtonPressed()
    {
        _fadeTimeline.Play();
    }
}