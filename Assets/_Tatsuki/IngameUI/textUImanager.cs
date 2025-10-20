using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class textUImanager : MonoBehaviour
{
    [SerializeField] private TMP_Text luggagetext; //現在荷物
    [SerializeField] private TMP_Text timertext;//タイマー
    [SerializeField] private InvokeSystem invokeSystem;//通知
    [SerializeField] private CollectionArea CollectionArea;
    


  
    private void OnEnable()
    {
       CollectionArea.OnEnterLuggage += LuggageSetText;
        CollectionArea.OnExitLuggage += LuggageSetText;
        invokeSystem.Gettimer +=  TimerSetText;
    }

    private void OnDisable()
    {
        CollectionArea.OnEnterLuggage -= LuggageSetText;
        CollectionArea.OnExitLuggage -= LuggageSetText;
        invokeSystem.Gettimer -= TimerSetText;
    }

    public void LuggageSetText(int text)
    {
        luggagetext.text = $"{text}/{UITestStatus.maxItem}";
    }

    public void TimerSetText(float ctx)
    {
        
            int minutes = (int)(ctx / 60);    // 分
            int seconds = (int)(ctx % 60);    // 秒（余り）
            timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 00:00形式で表示
        
    }
}