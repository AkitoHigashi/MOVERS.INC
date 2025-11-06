using UnityEngine;
using TMPro;

public class TextUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _luggagetext; //現在荷物
    [SerializeField] private TMP_Text _timertext;//タイマー

    [SerializeField] private CollectionArea _collectionArea;
    [SerializeField] private StatusNotifer _statusNotifer;
    private int _count = 0; //荷物の出し入れをカウントする



    private void OnEnable()
    {
        _collectionArea.OnEnterLuggage += LuggageInput;
        _collectionArea.OnExitLuggage += LuggageInput;
    }

    private void OnDisable()
    {
        _collectionArea.OnEnterLuggage -= LuggageInput;
        _collectionArea.OnExitLuggage -= LuggageInput;
    }

    public void LuggageInput(int text)
    {
        Debug.Log(text);
        _count += text;
   
    }

    public void LuggageSetText()
    {
        _luggagetext.text = $"{_count}/{_statusNotifer.MaxItem}";
    }

    public void TimerSetText(float ctx)
    {

        int minutes = (int)(ctx / 60);    // 分
        int seconds = (int)(ctx % 60);    // 秒（余り）
        _timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 00:00形式で表示

    }
}