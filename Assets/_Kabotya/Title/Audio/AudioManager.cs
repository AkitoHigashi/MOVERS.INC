using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Update()
    {
        float value = _slider.value;
        Debug.Log("Slider Value: " + value);
    }
}
