using UnityEngine;

public class PlayerCarry : MonoBehaviour, IStartSetVariables
{
    public bool IsCarrying => _isCarrying;
    private bool _isCarrying = false;
    private Collider _playerCollider;
    private LuggageData _luggageData;
    private Transform _luggagePosition;
    private GameObject _target;
    private float _carryRayDistance;
    private float _collisionCheckDelay;
    private float _carryStartTime;
    private string _luggageTag = "Luggage";
    private string _itemTag = "Item";
    private LayerMask _carryIgnoreLayer;

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
        _carryIgnoreLayer = playerData.CarryIgnoreLayer;
        _collisionCheckDelay = playerData.CollisionCheckDelay;
    }

    private void Update()
    {
        if (_isCarrying && _luggageData.LuggageGameObject != null)
        {
            if (Time.time - _carryStartTime > _collisionCheckDelay)
            {
                CheckLuggageCollision();
            }
        }
    }

    private void CheckLuggageCollision()
    {
        Collider luggageCollider = _luggageData.LuggageCollider;
        if (luggageCollider == null) return;

        // 荷物のColliderの中心と半径を取得
        Vector3 center = luggageCollider.bounds.center;
        Vector3 halfExtents = luggageCollider.bounds.extents;
        // プレイヤーレイヤーと指定された無視レイヤーを除外
        int layerMask = ~(LayerMask.GetMask("Player") | _carryIgnoreLayer);
        // 荷物のColliderと重なっている他のColliderを検出
        Collider[] hitColliders = Physics.OverlapBox(
            center,
            halfExtents,
            luggageCollider.transform.rotation,
            layerMask
        );
        // 重なっているColliderの中にPlayer以外のものがあるか確認
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != _playerCollider &&
                hitCollider != luggageCollider)
            {
                Debug.Log($"荷物が{hitCollider.name}に接触したため離します");
                // ForceDropLuggage();
                break;
            }
        }
    }

    private void ForceDropLuggage()
    {
        if (_luggageData.LuggageRb != null)
        {
            _luggageData.LuggageRb.useGravity = true;
        }
        if (_playerCollider != null && _luggageData.LuggageCollider != null)
            Physics.IgnoreCollision(_playerCollider, _luggageData.LuggageCollider, false);

        _luggageData.LuggageRb.isKinematic = false;
        _luggageData.LuggageGameObject.transform.SetParent(null);
        _luggageData.LuggageScript = null;
        _isCarrying = false;
    }

    public void CarryAction()
    {
        //if (_isCarrying == true && _luggageData.LuggageGameObject == null && _luggageData.LuggageRb == null && _luggageData.LuggageScript == null)
        //{
        //    CarryingBoolFalse();
        //    Debug.Log(_isCarrying);
        //}
        if (!_isCarrying)
        {
            // カメラのビューポート中心からRayを生成
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            // Rayを飛ばして、何かに当たったら情報を取得
            if (Physics.Raycast(ray, out hit, _carryRayDistance))
            {
                _target = hit.collider.gameObject;
                if (_target.CompareTag(_luggageTag) || _target.CompareTag(_itemTag))
                {
                    Collider _targetCollider = _target.GetComponent<Collider>();
                    Rigidbody _targetRb = _target.GetComponent<Rigidbody>();
                    if (_target.TryGetComponent<Luggage>(out var luggageScript))
                    {
                        _luggageData.LuggageScript = luggageScript;
                    }
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
                    _carryStartTime = Time.time;
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
            _luggageData.LuggageGameObject = null;
            _luggageData.LuggageScript = null;
            _isCarrying = false;
        }
    }

    public ItemBase ReturnLuggageItemBase()
    {
        if (_luggageData.LuggageGameObject != null &&
            _luggageData.LuggageGameObject.TryGetComponent<ItemBase>(out var itemBase))
        {
            return itemBase;
        }
        return null;
    }

    public void CarryingBoolFalse() => _isCarrying = !_isCarrying;
    public bool ReturnIsCarrying() => _isCarrying;
}
