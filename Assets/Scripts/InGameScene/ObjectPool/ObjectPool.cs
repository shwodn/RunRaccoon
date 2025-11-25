using System.Collections.Generic;
using UnityEngine;

public class ObjectPool <T> where T : Component, IPoolable
{
    // 하나에 대한 여러 패턴의 프리팹들을 저장
    private T[] prefabs;
    private Queue<T> pool;
    // 확장 가능 여부
    public bool IsExpandable { get; set; }
    // 요소가 0일 경우 참, 아니면 거짓 반환
    public bool IsEmpty => pool.Count == 0;

    public Queue<T> Pool { get { return pool; } }

    // 기본 생성자
    public ObjectPool(T[] inputPrefabs, int capacity = 5, bool isExpandable = false)
        => Init(inputPrefabs, capacity, isExpandable);

    private void Init(T[] inputPrefabs, int capacity, bool isExpandable)
    {
        prefabs = inputPrefabs;
        IsExpandable = isExpandable;
        pool = new Queue<T>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            T pooledObject = CreatePoolObject(i);
            Despawn(pooledObject);
        }
    }

    public T Spawn()
    {
        // 프리팹이 비어있을 경우 null 반환
        if (prefabs == null) { Debug.Log("Null"); return null; }

        T pooledObject = null;

        // 확장 가능하고 풀이 비어있다면 새로운 객체 생성
        if (IsExpandable && IsEmpty) { pooledObject = CreatePoolObject(0); }
        // 아닌경우 큐에 있는 것을 반환
        else {  pooledObject = pool.Dequeue(); }

        Debug.Log(pooledObject);

        return pooledObject;
    }

    public void Despawn(T poolableObject)
    {
        poolableObject.OnDespawn();
        pool.Enqueue(poolableObject);
    }

    public void DespawnNotEnqueue(T poolableObject) { poolableObject.OnDespawn(); }
    

    public void DisposePooledObject(int count)
    {
        // 컬렉션에 들어있는 개수보다 더 많은 수를 요청하는 것을 제한
        count = Mathf.Min(count, pool.Count);

        for(int i = 0; i < count; i++)
        {
            T pooledObject = pool.Dequeue();
            pooledObject.OnDispose();
        }
    }

    private T CreatePoolObject(int i)
    {
        // 저장된 프리팹 중 특정 하나를 생성
        T pooledObject = MonoBehaviour.Instantiate(prefabs[i]);
        pooledObject.OnCreate();
        return pooledObject;
    }
}
