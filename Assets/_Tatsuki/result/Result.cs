using UnityEngine;
using TMPro;
public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int score;

    private void Start()
    {
        score = ScoreManager.GetScore();
        _text.text = string.Format("Score{0},", score);
    }
}
