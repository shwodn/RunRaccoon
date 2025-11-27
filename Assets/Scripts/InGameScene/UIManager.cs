using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject InGameMainCanvus;
    [SerializeField] private GameObject ResultGameCanvus;

    public void UIInit()
    {
        InGameMainCanvus.SetActive(true);
        ResultGameCanvus.SetActive(false);
    }

    public void ResultUI()
    {
        InGameMainCanvus.SetActive(false);
        ResultGameCanvus.SetActive(true);
    }

    public void RestartGame()
    {
        // 저장 전에 저장할 데이터 변경 사항 대입
        GameManager.Instance.tempHpLevel = PlayerModel.PlayerInstance.MaxHPLevel;
        GameManager.Instance.tempCoinLevel = PlayerModel.PlayerInstance.CoinLevel;
        GameManager.Instance.tempCheeseLevel = PlayerModel.PlayerInstance.CheeseLevel;
        GameManager.Instance.tempCurrentCoin += PlayerModel.PlayerInstance.CurrentCoin;
        // 저장 진행 후 씬 전환
        GameManager.Instance.saveData.Save(GameManager.Instance.tempHpLevel, GameManager.Instance.tempCoinLevel,
            GameManager.Instance.tempCheeseLevel, GameManager.Instance.tempCurrentCoin);
        SceneManager.LoadScene("GameStartScene");
    }
}
