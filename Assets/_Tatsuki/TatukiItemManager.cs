using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TatukiItemManager : MonoBehaviour
{
    private List<GameObject> ItemArea = new List<GameObject>();

    public void RegisterItem(GameObject item)
    {
        
        ItemArea.Add(item);
        //Debug.Log("RegisterItem");

    }
    public void UnregisterItem(GameObject item)
    {
        {
            ItemArea.Remove(item);
        }
    }

    public List<GameObject> GetItemInArea() => new List<GameObject>(ItemArea);
}

