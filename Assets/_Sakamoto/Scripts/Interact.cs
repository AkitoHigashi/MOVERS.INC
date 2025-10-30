using UnityEngine;
using static LuggageGenerator;

public class Interact : MonoBehaviour, IStartSetVariables
{
    private float _interactStartTime;
    private float _interactTime;
    private float _interactDistance;

    LuggageData _luggage;
    PlayerCarry _carry;
    InteractBase _startInteract, _endInteract;

    private void Start()
    {
        _luggage = GetComponent<LuggageData>();
        _carry = GetComponent<PlayerCarry>();
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _interactTime = playerData.InteractTime;
        _interactDistance = playerData.InteractDistance;
    }

    public void StartInteract()
    {
        _interactStartTime = Time.time;
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
        if (Time.time - _interactStartTime >= _interactTime)
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
        Debug.Log($"{Time.time - _interactStartTime >= _interactTime}");
        interact.Interact();
    }
}
