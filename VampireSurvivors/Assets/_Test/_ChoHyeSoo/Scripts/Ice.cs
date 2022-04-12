using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;
    Transform target = null;

    protected override void ActiveAttack(int i)
    {
        if (target == null) return;

        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(-.2f, .2f)); // 위치 지정
        //tempPrefab.transform.rotation = player.viewRotation; // 방향 지정
        
        Vector2 dir = target.position - tempPrefab.transform.position;
        float angle = Mathf.Atan2(dir.y + 0.5f, dir.x) * Mathf.Rad2Deg;
        tempPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.might = GetMight();
        stat.penetrate = GetPenetrate();
    }
    protected override void WeaponEquipFX()
    {

    }
    protected override void Level2()

    {
        amount++;
    }

    protected override void Level3()
    {
        amount++;
        minMight += 5;
        maxMight += 5;
    }
    protected override void Level4()
    {
        amount++;
    }
    protected override void Level5()
    {
        penetrate++;
    }
    protected override void Level6()
    {
        amount++;
    }
    protected override void Level7()
    {
        amount++;
        minMight += 5;
        maxMight += 5;
    }
    protected override void Level8()
    {
        penetrate++;
    }
    private void OnTriggerEnter2D(Collider2D col)
    { 
        if(col.CompareTag("Enemy"))
        {
            //Debug.Log("col in");
            target = col.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            //Debug.Log("col out");
            target = null;
        }
    }
}
