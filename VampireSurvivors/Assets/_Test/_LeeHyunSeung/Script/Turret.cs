using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletOj;         // ź�� ������Ʈ
    public float bulletRate = 2f;       // ź�� ������
    private float timeRate = 1.5f;      // �ֱ� �߻��� �ð�

    private Transform target;           // �߻��� ���


    private void Update()
    {
        
        Fire();
    }

    private void Fire()
    {
        // timeRate  ����
        timeRate += Time.deltaTime;

        if (timeRate >= bulletRate)
        {
            // ���� �ð� ����
            timeRate = 0f;

            target = GameObject.FindGameObjectWithTag("Enemy").transform;   // �߻��� ����� ��ġ
            //BulletOj �� �������� transfor.position ��ġ�� transfor.rotation ȸ������ ����
            GameObject bullet = Instantiate(bulletOj, transform.position, target.transform.rotation);
            //������ bullet ���ӿ�����Ʈ�� ���� ������ target�� ���ϵ��� ȸ��
            bullet.transform.LookAt(target);
        }
        Destroy(gameObject, 10f);
    }
}
