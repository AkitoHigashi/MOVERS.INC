using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FallButton : MonoBehaviour
{
    [SerializeField] private Button button;   // 押すボタン
    [SerializeField] private Transform target; // 落ちるオブジェクト
    [SerializeField] private float fallDistance = 2f; // 落ちる距離
    [SerializeField] private float fallSpeed = 5f;    // 落ちる速さ
    [SerializeField]private TatukiCollector collector;
    [SerializeField] private TatukiItemManager TatukiItemManager;
    [SerializeField] TatukiScore _score;

    private Vector3 originalPosition;
    private bool isFalling = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var score = other.gameObject.GetComponent<TatukiItem>();
            TatukiItemManager.UnregisterItem(other.gameObject);
            Destroy(other.gameObject);
            
            _score.SetScore(-score.Score);
            _score.SetText(_score.nowScore.ToString());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var score = collision.gameObject.GetComponent<TatukiItem>();
            TatukiItemManager.UnregisterItem(collision.gameObject);
            Destroy(collision.gameObject);

            _score.SetScore(-score.Score);
            _score.SetText(_score.nowScore.ToString());
        }
    }
    private void Start()
    {
        originalPosition = target.position;
        button.onClick.AddListener(() => StartCoroutine(FallAndReturn()));
    }

    private IEnumerator FallAndReturn()
    {
        if (isFalling) yield break; // 二重実行防止
        isFalling = true;

        Vector3 targetPosition = originalPosition + Vector3.down * fallDistance;

        // 落ちる
        while (Vector3.Distance(target.position, targetPosition) > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position, targetPosition, fallSpeed * Time.deltaTime);
            yield return null;
            
        }
        Debug.Log("fallend");
        // 1秒待つ
        yield return new WaitForSeconds(1f);

        // 戻る
        while (Vector3.Distance(target.position, originalPosition) > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position, originalPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        isFalling = false;
        collector.Collect();
        

    }

  
   
}
