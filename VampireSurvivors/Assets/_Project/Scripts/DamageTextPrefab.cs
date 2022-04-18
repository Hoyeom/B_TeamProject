using System;
using DG.Tweening;
using UnityEngine;

public class DamageTextPrefab : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    public string sortingLayerName = "UI";
    public int sortingOrder = 1;

    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }

    public void SpawnText(int damage,Vector2 pos)
    {
        transform.position = pos;
        textMesh.text = $"{damage}";
        textMesh.color = Color.white;
        transform.DOMove(transform.position + Vector3.up, 1);
        DOTween.To(() => textMesh.color, x => textMesh.color = x, Color.clear, 1)
            .OnComplete(() => ObjectPooler.Instance.DestroyGameObject(gameObject));
    }
}
