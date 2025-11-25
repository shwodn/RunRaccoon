using UnityEngine;

public interface IPoolable
{
    public void OnCreate(); // 생성되었을 때
    public void OnSpawn(); // 소환 됐을 때
    public void OnDespawn(); // 소환 해제 됐을 때
    public void OnDispose(); // 파괴될 때
}
