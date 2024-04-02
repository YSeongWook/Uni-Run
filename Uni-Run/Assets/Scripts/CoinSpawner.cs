using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin")]
    public GameObject coinPrefab;           // 코인 프리팹
    public GameObject tripleCoinPrefab;     // 트리플 코인 프리팹
    public int tripleCoinProbability = 33;  // 트리플 코인 생성 확률 (%)

    [Space(10)]
    [Header("Spawn Time")]
    [SerializeField] float timeBetSpawnMin = 1.25f;   // 다음 배치까지의 시간 간격 최솟값
    [SerializeField] float timeBetSpawnMax = 2.25f;   // 다음 배치까지의 시간 간격 최댓값

    [Space(10)]
    [Header("Position Range")]
    [SerializeField] float xMin = 5f;   // 생성할 위치의 최소 x값
    [SerializeField] float xMax = 20f;  // 생성할 위치의 최대 x값
    [SerializeField] float yMin = 1.5f; // 생성할 위치의 최소 y값
    [SerializeField] float yMax = 3f;   // 생성할 위치의 최대 y값

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            // 코인을 생성할 위치를 랜덤하게 선택
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);

            // 발판과 겹치는지 확인
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(xPos, yPos), 1f);
            bool overlap = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Platform") || collider.CompareTag("Dead"))
                {
                    overlap = true;
                    break;
                }
            }

            // 발판과 겹치지 않는 위치에 코인 생성
            if (!overlap)
            {
                // 0 ~ 99 사이의 랜덤 값 생성
                int randomValue = Random.Range(0, 100);

                // 트리플 코인 생성 여부 결정
                if (randomValue < tripleCoinProbability)
                {
                    // 트리플 코인 생성
                    Instantiate(tripleCoinPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
                }
                else
                {
                    // 일반 코인 생성
                    Instantiate(coinPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
                }
            }

            // 다음 코인 생성까지의 대기 시간
            yield return new WaitForSeconds(Random.Range(timeBetSpawnMin, timeBetSpawnMax));
        }
    }
}
