using UnityEngine;
using System.Collections.Generic;

public class CardboardInstance : InteractBase
{
    Animator _anim;
    List<Luggage> _luggages = new List<Luggage>();
    public List<Luggage> Luggages => _luggages;
    Luggage _luggage;
    bool _canPutin = true;

    private void Start()
    {
        _luggage = GetComponent<Luggage>();
        _anim = GetComponent<Animator>();
        _luggage.NoDamage();
    }

    [ContextMenu("a")]
    public override void Interact()
    {
        _anim.SetTrigger("Close");
        _canPutin = false;
        _luggage.TakeDamage();
        _luggage.ChangeScoreInCardboad(_luggages);
    }

    public override void PutLuggage(Collision collision)
    {
        if (_canPutin)
        {
            if (collision.gameObject.tag == "Luggage")
            {
                if (_luggages.Count < 4)
                {
                    _luggages.Add(collision.gameObject.GetComponent<Luggage>());
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    [ContextMenu("b")]
    public override void DemolishedLuggage()
    {
        _anim.SetTrigger("Open");
    }

    public void LuggageRelease()
    {
        foreach (var luggage in _luggages)
        {
            luggage.gameObject.SetActive(true);
            luggage.transform.position = transform.position;
        }
    }

    public void DestroyCardboard()
    {
        Destroy(gameObject);
    }
}
