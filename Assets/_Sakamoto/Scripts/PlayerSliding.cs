using UnityEngine;


public class PlayerSliding : MonoBehaviour, IStartSetVariables
{
    private Rigidbody _rb;
    private CapsuleCollider _capsuleCollider;
    private float _startYScale;
    private float _slidingYScale;
    private float _slidingTimer = 0f;
    private float _slidingCurrentTimer;
    private bool _isSliding = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _startYScale = _capsuleCollider.height;
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _slidingCurrentTimer = playerData.SlidingTimer;
        _slidingTimer = playerData.SlidingTimer;
        _slidingYScale = playerData.SlidingYScale;
    }

    private void Update()
    {
        if (_isSliding) UpdateSlidingTimer();
    }

    public void StartSliding()
    {
        if (_isSliding) return;

        _isSliding = true;
        _slidingTimer = _slidingCurrentTimer;
        _capsuleCollider.height = _slidingYScale;
        _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    public void StopSliding()
    {
        _isSliding = false;
        _capsuleCollider.height = _startYScale;
    }

    /// <summary>
    /// スライディングタイマーの更新
    /// </summary>
    private void UpdateSlidingTimer()
    {
        if (_rb.linearVelocity.y > -0.1f)
        {
            _slidingTimer -= Time.deltaTime;
        }
        if (_slidingTimer <= 0f)
        {
            StopSliding();
        }
    }

    public bool CanSliding(bool isSprint, bool IsGround, Vector2 input)
        => isSprint && IsGround && input.magnitude > 0.1f && !_isSliding;

    public bool ReturnIsSliding() => _isSliding;
}
