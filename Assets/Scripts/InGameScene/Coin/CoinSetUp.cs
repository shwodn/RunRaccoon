using UnityEngine;

public class CoinSetUp : MonoBehaviour
{

    [SerializeField] private GameObject inputCoin1;
    [SerializeField] private GameObject inputCoin2;
    [SerializeField] private GameObject inputCoin3;

    private void Start()
    {
        // 모든 코인 비활성화
        Init();
        // 해당하는 코인만 활성화
        SetUpCoin(GameManager.Instance.saveData.PlayerToSave.PlayerCoinLevel);
    }

    private void Init()
    {
        inputCoin1.SetActive(false);
        inputCoin2.SetActive(false);
        inputCoin3.SetActive(false);
    }

    private void SetUpCoin(PlayerModel.StatLevel inputStatLevel)
    {
        switch(inputStatLevel)
        {
            case PlayerModel.StatLevel.Level1:
                inputCoin1.SetActive(true);
                break;
            case PlayerModel.StatLevel.Level2:
                inputCoin2.SetActive(true);
                break;
            case PlayerModel.StatLevel.Level3:
                inputCoin3.SetActive(true);
                break;
        }
    }
}
