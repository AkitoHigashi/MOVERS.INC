using UnityEngine;

public class LuggageTrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tag != "Luggage")
        {
            tag = "Luggage";
        }
    }
}
