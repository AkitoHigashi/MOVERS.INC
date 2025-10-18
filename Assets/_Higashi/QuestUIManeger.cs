using UnityEngine;
using UnityEngine.UI;

public class QuestUIManeger : MonoBehaviour
{
    [SerializeField,Header("クエストの情報")] QuestData _questData;
    [SerializeField] Image _questImage;//クエストの内部イメージ
    [SerializeField] Text _questTitle;//
    [SerializeField] Text _questDescription;
    [SerializeField] Text _requiredDeliveryCount;
    [SerializeField] Text _reward;
    [SerializeField] Image _trapIcon;

    [SerializeField] Canvas _canvas;
    //[SerializeField] Image _monsterIcon;

    private void Start()
    {
        _questImage.sprite = _questData.QuestImage;//クエストの
        _questTitle.text = _questData.QuestName;
        _questDescription.text = _questData.QuestDescription;
        _requiredDeliveryCount.text = $"指定荷物の数　{_questData.RequiredDeliveryCount.ToString()}個";
        _reward.text = $"推定獲得報酬金額　{ _questData.Reward.ToString()}";
        _trapIcon.sprite = _questData.Traps[0].TrapIcon;
      //= _questData.Traps[0].IsPlacedInQuset;
    }

    public void Back()
    {
        _canvas.gameObject.SetActive(false);
    }

}
