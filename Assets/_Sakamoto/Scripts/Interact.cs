using UnityEngine;

public class Interact : MonoBehaviour, IStartSetVariables
{
    private float _interactCurrentTime;
    private float _interactTime;
    private float _interactDistance;

    private bool _isInteracting = false;
    public bool IsInteracting => _isInteracting;
    public float InteractCurrentTime => _interactCurrentTime;
    public float InteractTime => _interactTime;

    public float InteractProgress => Mathf.Clamp01(_interactCurrentTime / _interactTime);

    PlayerCarry _carry;
    InteractBase _startInteract, _endInteract;

    private void Start()
    {
        _carry = GetComponent<PlayerCarry>();
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _interactTime = playerData.InteractTime;
        _interactDistance = playerData.InteractDistance;
    }

    public void StartInteract()
    {
        _interactCurrentTime = Time.time;
        Debug.Log("StartInteract");
        if (!_carry.IsCarrying)
        {
            // カメラのビューポート中心からRayを生成
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            // Rayを飛ばして、何かに当たったら情報を取得
            if (Physics.Raycast(ray, out hit, _interactDistance))
            {
                var _target = hit.collider.gameObject;
                Debug.Log(_target);
                if (_target.TryGetComponent<InteractBase>(out var interact))
                {
                    _startInteract = interact;
                    _isInteracting = true;
                }
            }
            else
            {
                Debug.Log("Rayが何も拾いませんでした");
            }
        }
    }

    public void StopInteract()
    {
        _isInteracting = false;
        if (Time.time - _interactCurrentTime >= _interactTime)
        {
            if (!_carry.IsCarrying)
            {
                // カメラのビューポート中心からRayを生成
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                // Rayを飛ばして、何かに当たったら情報を取得
                if (Physics.Raycast(ray, out hit, _interactDistance))
                {
                    var _target = hit.collider.gameObject;
                    if (_target.TryGetComponent<InteractBase>(out var interact))
                    {
                        _endInteract = interact;
                    }
                }
                else
                {
                    Debug.Log("Rayが何も拾いませんでした");
                }
            }
            if (_startInteract == _endInteract)
            {
                InteractExecution(_endInteract);
            }
        }

    }

    private void InteractExecution(InteractBase interact)
    {
        Debug.Log($"{Time.time - _interactCurrentTime >= _interactTime}");
        interact?.Interact();//nullなら呼ばれない。
    }
}
