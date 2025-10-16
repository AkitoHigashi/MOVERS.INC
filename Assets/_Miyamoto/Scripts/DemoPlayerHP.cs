using UnityEngine;

namespace MiyamotoDemoPlayer
{
    public class DemoPlayerHP : MonoBehaviour
    {
        [SerializeField]
        private float _Hp;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MonsterWeapon"))
            {
                var weapon = other.GetComponent<MonsterWeapon>();
                _Hp -= weapon.Power;
            }
        }
    }
}
