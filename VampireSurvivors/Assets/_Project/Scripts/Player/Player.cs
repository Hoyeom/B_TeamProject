using System;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private static readonly int HashIsMove = Animator.StringToHash("isMove");
    private int enemyLayer;
    
    public PlayerStatRank playerStatRank;
    private Vector2 moveVector;
    
    private V_PlayerInput _playerInput;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigid;
    private Animator _anim;

    public Item[] _item;

    public int level;
    [SerializeField] private float maxExp;
    [SerializeField] private float thisExp;

    #region PlayerDefaultStat
    
    private float maxHealth = 50f;
    [SerializeField] private float health;
    private float moveSpeed = 3f; // 랭크당 이동속도 5% 증가
    private float magnetRadius = 1f; // 랭크당 획득반경 25% 증가
    private bool hitDelay;
    #endregion

    public Quaternion viewRotation;
    
    public UnityEvent onPlayerDead;

    public AudioClip soundPickUpEXP;
    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        
        _renderer = GetComponentInChildren<SpriteRenderer>();
        playerStatRank = new PlayerStatRank();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        health = maxHealth;
        
        ItemMagnetStart();
        
        InputSystemReset();;
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        _rigid.MovePosition((Vector2) transform.position + moveVector *
            playerStatRank.GetMoveSpeed(moveSpeed) * Time.fixedDeltaTime);
    }

    public void AddExp(float exp)
    {
        AudioManager.instance.AudioPlay(soundPickUpEXP);
        thisExp += exp;
        LevelUp();
    }

    private void LevelUp()
    {
        if (maxExp <= thisExp)
        {
            maxExp *= 2;
            level++;
            LevelUp();
        }
    }

    #region ItemMagnet
    private void ItemMagnetStart()
    {
        StartCoroutine(ItemMagnet());
    }

    IEnumerator ItemMagnet()
    {
        int itemLayer = 1 << LayerMask.NameToLayer("Item");

        while (true)
        {
            foreach (var hit in Physics2D.CircleCastAll(transform.position,magnetRadius,Vector2.zero,Mathf.Infinity,itemLayer))
            {
                hit.collider.GetComponent<Experience>()?.GoPlayer(transform);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }    
    
    #endregion
    
    #region AwakeFunc

    private void InputSystemReset()
    {
        _playerInput = new V_PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += Move_performed;
        _playerInput.Player.Move.canceled += Move_canceled;
    }
    
    #endregion
    
    #region InputCallbackFunc

    private void Move_canceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        _anim.SetBool(HashIsMove, false);
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        // Item Rotation
        
        Vector2 view = context.ReadValue<Vector2>();
        
        float angle = Mathf.Atan2(view.y, view.x) * Mathf.Rad2Deg;
        
        viewRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Move
        moveVector = context.ReadValue<Vector2>();
        if(moveVector.x == 0) return;
        _renderer.flipX = moveVector.x < 0;
        _anim.SetBool(HashIsMove, true);
    }
    #endregion

    IEnumerator HitDealay(float time)
    {
        hitDelay = true;
        yield return new WaitForSeconds(time);
        hitDelay = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.layer != enemyLayer) return;
        if (!hitDelay)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            health -= enemy.damage;
            if (health <= 0)
                onPlayerDead.Invoke();
            StartCoroutine(HitDealay(0.1f));
        }
    }

    public void TestSceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }
}
