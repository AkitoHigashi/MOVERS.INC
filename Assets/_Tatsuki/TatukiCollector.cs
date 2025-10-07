using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class TatukiCollector : MonoBehaviour
{
    [SerializeField]private TatukiItemManager tatukiItemManager;
    [SerializeField] private Collider Collider;
    [SerializeField]private TatukiScore tatukiScore;

    public void Collect()
    {
        int collectedCount = 0;
        List<GameObject> toRemove = new List<GameObject>();

        foreach(var item in tatukiItemManager.GetItemInArea())
        {
            
           
            
                //Debug.Log("isFullyInside");
                collectedCount++;
                toRemove.Add(item);
                Destroy(item);
            
        }

        foreach (var item in toRemove)
            tatukiItemManager.UnregisterItem(item);
        tatukiScore.SetEndScore(true);
        Debug.Log($"Collected {collectedCount} items!");
    }
   
    
      private bool IsFullyInside(GameObject item, Collider area)
    {
        return area.bounds.Contains(item.transform.position);
    }

}

