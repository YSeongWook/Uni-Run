using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // 인스턴스가 null이면
            if(instance == null)
            {
                // 만약 scene에 이미 해당 타입의 인스턴스가 있으면
                instance = (T)FindObjectOfType<T>();

                if(instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();

                    // scene이 변경되더라도 파괴되지 않도록
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

}
