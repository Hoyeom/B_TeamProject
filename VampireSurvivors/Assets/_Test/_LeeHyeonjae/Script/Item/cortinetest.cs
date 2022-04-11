using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cortinetest : MonoBehaviour
{
    
    private IEnumerator m_Coroutine;
    private bool m_IsBreak; // 무한반복문 종료를 위한 변수

    void Start()
    {
        m_Coroutine = CoroutineMethod();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(m_Coroutine);

        if (Input.GetKeyDown(KeyCode.B))
            m_IsBreak = true; // 무한반복문 종료

    }

    IEnumerator CoroutineMethod()
    {
        int count = 0;

        while (!m_IsBreak)
        {
            Debug.Log(count);
            yield return new WaitForSeconds(1.0f);
            count++;
        }
    }

}
