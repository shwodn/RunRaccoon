using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class JsonSupport
{
    // 세이브
    public void Save(object Target)
    {
        // 저장 경로를 지정하고, 해당 폴더가 없는 경우 생성해야 한다.
        string filePath = Application.dataPath + "/Database/SaveData";
        Directory.CreateDirectory(filePath);

        // 경로에 파일과 확장자를 덧붙여 파일경로를 완성한다.
        filePath += "/" + Target.GetType().Name + ".json";

        // Class 데이터를 Json 문자열로 변환한다.
        string jsonString = JsonUtility.ToJson(Target);

        // 이진 형식(10110110101..)으로 직렬화 한다.
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            byte[] bytes = Encoding.Default.GetBytes(jsonString);
            bf.Serialize(fs, bytes);
        }
    }
    // 로드
    public void Load(object Target)
    {
        // 폴더 경로를 지정하고, 해당 폴더가 없는 경우 생성한다.
        string filePath = Application.dataPath + "/Database/SaveData";
        Directory.CreateDirectory(filePath);

        // 경로에 파일명과 확장자를 덧붙여 파일경로를 완성한다.
        filePath += "/" + Target.GetType().Name + ".json";

        // 경로에 해당 파일이 존재하는 경우에만
        if (File.Exists(filePath))
        {
            //이진 형식으로부터 역직렬화한다.
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] bytes = (byte[])bf.Deserialize(fs);
                string jsonString = Encoding.Default.GetString(bytes);
                JsonUtility.FromJsonOverwrite(jsonString, Target);
            }
        }
    }
}
