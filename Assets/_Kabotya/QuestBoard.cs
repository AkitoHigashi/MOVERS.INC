using UnityEngine;

public class QuestBoard:InteractBase
{
    [SerializeField] private string nextSceneName;

    public override void Interact()
    {
        Debug.Log("押された");
        SceneLordManager.Instance.Scnenlode(nextSceneName);
    }

    public override void DemolishedLuggage(){}

    public override void PutLuggage(Collision collision){}
}
