using System.Xml;
using TMPro;
using UnityEngine;
public class TatukiScore : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    [SerializeField] int NowScore = 0;
    [SerializeField] int EndScore = 0;
    private  bool isEnd = false;

    public int  nowScore => NowScore;
    public void SetScore(int score)
    {
        if(!isEnd)NowScore+=score;
    }
    public void SetText(string message)
    {
        if(!isEnd)
        tmpText.text = message;
    }
    public void SetEndScore(bool end)=> isEnd = end; 
    
}


