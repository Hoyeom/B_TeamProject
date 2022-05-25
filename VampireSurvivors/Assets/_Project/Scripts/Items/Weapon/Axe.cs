using UnityEngine;

public class Axe : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    /// <summary>
    /// 위로 발사체 발사 후 중력 적용을 받아 떨어진다
    /// </summary>
    /// <param name="i"></param>
    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(-.2f, .2f)); // 위치 지정

        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.might = GetMight();
        stat.penetrate = GetPenetrate();
        stat.transform.localScale = Vector3.one * GetArea();
        stat.GetComponent<CircleCollider2D>().radius = GetArea() / 10;
        stat.rigid.AddForce(((Vector2.up) + Vector2.right * Random.Range(-.4f, .4f)) * speed, ForceMode2D.Impulse);
        stat.rigid.AddTorque(Random.Range(-90f, 90f));
    }

    protected override void Level2()
    {
        amount++;
    }

    protected override void Level3()
    {
        minMight += 20;   
        maxMight += 20;
    }

    protected override void Level4()
    {
        penetrate += 2;
    }

    protected override void Level5()
    {
        amount++;
    }

    protected override void Level6()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void Level7()
    {
        penetrate += 2;
    }

    protected override void Level8()
    {
        minMight += 20;
        maxMight += 20;
    }

    

    
}