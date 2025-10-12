using UnityEngine;

public class PlayerCarry : MonoBehaviour, IStartSetVariables
{
    private Collider _playerCollider;
    private LuggageData _luggageData;
    private Transform _luggagePosition;
    private GameObject _target;
    private float _carryRayDistance;
    private string _luggageTag = "Luggage";
    private bool _isCarrying = false;

    private void Start()
    {
        _luggageData = GetComponent<LuggageData>();
        _playerCollider = GetComponent<Collider>();
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
                if (_target.CompareTag(_luggageTag))
                {
                    Rigidbody _targetRb = _target.GetComponent<Rigidbody>();
                    Collider _targetCollider = _target.GetComponent<Collider>();
                    //_luggageCollider = _target.GetComponent<Collider>();
                    // PlayerとLuggageのColliderが両方存在する場合、衝突を無視する
                    if (_playerCollider != null && _targetCollider != null)
                        Physics.IgnoreCollision(_playerCollider, _targetCollider, true);
                    _targetRb.linearVelocity = Vector3.zero;
                    _targetRb.angularVelocity = Vector3.zero;
                    _targetRb.angularDamping = 0;
                    _targetRb.Sleep();
                    _target.transform.SetParent(_luggagePosition);
                    _luggageData.LuggageGameObject = _target.gameObject;
                    _luggageData.LuggageRb = _target.GetComponent<Rigidbody>();
                    _luggageData.LuggageCollider = _target.GetComponent<Collider>();
                    _luggageData.LuggageRb.isKinematic = true;
                    _luggageData.LuggageRb.useGravity = false;
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
            if (_luggageData.LuggageRb != null)
            {
                _luggageData.LuggageRb.useGravity = true;
            }
            // PlayerとLuggageのColliderが両方存在する場合、衝突を再度有効にする
            if (_playerCollider != null && _luggageData.LuggageCollider != null)
                Physics.IgnoreCollision(_playerCollider, _luggageData.LuggageCollider, false);
            _luggageData.LuggageRb.isKinematic = false;
            _luggageData.LuggageGameObject.transform.SetParent(null);
            _isCarrying = false;
        }
    }

    public void CarryingBoolFalse() => _isCarrying = !_isCarrying;
    public bool ReturnIsCarrying() => _isCarrying;
}
