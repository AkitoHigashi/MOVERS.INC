using UnityEngine;

public class FadeoutTrigger : MonoBehaviour
{
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private GameObject targetObject;

    public void OnFadeButtonPressed()
    {
        targetObject.SetActive(true);
        _fadeAnimator.SetTrigger("Fade");
    }
}