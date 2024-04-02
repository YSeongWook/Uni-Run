using UnityEngine;

public class Coin : MonoBehaviour
{
    public float respawnTime = 3f; // 코인 재생성 시간

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // 코인을 비활성화
            this.gameObject.SetActive(false);

            GameManager.Instance.AddScore(10);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}
