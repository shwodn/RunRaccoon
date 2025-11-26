using UnityEngine;

public class CheeseSetUp : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;

    [SerializeField] private GameObject inputCheese1;
    [SerializeField] private GameObject inputCheese2;
    [SerializeField] private GameObject inputCheese3;

    private void OnEnable()
    {
        // 모든 코인 비활성화
        Init();
        // 해당하는 코인만 활성화
        SetUpCoin(playerModel.CheeseLevel);
    }

    private void Init()
    {
        inputCheese1.SetActive(false);
        inputCheese2.SetActive(false);
        inputCheese3.SetActive(false);
    }

    private void SetUpCoin(PlayerModel.StatLevel inputStatLevel)
    {
        switch (inputStatLevel)
        {
            case PlayerModel.StatLevel.Level1:
                inputCheese1.SetActive(true);
                break;
            case PlayerModel.StatLevel.Level2:
                inputCheese2.SetActive(true);
                break;
            case PlayerModel.StatLevel.Level3:
                inputCheese3.SetActive(true);
                break;
        }
    }
}
