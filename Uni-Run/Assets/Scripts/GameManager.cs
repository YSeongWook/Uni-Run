using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : Singleton<GameManager>
{
    public bool isGameover = false;     // 게임 오버 상태
    public TextMeshProUGUI scoreText;   // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI;       // 게임 오버시 활성화 할 UI 게임 오브젝트

    private int score = 0;              // 게임 점수

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // 현재 활성화 되어있는 Scene를 재시작(Main Scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;  // 점수를 증가
            scoreText.text = "Score: " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}