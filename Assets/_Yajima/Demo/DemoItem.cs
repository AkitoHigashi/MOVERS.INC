using UnityEngine;

public class DemoItem : ItemBase
{
    private void Start()
    {
        base.Init();
    }

    public override void ItemActivate(LuggageData luggage)
    {
        Debug.Log("a");
    }
}
