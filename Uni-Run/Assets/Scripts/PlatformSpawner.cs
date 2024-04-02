using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f;   // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f;   // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn;             // 다음 배치까지의 시간 간격

    public float yMin = -3.5f;  // 배치할 위치의 최소 y값
    public float yMax = 1.5f;   // 배치할 위치의 최대 y값
    private float xPos = 20f;   // 배치할 위치의 x 값

    private Queue<GameObject> platformPool = new Queue<GameObject>();   // 미리 생성한 발판들, 오브젝트 풀링
    private Vector2 poolPosition = new Vector2(0, -25);                 // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime;                                        // 마지막 배치 시점

    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        for(int i = 0; i < count; i++)
        {
            GameObject platform = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            // yield return new WaitForSeconds(Random.Range(timeBetSpawnMin, timeBetSpawnMax));

            float yPos = Random.Range(yMin, yMax);

            if (!GameManager.Instance.isGameover)
            {
                GameObject platform = platformPool.Dequeue();
                platformPool.Enqueue(platform);
                platform.SetActive(false);
                platform.SetActive(true);

                platform.transform.position = new Vector2(xPos, yPos);
            }

            yield return new WaitForSeconds(Random.Range(timeBetSpawnMin, timeBetSpawnMax));
        }
    }
}