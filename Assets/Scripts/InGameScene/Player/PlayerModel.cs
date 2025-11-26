using System;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    // 필요한 멤버 구현
    public enum StatLevel { Level1 = 1, Level2, Level3 };

    private StatLevel maxHPLevel = StatLevel.Level1;
    private StatLevel coinLevel = StatLevel.Level1;
    private StatLevel cheeseLevel = StatLevel.Level1;

    [SerializeField] private int currentHp = 3;
    private int currentCoin = 0;
    private int currentCheese = 0;

    // 프로퍼티 구현
    public StatLevel MaxHPLevel { get { return maxHPLevel; }  set { maxHPLevel = value; } }
    public StatLevel CoinLevel { get { return coinLevel; } set { coinLevel = value; } }
    public StatLevel CheeseLevel { get { return cheeseLevel; } set { cheeseLevel = value; } }

    public int CurrentHp { get { return currentHp; } private set { currentHp = value; } }
    public int CurrentCoin { get { return currentCoin; } set { currentCoin = value; } }
    public int CurrentCheese { get { return currentCheese; } set { currentCheese = value; } }

    public event Action OnInit;
    public event Action<int, StatLevel> OncurrentHpChanged;
    public event Action<int> OnCurrentCoinChanged;
    public event Action<int> OnCurrentCheeseChanged;
    public event Action OnDead;

    // 멤버 변화 내용을 알릴 매서드
    public void ChangeHp(int damage)
    {
        currentHp += damage;
        OncurrentHpChanged?.Invoke(currentHp, maxHPLevel);
        // 체력이 1보다 작을 경우 플레이어가 죽었을 때 실행할 매서드들 실행
        if(currentHp < 1) { OnDead?.Invoke(); }
    }

    public void GetCoin()
    {
        switch(coinLevel)
        {
            case StatLevel.Level1:
                currentCoin++;
                break;
            case StatLevel.Level2: 
                currentCoin += 3;
                break;
            case StatLevel.Level3:
                currentCoin += 5;
                break;
        }
        OnCurrentCoinChanged?.Invoke(currentCoin);
    }

    public void GetCheese()
    {
        switch (cheeseLevel)
        {
            case StatLevel.Level1:
                currentCoin++;
                break;
            case StatLevel.Level2:
                currentCoin += 3;
                break;
            case StatLevel.Level3:
                currentCoin += 5;
                break;
        }
        OnCurrentCheeseChanged?.Invoke(currentCheese);
    }

    // 플레이어 정보 초기화 매서드
    public void InitPlayer()
    {
        #region 체력 설정
        switch (maxHPLevel)
        {
            case StatLevel.Level1:
                currentHp = 3;
                break;
            case StatLevel.Level2:
                currentHp = 4;
                break;
            case StatLevel.Level3:
                currentHp = 5;
                break;
        }
        #endregion

        // 코인과 치즈 0으로 초기화
        currentCoin = 0;
        currentCheese = 0;
        OnInit?.Invoke();
    }
}
