using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public SaveData saveData;

    // 인스턴스 전역 선언 및 자동 프로퍼티
    public static GameManager Instance { get; private set; }

    public PlayerModel.StatLevel tempHpLevel;
    public PlayerModel.StatLevel tempCoinLevel;
    public PlayerModel.StatLevel tempCheeseLevel;
    public int tempCurrentCoin;

    public event Action testEvent;


    private void Awake()
    {
        // 싱글톤 설정
        SetSingleTon();
        saveData = GetComponent<SaveData>();
    }

    

    private void Start()
    {
        StartCoroutine(Loading());
        
    }

    
    private void Update()
    {
        // a키 누르면 돈 복사 ( 테스트용 )
        if ( Keyboard.current.aKey.IsPressed()) 
        {
            tempCurrentCoin += 10;
            testEvent?.Invoke();
            Debug.Log("돈 복사 완료");
        }
        // d키 누르면 데이터 초기화 ( 테스트용 )
        if (Keyboard.current.dKey.IsPressed())
        {
            tempHpLevel = PlayerModel.StatLevel.Level1;
            tempCoinLevel = PlayerModel.StatLevel.Level1;
            tempCheeseLevel = PlayerModel.StatLevel.Level1;
            tempCurrentCoin = 0;
            testEvent?.Invoke();
            Debug.Log("데이터 초기화 완료");
        }
    }

    private void SetSingleTon()
    {
        StartCoroutine(Loading());
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
        // 인스턴스에 변경 값 저장
        Instance.tempHpLevel = PlayerModel.PlayerInstance.MaxHPLevel;
        Instance.tempCoinLevel = PlayerModel.PlayerInstance.CoinLevel;
        Instance.tempCheeseLevel = PlayerModel.PlayerInstance.CheeseLevel;
        Instance.tempCurrentCoin  += PlayerModel.PlayerInstance.CurrentCoin;
        //게임 플레이어 데이터 저장
        saveData.Save(Instance.tempHpLevel, Instance.tempCoinLevel,
            Instance.tempCheeseLevel, Instance.tempCurrentCoin);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    // 로딩 매서드
    public IEnumerator Loading()
    {
        yield return new WaitForSeconds(1f);
        saveData.Load();
        // 세이브 데이터를 임시 변수에 복사
        tempHpLevel = saveData.PlayerToSave.PlayerMaxHpLevel;
        tempCoinLevel = saveData.PlayerToSave.PlayerCoinLevel;
        tempCheeseLevel = saveData.PlayerToSave.PlayerCheeseLevel;
        tempCurrentCoin = saveData.PlayerToSave.PlayerCurrentCoin;
        Debug.Log("데이터 복사 완료");

    }
}
