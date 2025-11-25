using UnityEngine;

public class TilemapController : MonoBehaviour, IPoolable
{
    // 위치 정보를 임시로 받아둘 변수
    private Transform tempTransform;

    private void Awake()
    {
        // 트랜스폼 초기화
        tempTransform = this.transform;
    }
    public void OnCreate()
    {
        Debug.Log("타일맵 생성");
    }

    public void OnSpawn()
    {
        gameObject.SetActive(true);
        Debug.Log("풀에서 사용됨");
    }

    public void OnDispose()
    {
        Destroy(gameObject);
        Debug.Log("파괴됨");
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
        Debug.Log("풀로 반납됨");
    }

    public void SetUp(GameObject inputSpawnPoint)
    {
        // 입력 받은 위치 정보를 임시 변수에 저장 및 수정
        tempTransform.position = inputSpawnPoint.transform.position;

        // 게임 오브젝트에 위치 정보 대입
        transform.position = tempTransform.position;
    }

    public void SetUpFirst()
    {
        tempTransform.position = Vector3.zero;

        transform.position = tempTransform.position;
    }
    
}
