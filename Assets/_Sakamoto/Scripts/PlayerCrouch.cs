using UnityEngine;

public class PlayerCrouch : MonoBehaviour,IStartSetVariables
{
    private CapsuleCollider _capsuleCollider;
    private float _startHeight;
    private float _crouchHeight;
    private bool _isCrouching = false;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _startHeight = _capsuleCollider.height;
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _crouchHeight = playerData.CouchHeight;
    }

    public void StartCrouch()
    {
        _capsuleCollider.height = _crouchHeight;
        _isCrouching = true;
    }

    public void StopCrouch()
    {
        _capsuleCollider.height = _startHeight;
        _isCrouching = false;
    }

    public bool ReturnIsCrouch() => _isCrouching;
}
