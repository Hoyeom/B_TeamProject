using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test 싱글톤 테스트
public abstract class TestSingleTon<T> : MonoBehaviour where T : TestSingleTon<T>
{
    public static T Inst;
    protected Rigidbody2D rigid;
    protected SpriteRenderer _renderer;
    protected Animator _animator;

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

    public void MgrInfo()
    {
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
}
