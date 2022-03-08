using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private static readonly int HashIsMove = Animator.StringToHash("isMove");
    private const int EnemyLayer = 6; // "Enemy Layer" 6번

    public Transform model;
    public PlayerStatRank playerStatRank;
    private Vector2 _moveVector;

    private V_PlayerInput _playerInput;
    private Rigidbody2D _rigid;
    private Animator _anim;

    public int level = 1;
    private float maxExp = 10;
    private float minExp;
    private float thisExp;
    public float ThisExp
    {
        get => thisExp;
        set
        {
            thisExp = value;
            UIManager.Instance.SetExpValue(thisExp);
            
            if (maxExp <= thisExp && tempCoroutine == null) 
            {
                tempCoroutine = StartCoroutine(LevelUp());
            }
        }
    }

    #region PlayerDefaultStat

    private float maxHealth = 50;
    private float health;

    private float moveSpeed = 3f; // 랭크당 이동속도 5% 증가
    private float magnetRadius = 1f; // 랭크당 획득반경 25% 증가
    private bool _hitDelay;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            UIManager.Instance.SetMaxHp(maxHealth);
        }
    }
    public float Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.SetHpValue(health);
        }
    }
    
    #endregion

    internal Quaternion viewRotation; // 플레이어 방향

    private Coroutine tempCoroutine;
    
    public UnityEvent onPlayerDead; // 죽을때 호출
    public UnityEvent onPlayerLevelUp; // 레벨업 시 호출

    public AudioClip expPickUpClip;

    private void Awake()
    {
        playerStatRank = new PlayerStatRank();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        model = transform.GetChild(0);
        ItemMagnetStart();
        InputSystemReset();
    }

    private void Start()
    {
        Health = MaxHealth;
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
    
    #region LevelUp

    public void AddExp(float exp)
    {
        AudioManager.Instance.FXPlayerAudioPlay(expPickUpClip);
        ThisExp += exp;
    }

    IEnumerator LevelUp()
    {
        while (maxExp<=ThisExp)
        {
            onPlayerLevelUp.Invoke();
            
            while (Time.timeScale==0)
            {
                yield return null;
            }
            
            minExp = maxExp;
            maxExp *= 1.2f;
            level++;
            UIManager.Instance.SetMaxExp(minExp, maxExp, level);
            UIManager.Instance.SetExpValue(ThisExp);
            yield return null;
        }

        tempCoroutine = null;
        yield return null;
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
            foreach (var hit in Physics2D.CircleCastAll(transform.position, magnetRadius, Vector2.zero, Mathf.Infinity, itemLayer))
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
        //_playerInput.UI.Enable();
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
        if(Time.timeScale==0) return;
        
        // Item Rotation

        Vector2 view = context.ReadValue<Vector2>();

        float angle = Mathf.Atan2(view.y, view.x) * Mathf.Rad2Deg;

        viewRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Move
        _moveVector = context.ReadValue<Vector2>();
        if (_moveVector.x == 0) return;
        model.eulerAngles = _moveVector.x < 0 ? Vector3.down * 180 : Vector3.zero;
        _anim.SetBool(HashIsMove, true);
    }

    #endregion

    #region PlayerHit,Heal

    IEnumerator HitDealay(float time)
    {
        _hitDelay = true;
        yield return new WaitForSeconds(time);
        _hitDelay = false;
    }

    public void HealPlayer(float heal)
    {
        Health = Mathf.Min(Health + heal, MaxHealth);
    }
    #endregion

    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer != EnemyLayer) return;
        if (!_hitDelay)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            Health -= enemy.damage;
            
            if (Health <= 0)
                onPlayerDead.Invoke();
            StartCoroutine(HitDealay(0.05f));
        }
    }


    private void OnDrawGizmos()
    {
        // 자석 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }

    public void TestSceneReset() // OnPlayerDead Event로 호출중
    {
        ObjectPooler.Instance.AllDestroyGameObject();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        _playerInput?.Player.Enable();
        _playerInput?.UI.Enable();
    }

    private void OnDisable()
    {
        _playerInput?.Player.Disable();
        _playerInput?.UI.Disable();
    }
}