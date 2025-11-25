using UnityEngine;

public class StartPointInspector : MonoBehaviour
{
    [SerializeField] private MapSpawner inputTilemapSpawner;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("충돌을 감지했습니다.");
        // 타일 맵이 카메라에 들어가면 수행할 코드
        if (collision.CompareTag("TileStartPoint")) 
        { 
            Debug.Log("타일 맵이 카메라에 들어오기 시작합니다.");
            inputTilemapSpawner.SpawnTilemap();
        }
        
    }
}
