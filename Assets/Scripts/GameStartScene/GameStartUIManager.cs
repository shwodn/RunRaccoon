using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStartUIManager : MonoBehaviour
{

    [SerializeField] GameObject inputStartGameCanvus;
    [SerializeField] GameObject inputSetUpGameCanvus;

    [SerializeField] GameObject hpUpgardeImage;
    [SerializeField] GameObject coinUpgardeImage;
    [SerializeField] GameObject cheeseUpgardeImage;

    [SerializeField] private TextMeshProUGUI heartFigure;
    [SerializeField] private TextMeshProUGUI heartCost;
    [SerializeField] private TextMeshProUGUI coinFigure;
    [SerializeField] private TextMeshProUGUI coinCost;
    [SerializeField] private TextMeshProUGUI cheeseFigure;
    [SerializeField] private TextMeshProUGUI cheeseCost;
    [SerializeField] private TextMeshProUGUI currentCoinText;

    public static GameStartUIManager ShopInstance { get; private set; }

    private void Awake(){ ShopInstance = this; }

    // 구독 설정 및 초기화
    private void OnEnable() 
    { 
        GameManager.Instance.testEvent += UpdateShopUI;
        // 초기 설정 진행
        Init();
    }

    private void OnDestroy() 
    { 
        GameManager.Instance.testEvent -= UpdateShopUI; 
        ShopInstance = null;
    }
    private void Init()
    {
        // 캔버스 초기 설정
        inputStartGameCanvus.SetActive(true);
        inputSetUpGameCanvus.SetActive(false);
        // 업그레이드 버튼 비활성화
        hpUpgardeImage.SetActive(false);
        coinUpgardeImage.SetActive(false);
        cheeseUpgardeImage.SetActive(false);
        // 맥스 레벨일 경우 해당 버튼 이미지만 활성화
        if (GameManager.Instance.tempHpLevel == PlayerModel.StatLevel.Level3) 
        { hpUpgardeImage.SetActive(true); }
        if (GameManager.Instance.tempCoinLevel == PlayerModel.StatLevel.Level3) 
        { coinUpgardeImage.SetActive(true); }
        if (GameManager.Instance.tempCheeseLevel == PlayerModel.StatLevel.Level3) 
        { cheeseUpgardeImage.SetActive(true); }

    }


    // 상점 캔버스 활성화
    public void ShowSetUpUi()
    {
        inputStartGameCanvus.SetActive(false);
        inputSetUpGameCanvus.SetActive(true);
        // 데이터 로딩
        GameManager.Instance.Loading();
        // UI 업데이트 실행
        UpdateShopUI();
    }

    // 씬 전환
    public void StartGame() 
    {
        GameManager.Instance.saveData.Save(GameManager.Instance.tempHpLevel, GameManager.Instance.tempCoinLevel, 
            GameManager.Instance.tempCheeseLevel, GameManager.Instance.tempCurrentCoin);
        SceneManager.LoadScene("InGameScene"); 
    }

    // 레벨에 따른 업데이트 구현
    public void UpgradeHpLevel()
    {
        switch (GameManager.Instance.tempHpLevel)
        {
            case PlayerModel.StatLevel.Level1:
                GameManager.Instance.tempHpLevel = PlayerModel.StatLevel.Level2;
                GameManager.Instance.tempCurrentCoin--;
                break;
            case PlayerModel.StatLevel.Level2:
                GameManager.Instance.tempHpLevel = PlayerModel.StatLevel.Level3;
                GameManager.Instance.tempCurrentCoin -= 2;
                // 버튼 비활성화
                hpUpgardeImage.SetActive(true);
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        UpdateShopUI();
    }

    public void UpgradeCoinLevel()
    {
        switch (GameManager.Instance.tempCoinLevel)
        {
            case PlayerModel.StatLevel.Level1:
                // 돈이 충분하지 않을 경우 수행 X
                if (!IsEnoughCoin(1)) { Debug.Log("코인이 부족합니다."); return; }
                GameManager.Instance.tempCoinLevel = PlayerModel.StatLevel.Level2;
                GameManager.Instance.tempCurrentCoin--;
                break;
            case PlayerModel.StatLevel.Level2:
                // 돈이 충분하지 않을 경우 수행 X
                if (!IsEnoughCoin(1)) { Debug.Log("코인이 부족합니다."); return; }
                GameManager.Instance.tempCoinLevel = PlayerModel.StatLevel.Level3;
                GameManager.Instance.tempCurrentCoin -= 2;
                // 버튼 비활성화
                coinUpgardeImage.SetActive(true);
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        UpdateShopUI();
    }

    public void UpgradeCheeseLevel()
    {
        switch (GameManager.Instance.tempCheeseLevel)
        {
            case PlayerModel.StatLevel.Level1:
                // 돈이 충분하지 않을 경우 수행 X
                if(!IsEnoughCoin(1)) { Debug.Log("코인이 부족합니다."); return; }
                GameManager.Instance.tempCheeseLevel = PlayerModel.StatLevel.Level2;
                GameManager.Instance.tempCurrentCoin--;
                break;
            case PlayerModel.StatLevel.Level2:
                // 돈이 충분하지 않을 경우 수행 X
                if (!IsEnoughCoin(2)) { Debug.Log("코인이 부족합니다."); return; }
                GameManager.Instance.tempCheeseLevel = PlayerModel.StatLevel.Level3;
                GameManager.Instance.tempCurrentCoin -= 2;
                // 버튼 비활성화
                cheeseUpgardeImage.SetActive(true);
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        UpdateShopUI();
    }

    //UI 업데이트
    public void UpdateShopUI()
    {
        Debug.Log(GameManager.Instance.saveData.PlayerToSave.PlayerMaxHpLevel);
        Debug.Log(GameManager.Instance.tempHpLevel);
        // 체력 텍스트 레벨 별로 설정
        switch (GameManager.Instance.tempHpLevel)
        {
            case PlayerModel.StatLevel.Level1:
                heartFigure.text = "3 : 4";
                heartCost.text = "1";
                break;
            case PlayerModel.StatLevel.Level2:
                heartFigure.text = "4 : 5";
                heartCost.text = "2";
                break;
            case PlayerModel.StatLevel.Level3:
                heartFigure.text = "MaxLevel";
                heartCost.text = "";
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        //코인
        switch (GameManager.Instance.tempCoinLevel)
        {
            case PlayerModel.StatLevel.Level1:
                coinFigure.text = "1 : 3";
                coinCost.text = "1";
                break;
            case PlayerModel.StatLevel.Level2:
                coinFigure.text = "3 : 5";
                coinCost.text = "2";
                break;
            case PlayerModel.StatLevel.Level3:
                coinFigure.text = "MaxLevel";
                coinCost.text = "";
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        // 치즈
        switch (GameManager.Instance.tempCheeseLevel)
        {   
            case PlayerModel.StatLevel.Level1:
                cheeseFigure.text = "1 : 3";
                cheeseCost.text = "1";
                break;
            case PlayerModel.StatLevel.Level2:
                cheeseFigure.text = "3 : 5";
                cheeseCost.text = "2";
                break;
            case PlayerModel.StatLevel.Level3:
                cheeseFigure.text = "MaxLevel";
                cheeseCost.text = "";
                break;
            default:
                Debug.Log("예외 상황 발생");
                break;
        }
        // 현재 코인
        currentCoinText.text = $"{GameManager.Instance.tempCurrentCoin}";
    }

    private bool IsEnoughCoin(int input)
    {
        // 입력 값보다 돈이 부족한지 확인
        if (input <= GameManager.Instance.tempCurrentCoin) 
        {  return true; } else { return false; }
    }
}
