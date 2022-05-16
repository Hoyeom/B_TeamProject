using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test 싱글톤 테스트
public abstract class TestSingleTon<T> : MonoBehaviour where T : TestSingleTon<T>
{
    public static T Inst;

    protected virtual void Awake()
    {
        if(Inst != null)
        {
            Destroy(this);
            return;
        }

        Inst = this as T;
        DontDestroyOnLoad(this);  // 자율
    }
}
