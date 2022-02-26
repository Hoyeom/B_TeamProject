using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private PlayerStatRank _playerStatRank;

    private Vector2 moveVector;
    
    private V_PlayerInput _playerInput;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigid;
    
    private float moveSpeed = 3;


    public int testRank;
    
    private void Awake()
    {
        _playerStatRank = new PlayerStatRank();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        
        InputSystemReset();;
        _playerInput.Player.Test.started += Test_started;
    }

    private void Test_started(InputAction.CallbackContext obj)
    {
        _playerStatRank.SetMoveSpeed(testRank);
    }

    private void FixedUpdate()
    {
        _rigid.MovePosition((Vector2) transform.position + moveVector *
            _playerStatRank.GetMoveSpeed(moveSpeed) * Time.fixedDeltaTime);
    }

    private void InputSystemReset()
    {
        _playerInput = new V_PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += Move_performed;
        _playerInput.Player.Move.canceled += Move_canceled;
    }



    #region InputCallbackFunc
    
    private void Move_canceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        if(moveVector.x == 0) return;
        _renderer.flipX = moveVector.x < 0;
    }
    #endregion
}
