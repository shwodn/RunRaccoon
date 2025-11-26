using TMPro;
using UnityEngine;

public class PlayerView2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentCoinText;
    [SerializeField] private TextMeshProUGUI currentCheeseText;

    public void UpdateCoinUI(int inputCurrentCoin) { currentCoinText.text = $"{inputCurrentCoin}"; }
    public void UpdateCheeseUI(int inputCurrentCheese) { currentCheeseText.text = $"{inputCurrentCheese}"; }
}
