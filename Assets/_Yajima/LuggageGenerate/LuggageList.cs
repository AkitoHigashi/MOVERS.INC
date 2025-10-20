using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LuggageList", menuName = "Data/LuggageList")]
public class LuggageList : ScriptableObject
{
    [SerializeField] List<LuggageClass> _list;
    public List<LuggageClass> List => _list;

    public override string ToString()
    {
        return name;
    }
}

[System.Serializable]
public class LuggageClass
{
    [SerializeField] Luggage _prefab;
    [SerializeField] GameObject _particle;

    public Luggage Prefab => _prefab;
    public GameObject Particle => _particle;
}
