using UnityEngine;
using System.IO;

public class PlayerData
{
    public PlayerModel.StatLevel PlayerMaxHpLevel = PlayerModel.StatLevel.Level1;
    public PlayerModel.StatLevel PlayerCoinLevel = PlayerModel.StatLevel.Level1;
    public PlayerModel.StatLevel PlayerCheeseLevel = PlayerModel.StatLevel.Level1;
    public int PlayerCurrentCoin = 0;
}

public class SaveData : MonoBehaviour
{
    public PlayerData PlayerToSave = new PlayerData();
    private string filePath;
    private string dir;

    private void Awake()
    {
        Init();
    }

    public void Save(PlayerModel.StatLevel hpLv, PlayerModel.StatLevel coinLv, PlayerModel.StatLevel cheeseLv, int currentCoin)
    {
        Init();
        //유의미한 데이터 생성 및 기입
        PlayerData pd = new PlayerData();
        pd.PlayerMaxHpLevel = hpLv;
        pd.PlayerCoinLevel = coinLv;
        pd.PlayerCheeseLevel = cheeseLv;
        pd.PlayerCurrentCoin = currentCoin;

        //JSON으로 변환
        string json = JsonUtility.ToJson(pd, true);
        dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
        // 원하는 경로에 스트링을 파일로 저장
        File.WriteAllText(filePath, json);
        Debug.Log(pd.PlayerMaxHpLevel);
        Debug.Log(pd.PlayerCoinLevel);
        Debug.Log(pd.PlayerCheeseLevel);
        Debug.Log(pd.PlayerCurrentCoin);
        Debug.Log("저장 완료");
    }

    public void Load()
    {
        // 원하는 파일 경로에 존재하는지 확인하고 참 거짓 판단
        if(File.Exists(filePath))
        {
            //파일에서 문자열을 읽어옴
            string json = File.ReadAllText(filePath);
            //문자열을 클래스 형식에 맞게 파싱
            PlayerData player = JsonUtility.FromJson<PlayerData>(json);
            // 파싱한 데이터 저장
            PlayerToSave.PlayerMaxHpLevel = player.PlayerMaxHpLevel;
            PlayerToSave.PlayerCoinLevel = player.PlayerCoinLevel;
            PlayerToSave.PlayerCheeseLevel = player.PlayerCheeseLevel;
            PlayerToSave.PlayerCurrentCoin = player.PlayerCurrentCoin;
            Debug.Log("로드 완료");
        }
        else 
        { 
            Debug.LogWarning("파일 없어서 파일 생성");
            // 파일 생성
            Save(PlayerModel.StatLevel.Level1, PlayerModel.StatLevel.Level1, PlayerModel.StatLevel.Level1, 0);
        }
    }

    private void Init()
    {
        Debug.Log($"{filePath} : {dir}");
        bool isNeedSetPath = filePath == null || dir == null || filePath.Length == 0 || dir.Length == 0;
        if (isNeedSetPath)
        {
            // 저장한 파일 경로 설정
            filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
            dir = Path.GetDirectoryName(filePath);
            Debug.Log("filePath: " + filePath);
            Debug.Log("dir: " + dir);
            // 파일 경로에 상위 디렉토리가 있는지 확인하고 없으면 생성
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            Debug.Log("파일경로 : " + filePath);
        }
        
    }
}
