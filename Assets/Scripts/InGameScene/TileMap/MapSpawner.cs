using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject inputTileMapSpawnPoint;
    [SerializeField] private TilemapController[] tilemapPrefabs;
    [SerializeField] private float delay;
    [SerializeField] private float firstDelay;

    private bool isSpawning = true;
    private bool isFirstSpawn = true;
    private WaitForSeconds spawnDelay;
    // 소환된 객체를 임시로 받아 비활성화할 때 사용
    private Queue<TilemapController> tempInstance = new Queue<TilemapController>();
    private ObjectPool<TilemapController> tilemapPool;

    public Queue<TilemapController> TempInstance { get { return tempInstance; } }
    public ObjectPool<TilemapController> TilemapPool { get { return tilemapPool; } }

    public GameObject InputTileMapSpawnPoint { get { return inputTileMapSpawnPoint; } }

    private void OnEnable() { Init(); }

    private void Start()
    {
        StartCoroutine(SpawnTilemapForLoop());
        Debug.Log("맵 활성화");
    }

    private void Init()
    {
        Debug.Log("풀 초기화");
        tilemapPool = new ObjectPool<TilemapController>(tilemapPrefabs, tilemapPrefabs.Length, false);
        spawnDelay = new WaitForSeconds(delay);
    }

    public void SpawnTilemap()
    {
        // 풀이 끝났을 경우 실행할 코드
        if (tilemapPool == null)
        {
            Debug.Log("스테이지가 끝났습니다.");
        }

        // 타일맵 생성
        TilemapController tilemap = tilemapPool.Spawn();
        // 타일맵 설정 처음인 경우에만 카메라에 보이는 상태로 소환
        if (isFirstSpawn) 
        {
            tilemap.SetUpFirst();
        }
        else { tilemap.SetUp(inputTileMapSpawnPoint); }
        // 정보 참조를 위해 임시 객체에 대입
        tempInstance.Enqueue(tilemap);
        // 타일맵 활성화
        tilemap.OnSpawn();
    }

    // 타일맵 비활성화
    public void DespawnTileMap(TilemapController inputTilemap) { tilemapPool.DespawnNotEnqueue(inputTilemap); }

    // 스폰 간격을 조정할 코루틴 생성
    private IEnumerator SpawnTilemapForLoop()
    {
        
        while (isSpawning)
        {
            // 풀에 객체가 존재하면 반복
            if (tilemapPool.Pool.Any())
            {
                
                SpawnTilemap();

                // 처음에만 다른 딜레이 시간 입력
                if (isFirstSpawn) 
                {
                    isFirstSpawn = false;
                    yield return new WaitForSeconds(firstDelay); 
                }
                else { yield return spawnDelay; }
            }
            // 객체가 없을 경우 실행할 내용
            else
            {
                isSpawning = false;
                yield break;
            }

        }
    }
}
