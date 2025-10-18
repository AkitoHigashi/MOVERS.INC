using UnityEngine;

public class Interact : MonoBehaviour,IStartSetVariables
{
    private float _interactStartTime;
    private float _interactTime;
    private float _interactDistance;

    public void StartSetVariables(PlayerData playerData)
    {
        _interactTime= playerData.InteractTime;
        _interactDistance = playerData.InteractDistance;
    }

    public void StartInteract()
    {
        _interactStartTime = Time.time;
        Debug.Log("StartInteract");
    }

    public void StopInteract()
    {
        if(Time.time - _interactStartTime >= _interactTime)
        {
            InteractExecution();
        }
        
    }

    private void InteractExecution()
    {
        Debug.Log($"{Time.time - _interactStartTime >= _interactTime}");
    }
}
