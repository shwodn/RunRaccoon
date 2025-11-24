using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUIManager : MonoBehaviour
{
    [SerializeField] GameObject inputStartGameCanvus;
    [SerializeField] GameObject inputSetUpGameCanvus;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        // 캔버스 초기 설정
        inputStartGameCanvus.SetActive(true);
        inputSetUpGameCanvus.SetActive(false);
    }

    public void ShowSetUpUi()
    {
        inputStartGameCanvus.SetActive(false);
        inputSetUpGameCanvus.SetActive(true);
    }

    // 씬 전환
    public void StartGame() { SceneManager.LoadScene("InGameScene"); }

}
