using UnityEngine;

public class FadeInTrigger : MonoBehaviour
{
   [SerializeField] private Animator _fadeAnimator;
   [SerializeField] private GameObject targetObject;

    private void Awake()
    {
        OnFadeInButtonPressed();
    }
    public void OnFadeInButtonPressed()
    {
        targetObject.SetActive(true);
        _fadeAnimator.SetTrigger("FadeIn");
    }

    public void OffFadeInObject()
    {
        targetObject.SetActive(false);
    }

}
