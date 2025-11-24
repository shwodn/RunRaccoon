using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 인스턴스 전역 선언 및 자동 프로퍼티
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        // 싱글톤 설정
        SetSingleTon();
    }

    private void SetSingleTon()
    {
        if (Instance != null) { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // 실행시 프로그램 종료
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
