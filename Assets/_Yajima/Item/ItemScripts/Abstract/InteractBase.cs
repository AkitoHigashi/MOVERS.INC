using UnityEngine;

public abstract class InteractBase : MonoBehaviour
{
    public abstract void Interact();
    public abstract void DemolishedLuggage();
    public abstract void PutLuggage(Collision collision);
}
