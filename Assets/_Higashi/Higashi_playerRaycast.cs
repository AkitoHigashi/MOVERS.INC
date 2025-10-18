using UnityEngine;

public class Higashi_playerRaycast : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 2f;
    [SerializeField] private bool _raycastOn = true;//raycastを出すのか
    [SerializeField] private Canvas _canvas;

    [SerializeField, Range(0f, 1f)]
    private RaycastHit hit;
    public void Update()
    {
        if (_raycastOn)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance);
            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);
            if (hit.collider != null && hit.collider.CompareTag("QuestPaper"))
            {
                if (Input.GetKeyDown(KeyCode.F))//離した瞬間の１フレーム
                {
                    _canvas.gameObject.SetActive(true);
                    Debug.Log("当たってる");
                }
            }
        }
    }
}
