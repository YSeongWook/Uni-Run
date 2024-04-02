using UnityEngine;

public class TripleCoin : MonoBehaviour
{
    private void Update()
    {
        // 부모 코인에 자식 코인이 있는지 확인
        bool allChildrenInactive = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                allChildrenInactive = false;
                break;
            }
        }

        // 모든 자식 코인이 비활성화되었다면 부모 코인 삭제
        if (allChildrenInactive)
        {
            Destroy(gameObject);
        }
        // 모든 자식 코인이 비활성화 되지 않아도 3초 이후에 삭제
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}
