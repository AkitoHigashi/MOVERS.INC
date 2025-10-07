using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// �C���x���g���̃N���X
/// </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryData> _inventoryList;

    /// <summary>
    /// �A�C�e���̊i�[�A���o�����s���֐�
    /// </summary>
    /// <param name="item">�i�[����A�C�e��</param>
    /// <param name="inventoryNum">�i�[����C���x���g���̃C���f�b�N�X</param>
    /// <returns>
    /// �i�[���悤�Ƃ��Ă�����̂̎�ނɂ���āA�i�[����Ă����A�C�e���Anull�A�i�[���悤�Ƃ��Ă�����̂̂����ꂩ
    /// </returns>
    public GameObject StoreItem(GameObject item, int inventoryNum)
    {
        if (!item)
        {
            //���������Ă��Ȃ����null�ŃC���x���g���̊֐������s
            Debug.Log($"�C���x���g��{inventoryNum + 1}������o��");
            return _inventoryList[inventoryNum].StoreItem(item);
        }
        else if (item.tag == "Item")
        {
            //�A�C�e���������Ă����炻�̃A�C�e���ŃC���x���g���̊֐������s
            Debug.Log($"�C���x���g��{inventoryNum + 1}�Ɋi�[");
            item.transform.SetParent(this.transform);
            return _inventoryList[inventoryNum].StoreItem(item);
        }
        else
        {
            //�A�C�e���ȊO���i�[���悤�Ƃ��Ă�����A�C�e�����o��
            //�����͌�ŕύX����\������ӏ�
            //�����Ă�����̂����̏�ɒu���ꍇ
            item.transform.SetParent(null);
            Debug.Log("�A�C�e���ȊO���i�[���邱�Ƃ͂ł��܂���");
            return _inventoryList[inventoryNum].StoreItem(null);
        }
    }

    /// <summary>
    /// �C���x���g���̃f�[�^�̃N���X
    /// </summary>
    [System.Serializable]
    public class InventoryData
    {
        [SerializeField] Image _image;
        Queue<GameObject> _inventory = new Queue<GameObject>();

        /// <summary>
        /// �A�C�e���̊i�[�A���o�����s���֐�
        /// </summary>
        /// <param name="item">�i�[����A�C�e��</param>
        /// <returns>���o�����A�C�e����null</returns>
        public GameObject StoreItem(GameObject item)
        {
            //�A�C�e�������o������
            GameObject go = null;
            if (_inventory.Count > 0)
            {
                go = _inventory.Dequeue();
                go.SetActive(true);
                //_image.sprite = null;
            }

            //�A�C�e�������܂�����
            if (item)
            {
                _inventory.Enqueue(item);
                item.SetActive(false);
                //�A�C�e���̃X�v���C�g��ݒ肷��
                //�X�v���C�g���ǂ�����ĕێ����Ă��邩�킩��Ȃ����ߕۗ�
                //_image.sprite = ;
            }

            return go;
        }
    }
}
