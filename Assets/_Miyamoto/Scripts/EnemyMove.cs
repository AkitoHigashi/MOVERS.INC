using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̓����𐧌䂷��N���X
/// </summary>
public class EnemyMove : EnemyBase
{
    [SerializeField, Header("�ړI�n�̃��X�g")]
    private List<Transform> _destinations = new List<Transform>();
    [SerializeField, Header("�ړI�n�ɓ����������̑ҋ@����")]
    private float _waitTime;
    [SerializeField, Header("�ړI�n�Ɏ��_�����킹�鑬�x")]
    private float _angularSpeed;
    [SerializeField, Header("�~�܂�܂ł̋���")]
    private float _stopDistance;
    [SerializeField, Header("����p")]
    private float _angle;

    /// <summary>���݂̖ړI�n</summary>
    private Vector3 _currentDestination;
    /// <summary>�Ō�ɖK�ꂽ�ړI�n</summary>
    private Vector3 _lastDestination;
    /// <summary>�v���C���[�����������ǂ����̃t���O</summary>
    private bool hasSeenPlayer = false;

    protected override void Awake()
    {
        base.Awake();
        _currentDestination = _destinations[0].position;
        _lastDestination = _currentDestination;
        _navMeshAgent.angularSpeed = _angularSpeed;
    }
    private void Update()
    {
        //�ړI�n���Z�b�g����
        _navMeshAgent.SetDestination(_currentDestination);

        float distance = Vector3.Distance(this.transform.position, _currentDestination);

        // �v���C���[�������Ă��Ȃ��ꍇ�ŖړI�n�߂��ɂ���Ȃ玟�̖ړI�n��
        if (!_navMeshAgent.isStopped && !IsFind && distance <= 1)
        {
            StartCoroutine(ChangeDestination());
        }
        // �v���C���[�ǐՒ��͏���ύX�������~�߂�
        else if (IsFind)
        {
            StopCoroutine(ChangeDestination());
        }
    }
    /// <summary>
    /// �v���C���[�����E�ɓ��������ǂ������肵�Ēǔ�����
    /// </summary>
    /// <param name="playerCollider"></param>
    public void FindPlayer(Collider playerCollider)
    {
        Vector3 origin = transform.position;
        Vector3 toPlayer = (playerCollider.transform.position - origin).normalized;
        float targetAngle = Vector3.Angle(transform.forward, toPlayer);
        float distance = Vector3.Distance(this.transform.position, playerCollider.transform.position);

        bool raycastHitPlayer = false;//raycast���v���C���[�ɓ����������ǂ���

        // ����p���ɂ��邩����
        if (targetAngle < _angle / 2)
        {
            RaycastHit hit;
            // Raycast�ŏ�Q���̗L�����`�F�b�N����
            if (Physics.Raycast(origin, toPlayer, out hit, _enemyFov) && hit.collider == playerCollider)
            {
                raycastHitPlayer = true;
            }
        }

        if (raycastHitPlayer)
        {
            Debug.Log("�v���C���[�𔭌�");
            hasSeenPlayer = true;
            IsFind = true;
            _currentDestination = playerCollider.transform.position;//�v���C���[��W�I�ɃZ�b�g
            _navMeshAgent.isStopped = false;
            if (distance < 5)
            {
                //�����ɍU�������邩�ǂ������肷�鏈��
                Debug.Log("�U��");
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            // �v���C���[�����E����O�ꂽ���Ɍ���������
            if (hasSeenPlayer)
            {
                Debug.Log("�v���C���[�����E���猩������");
                hasSeenPlayer = false;
                IsFind = false;
                _currentDestination = _lastDestination; //���̖ړI�n�ɖ߂�
                _navMeshAgent.isStopped = false;
            }
        }
    }
    /// <summary>
    /// �ꎞ��~���ĖړI�n��ύX����
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeDestination()
    {
        _navMeshAgent.isStopped = true;
        Debug.Log("�ꎞ��~");
        yield return new WaitForSeconds(_waitTime);

        _lastDestination = _currentDestination;
        _currentDestination = _destinations[Random.Range(0, _destinations.Count)].position;
        _navMeshAgent.isStopped = false;
        Debug.Log("�ĊJ");
    }

    //�G�̎��E����������֐�(�K�v�ɉ����ăR�����g�A�E�g����)
    //private void OnDrawGizmos()
    //{
    //    if (_navMeshAgent == null) return;

    //    Vector3 origin = transform.position;
    //    Vector3 forward = transform.forward;
    //    float viewAngle = _angle; // ����p
    //    int segments = 20;

    //    // �F��ݒ�
    //    Gizmos.color = IsFind ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);

    //    // ��`���O�p�`�œh��Ԃ�
    //    for (int i = 0; i < segments; i++)
    //    {
    //        float angle1 = -_angle / 2 + (viewAngle * i / segments);
    //        float angle2 = -_angle / 2 + (viewAngle * (i + 1) / segments);

    //        Vector3 dir1 = Quaternion.Euler(0, angle1, 0) * forward * _enemyFov;
    //        Vector3 dir2 = Quaternion.Euler(0, angle2, 0) * forward * _enemyFov;

    //        // �O�p�`��`��
    //        Vector3[] vertices = new Vector3[] { origin, origin + dir1, origin + dir2 };

    //        // Gizmos�ŎO�p�`��h��Ԃ�
    //        DrawTriangle(vertices[0], vertices[1], vertices[2]);
    //    }
    //}
    //private void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    //{
    //    Gizmos.DrawLine(p1, p2);
    //    Gizmos.DrawLine(p2, p3);
    //    Gizmos.DrawLine(p3, p1);

    //    // ����������ς��ďd�˂邱�Ƃœh��Ԃ��̂悤�Ɍ�����
    //    for (float t = 0; t <= 1; t += 0.1f)
    //    {
    //        Vector3 a = Vector3.Lerp(p1, p2, t);
    //        Vector3 b = Vector3.Lerp(p1, p3, t);
    //        Gizmos.DrawLine(a, b);
    //    }
    //}
}