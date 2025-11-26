using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject hp0;
    [SerializeField] private GameObject hp01;
    [SerializeField] private GameObject hp02;
    [SerializeField] private GameObject hp03;
    [SerializeField] private GameObject hp04;
    [SerializeField] private GameObject hp05;
    
    
    [SerializeField] private TextMeshProUGUI currentCoinText;
    [SerializeField] private TextMeshProUGUI currentCheeseText;

    public void UpdateCurrentHpUI(int inputCurrentHp, PlayerModel.StatLevel inputStatLevel)
    {
        // 이미지 초기화
        InitHpImage();
        // 현재 체력에 해당하는 이미지만 활성화
        ActiveTrueImage(inputCurrentHp, inputStatLevel);
        
    }
    public void UpdateCoinUI(int inputCurrentCoin) { currentCoinText.text = $"{inputCurrentCoin}"; }
    public void UpdateCheeseUI(int inputCurrentCheese) { currentCheeseText.text = $"{inputCurrentCheese}"; }

    private void InitHpImage()
    {
        hp0.SetActive(false);
        hp01.SetActive(false);
        hp02.SetActive(false);
        hp03.SetActive(false);
        hp04.SetActive(false);
        hp05.SetActive(false);
    }

    private void ActiveTrueImage(int inputCurrentHp, PlayerModel.StatLevel inputStatLevel)
    {
        // 레벨 마다 최대 체력이 다르니 레벨 별로 활성화되는 hp이미지를 다르게 함
        if((int)inputStatLevel == 1)
        {
            switch (inputCurrentHp)
            {
                case 0:
                    hp0.SetActive(true);
                    break;
                case 1:
                    hp02.SetActive(true);
                    break;
                case 2:
                    hp04.SetActive(true);
                    break;
                case 3:
                    hp05.SetActive(true);
                    break;
            }
        }
        else if((int)inputStatLevel == 2)
        {
            switch (inputCurrentHp)
            {
                case 0:
                    hp0.SetActive(true);
                    break;
                case 1:
                    hp01.SetActive(true);
                    break;
                case 2:
                    hp02.SetActive(true);
                    break;
                case 3:
                    hp03.SetActive(true);
                    break;
                case 4:
                    hp05.SetActive(true);
                    break;
            }
        }
        else if((int)inputStatLevel == 3)
        {
            switch (inputCurrentHp)
            {
                case 0:
                    hp0.SetActive(true);
                    break;
                case 1:
                    hp01.SetActive(true);
                    break;
                case 2:
                    hp02.SetActive(true);
                    break;
                case 3:
                    hp03.SetActive(true);
                    break;
                case 4:
                    hp04.SetActive(true);
                    break;
                case 5:
                    hp05.SetActive(true);
                    break;
            }
        }
        // 예상 입력 값이 아닌 경우 hp0 활성화
        else { hp0.SetActive(true); }
    }
}
