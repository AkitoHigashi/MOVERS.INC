using UnityEngine;

public class BatColSpawn : MonoBehaviour
{
    [SerializeField]private Collider col;

    public void ColOnTrigger() 
    {
        col.enabled = true;
    }

    public void ColOfTrigger()
    {
        col.enabled = false;
    }
}
