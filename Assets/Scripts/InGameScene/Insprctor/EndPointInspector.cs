using UnityEngine;

public class EndPointInspector : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("충돌을 감지했습니다.");
        // 타일 맵이 카메라를 나갔으면 시행할 코드
        if (collision.CompareTag("TileEndPoint")) { Debug.Log("타일 맵이 카메라 밖으로 나갔습니다. "); }
    }


}
