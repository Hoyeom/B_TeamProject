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
    private const int EnemyLayer = 6; // "Enemy Layer" 6번

    public PlayerStatRank playerStatRank;
    private Vector2 _moveVector;

    private V_PlayerInput _playerInput;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigid;
    private Animator _anim;

    public GameObject[] items = new GameObject[6];      // 최대 아이템 6개

    public int level = 1;
    [SerializeField] private float maxExp;
    [SerializeField] private float minExp;
    [SerializeField] private float thisExp;

    #region PlayerDefaultStat
    
    private float maxHealth = 50f;
    [SerializeField] private float health;
    private float moveSpeed = 3f; // 랭크당 이동속도 5% 증가
    private float magnetRadius = 1f; // 랭크당 획득반경 25% 증가
    private bool _hitDelay;
    #endregion
    
    internal Quaternion viewRotation;   // 플레이어 방향
    
    public UnityEvent onPlayerDead;     // 죽을때 호출

    public AudioClip expPickUpClip;
    
    private void Awake()
    {
        playerStatRank = new PlayerStatRank();
        
        _renderer = GetComponentInChildren<SpriteRenderer>();
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

    #region Move

    private void Move()
    {
        _rigid.MovePosition((Vector2) transform.position + _moveVector *
            playerStatRank.GetMoveSpeed(moveSpeed) * Time.fixedDeltaTime);
    }

    #endregion

    #region Item

    public void GetItem(Item item)
    {
        item.LevelUp();
    }

    // 아이템에 필요한 함수 생각중
    // 획득 = 레벨업
    // 해당 아이템이 없다면 = bool false

    #endregion
    
    
    #region LevelUp
    public void AddExp(float exp)
    {
        AudioManager.Instance.AudioPlay(expPickUpClip);
        // UIManager.Instance.SetPickExpSlider(thisExp);
        thisExp += exp;
        LevelUp();
    }

    private void LevelUp()
    {
        if (maxExp <= thisExp)
        {
            // 아이템 선택 함수
            // 아이템 선택후 아래 코드실행
            minExp = maxExp;
            maxExp *= 2;
            level++;
            UIManager.Instance.SetLevelUp(minExp, maxExp, level);
            LevelUp();
        }
        UIManager.Instance.SetPickExp(thisExp);
    }
    

    #endregion
    
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
        _moveVector = Vector2.zero;
        _anim.SetBool(HashIsMove, false);
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        // Item Rotation
        
        Vector2 view = context.ReadValue<Vector2>();
        
        float angle = Mathf.Atan2(view.y, view.x) * Mathf.Rad2Deg;
        
        viewRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Move
        _moveVector = context.ReadValue<Vector2>();
        if(_moveVector.x == 0) return;
        _renderer.flipX = _moveVector.x < 0;
        _anim.SetBool(HashIsMove, true);
    }
    #endregion

    #region PlayerHit

    IEnumerator HitDealay(float time)
    {
        _hitDelay = true;
        yield return new WaitForSeconds(time);
        _hitDelay = false;
    }
    
    #endregion    
    
    // 후에 상대가 공격 입력하도록 변경 예정
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer != EnemyLayer) return;
        if (!_hitDelay)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            health -= enemy.damage;
            if (health <= 0)
                onPlayerDead.Invoke();
            StartCoroutine(HitDealay(0.1f));
        }
    }


    private void OnDrawGizmos()
    {
        // 자석 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }

    public void TestSceneReset()    // OnPlayerDead Event로 호출중
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
