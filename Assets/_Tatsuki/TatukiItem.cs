using UnityEngine;

public class TatukiItem : MonoBehaviour
{
    [SerializeField] int _score = 100;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) _score--;
        if (_score <= 0) Destroy(gameObject);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player")) _score--;
    //    if(_score<=0)Destroy(gameObject);
    //}

    public int Score => _score;


}
