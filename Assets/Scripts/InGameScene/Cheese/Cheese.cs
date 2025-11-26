using UnityEngine;

public class Cheese : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면 비활성화
        if (collision.CompareTag("Player")) { gameObject.SetActive(false); }
        // 일부러 제외하는 충돌
        else if (collision.CompareTag("Exception")) return;
        // 예외는 로그 출력
        else { Debug.Log($"예상치 못한 충돌 발생 : {collision.name}-{collision.tag}"); }
    }
}
