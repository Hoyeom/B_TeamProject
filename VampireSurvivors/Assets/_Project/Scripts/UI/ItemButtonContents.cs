using UnityEngine;

public class ItemButtonContents : MonoBehaviour
{
    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
