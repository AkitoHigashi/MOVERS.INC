using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̊��N���X
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    public List<Transform> Destinations => _destinations;
    public float WaitTime => _waitTime;
    public float AngularSpeed => _angularSpeed;
    public float StopDistance => _stopDistance;
    public float Angle => _angle;
    public float Magnification => _magnification;
    public float EnemyHP => _enemyHp;
    public float EnemyMoveSpeed => _enemyMoveSpeed;
    public float EnemyFov => _enemyFov;
    public float EnemyPower => _enemyPower;
    public float EnemyAttackRange => _enemyAttackRange;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Vector3 CurrentDestination => _currentDestination;
    public Vector3 LastDesination => _lastDestination;
    public bool HasSeen => _hasSeen;

    [SerializeField, Header("�G�̃f�[�^")]
    protected EnemyData _enemyData;
    [SerializeField, Header("�ړI�n�̃��X�g")]
    protected List<Transform> _destinations = new List<Transform>();
    [SerializeField, Header("�ړI�n�ɓ����������̑ҋ@����")]
    protected float _waitTime;
    [SerializeField, Header("�ړI�n�Ɏ��_�����킹�鑬�x")]
    protected float _angularSpeed;
    [SerializeField, Header("�~�܂�܂ł̋���")]
    protected float _stopDistance = 5f;
    [SerializeField, Header("����p")]
    protected float _angle;
    [SerializeField, Header("�I�u�W�F�N�g������������Fov�̊g��{��")]
    protected float _magnification;

    //�X�e�[�^�X
    protected float _enemyHp;
    protected float _enemyMoveSpeed;
    protected float _enemyFov;
    protected float _enemyPower;
    protected float _enemyAttackRange;

    /// <summary>�v���C���[�����������ǂ����̃t���O</summary>
    protected bool _hasSeen = false;
    protected EnemyState _currentEnemyState;
    /// <summary>���݂̖ړI�n</summary>
    protected Vector3 _currentDestination;
    /// <summary>�Ō�ɖK�ꂽ�ړI�n</summary>
    protected Vector3 _lastDestination;
    protected NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetParameter();
        VisionGenerator();
    }
    private void Update()
    {
        Patrol();
    }
    /// <summary>
    /// �G�̏����l��ݒ肷��
    /// </summary>
    private void SetParameter()
    {
        _enemyHp = _enemyData.EnemyHpData;
        _enemyMoveSpeed = _enemyData.EnemyMoveSpeedData;
        _enemyFov = _enemyData.EnemyFoVData;
        _enemyPower = _enemyData.EnemyPowerData;
        _enemyAttackRange = _enemyData.EnemyAttackRangeData;
        _currentEnemyState = _enemyData.EnemyStateData;
        _navMeshAgent.speed = _enemyMoveSpeed;
        _currentDestination = _destinations[0].position;
        _lastDestination = _currentDestination;
        _navMeshAgent.angularSpeed = _angularSpeed;
    }
    private void VisionGenerator()
    {
        GameObject vision = new GameObject("EnemyVision");

        SphereCollider sphere = vision.AddComponent<SphereCollider>();
        EnemyVision enemyVision = vision.AddComponent<EnemyVision>();
        vision.transform.SetParent(transform);
        vision.transform.localPosition = Vector3.zero;

        sphere.radius = _enemyFov;
        sphere.isTrigger = true;
    }
    /// <summary>
    /// �ړI�n�������_���ɜp�j����
    /// </summary>
    private void Patrol()
    {
        //�ړI�n���Z�b�g����
        _navMeshAgent.SetDestination(_currentDestination);

        float distance = Vector3.Distance(this.transform.position, _currentDestination);

        // �v���C���[�������Ă��Ȃ��ꍇ�ŖړI�n�t�߂ɂ���Ȃ玟�̖ړI�n��
        if (!_navMeshAgent.isStopped && !_hasSeen && distance <= _stopDistance)
        {
            StartCoroutine(ChangeDestination());
        }
        // �v���C���[�ǐՒ��͏���ύX�������~�߂�
        else if (_hasSeen)
        {
            StopCoroutine(ChangeDestination());
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
    /// <summary>
    /// ���������猳�̖ړI�n�ɖ߂�
    /// </summary>
    public void ReturnDestination()
    {
        Debug.Log("��������");
        _hasSeen = false;
        _currentDestination = _lastDestination; //���̖ړI�n�ɖ߂�
        _navMeshAgent.isStopped = false;
    }
    /// <summary>
    /// �I�u�W�F�N�g�����E�ɓ��������ǂ������肷��
    /// </summary>
    /// <param name="collider"></param>
    public void FindObject(Collider collider)
    {
        Vector3 origin = transform.position;
        Vector3 toPlayer = (collider.transform.position - origin).normalized;
        float targetAngle = Vector3.Angle(transform.forward, toPlayer);
        float distance = Vector3.Distance(this.transform.position, collider.transform.position);
        float currentFov = _enemyFov;

        // ����p���ɂ��邩����
        if (targetAngle < _angle / 2)
        {
            RaycastHit hit;
            // Raycast�ŏ�Q���̗L�����`�F�b�N����
            if (Physics.Raycast(origin, toPlayer, out hit, _enemyFov) && hit.collider == collider)
            {
                _enemyFov = _magnification;
                _hasSeen = true;
                EnemyProcces(collider, distance);
            }
            else if (_hasSeen)
            {
                _enemyFov = currentFov;
                return;
            }
        }
    }
    /// <summary>
    /// �G�l�~�[�̏���
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="distance"></param>
    private void EnemyProcces(Collider collider, float distance)
    {
        if (collider.CompareTag("Player"))
        {
            ProccesToPlayer(collider, distance);
        }
        else if (collider.CompareTag("Baggage"))
        {
            ProccesToBaggage(collider, distance);
        }
    }
    /// <summary>
    /// �v���C���[�ɉ�������̍s�����s��
    /// </summary>
    /// <param name="player"></param>
    /// <param name="distance"></param>
    protected virtual void ProccesToPlayer(Collider player, float distance) { }
    /// <summary>
    /// �ו��ɉ�������̍s�����s��
    /// </summary>
    /// <param name="baggage"></param>
    /// <param name="distance"></param>
    protected virtual void ProccesToBaggage(Collider baggage, float distance) { }

    //�G�̎��E����������֐�(�K�v�ɉ����ăR�����g�A�E�g����<3)
    private void OnDrawGizmos()
    {
        if (_navMeshAgent == null) return;

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        float viewAngle = _angle; // ����p
        int segments = 20;

        // �F��ݒ�
        Gizmos.color = _hasSeen ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);

        // ��`���O�p�`�œh��Ԃ�
        for (int i = 0; i < segments; i++)
        {
            float angle1 = -_angle / 2 + (viewAngle * i / segments);
            float angle2 = -_angle / 2 + (viewAngle * (i + 1) / segments);

            Vector3 dir1 = Quaternion.Euler(0, angle1, 0) * forward * _enemyFov;
            Vector3 dir2 = Quaternion.Euler(0, angle2, 0) * forward * _enemyFov;

            // �O�p�`��`��
            Vector3[] vertices = new Vector3[] { origin, origin + dir1, origin + dir2 };

            // Gizmos�ŎO�p�`��h��Ԃ�
            DrawTriangle(vertices[0], vertices[1], vertices[2]);
        }
    }
    private void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p1);

        // ����������ς��ďd�˂邱�Ƃœh��Ԃ��̂悤�Ɍ�����
        for (float t = 0; t <= 1; t += 0.1f)
        {
            Vector3 a = Vector3.Lerp(p1, p2, t);
            Vector3 b = Vector3.Lerp(p1, p3, t);
            Gizmos.DrawLine(a, b);
        }
    }
}
