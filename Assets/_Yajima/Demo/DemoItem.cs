using UnityEngine;

public class DemoItem : ItemBase
{
    private void Start()
    {
        base.SetUp();
    }

    public override void ItemActivate()
    {
        Debug.Log("a");
    }
}
