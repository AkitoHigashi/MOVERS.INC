using UnityEngine;

public class PlayerCarry : MonoBehaviour, IStartSetVariables
{
    private LuggageData _luggageData;
    private Transform _luggagePosition;
    private GameObject _target;
    private float _carryRayDistance;
    private string _luggageTag = "Luggage";
    private bool _isCarrying = false;

    private void Start()
    {
        _luggageData = GetComponent<LuggageData>();
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _luggagePosition = playerData.LuggagePosition;
        _carryRayDistance = playerData.CarryRayDistance;
        _luggageTag = playerData.LuggageTag;
    }

    public void CarryAction()
    {
        if (!_isCarrying)
        {
            // カメラのビューポート中心からRayを生成
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            // Rayを飛ばして、何かに当たったら情報を取得
            if (Physics.Raycast(ray, out hit, _carryRayDistance))
            {
                _target = hit.collider.gameObject;
                if (_target.tag == _luggageTag)
                {
                    _target.transform.SetParent(_luggagePosition);
                    _luggageData.Luggage = _target.gameObject;
                    _luggageData.LuggageRb = _target.GetComponent<Rigidbody>();
                    _isCarrying = true;
                }
            }
            else
            {
                Debug.Log("Rayが何も拾いませんでした");
            }
        }
        else
        {
            _luggageData.Luggage.transform.SetParent(null);
            _isCarrying = false;
        }
    }

    public bool ReturnIsCarrying() => _isCarrying;
}
