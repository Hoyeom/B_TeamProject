using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPrefab : MonoBehaviour
{
    public GameObject attackPrefab;
    public int power;   //데미지
    public int divide; //분할 횟수
    private int divide_angle;
    private float delay;    //분할 주기
    private void FixedUpdate()
    {
   
        transform.Translate(Vector2.left * 5 * Time.fixedDeltaTime);
        delay++;
        if (divide > 0 && delay>50)
        {
            
            divide_angle = UnityEngine.Random.Range(0, 45); //0~45 랜덤
            divide--;
    
            GameObject skull_1 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_1.transform.position = transform.position;
            skull_1.transform.Rotate(0, 0, divide_angle);

            GameObject skull_2 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_2.transform.position = transform.position;
            skull_2.transform.Rotate(0, 0, divide_angle+90);

            GameObject skull_3 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_3.transform.position = transform.position;
            skull_3.transform.Rotate(0, 0, divide_angle+180);

            GameObject skull_4 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_4.transform.position = transform.position;
            skull_4.transform.Rotate(0, 0, divide_angle+270);

            ObjectPooler.Instance.DestroyGameObject(gameObject);
        }
    }

}
