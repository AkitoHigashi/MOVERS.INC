using UnityEngine;

/// <summary>
/// ����g���K�[��p�X�N���v�g�i�q�I�u�W�F�N�g�ɃA�^�b�`�j
/// </summary>
public class EnemyVision : MonoBehaviour
{
    private EnemyBase _enemyBase;
    private EnemyMove _enemyMove;
    private SphereCollider _sphereCollider;
    private void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        _enemyMove = GetComponentInParent<EnemyMove>();
        _sphereCollider = GetComponentInParent<SphereCollider>();
        _sphereCollider.radius = _enemyBase.EnemyFov;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _enemyMove != null)
        {
            Debug.Log("�v���C���[�������Ă���");
            _enemyMove.FindPlayer(other);
        }
    }
}
