using UnityEngine;

public class BulletProjectile : ProjectilePrefab
{
    [HideInInspector] public GameObject target = null;
    public int rotateSpeed = 90;

    private float axis = 0;

    private void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        //gameObject.transform.LookAt(target.transform);
        //Vector2 direction = new Vector2(
        //        transform.position.x - target.transform.position.x,
        //        transform.position.y - target.transform.position.y);

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        //transform.rotation = rotation;

        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        targetPos.z = myPos.z;

        Vector3 vectorToTarget = targetPos - myPos;
        Vector3 quaternionToTarget = Quaternion.Euler(0, 0, axis) * vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Debug.Log("추적");

        }
        //gameObject.transform.LookAt(target);
        //보는 방향대 bulletSpeed로 날라감
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime, Space.Self);
        //transform.Translate(dis * speed * Time.fixedDeltaTime, Space.Self);
    }
}