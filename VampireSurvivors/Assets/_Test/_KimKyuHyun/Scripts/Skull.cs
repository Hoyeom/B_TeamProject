using UnityEngine;

public class Skull : MonoBehaviour
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    private Player player;
    private void FixedUpdate()
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.LookAt(player.transform); // 방향 지정

        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
    }

}