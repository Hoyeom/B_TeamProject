using UnityEngine;

public class Knife : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;
    
    /// <summary>
    /// 바라보는 방향으로 발사체 발사
    /// </summary>
    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(-.2f, .2f)); // 위치 지정
        tempPrefab.transform.rotation = Player.viewRotation; // 방향 지정

        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.might = GetMight();
        stat.penetrate = GetPenetrate();
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

    

    
}