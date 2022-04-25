using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDividePrefab : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * 5 * Time.fixedDeltaTime);
    }
}
