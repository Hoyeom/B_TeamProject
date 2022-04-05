using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarilcMove : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Vector2 _moveVector;
    private float playerStatRank;
    private float moveSpeed;
    public GameObject Player;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        transform.position = Player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Player.transform.position;
    }
    
}
