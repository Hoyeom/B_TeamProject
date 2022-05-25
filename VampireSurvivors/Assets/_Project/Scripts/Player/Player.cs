using System;
using System.Collections;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAttackable
{
    private static readonly int HashIsMove = Animator.StringToHash("isMove");
    private const int EnemyLayer = 6; // "Enemy Layer" 6번

    public Transform model;
    public PlayerStatRank playerStatRank;   // 스탯 가져오기
    private Vector2 _moveVector;        // 이동

    private V_PlayerInput _playerInput; // 인풋시스템
    private Rigidbody2D _rigid; 
    private Animator _anim; 

    private int _level = 1;   // 레벨 기본값

    
    /// <summary>
    /// 레벨업할 경우 레벨 변경 이벤트 호출
    /// </summary>
    public int Level
    {
        get => _level;
        set
        {
            if (value > _level)
                Managers.Item.GetRandItem();
            _level = value;
            
            OnChangeLevel?.Invoke(_level);
        }
    }

    public event Action<int> OnChangeLevel;

    private float _maxExp = 10;  // 최대 경험치
    private float _curExp;  // 현재 경험치
    
    /// <summary>
    /// 현재 경험치 최대 경험치 초과시 레벨업
    /// </summary>
    public float Exp
    {
        get => _curExp;
        set
        {
            _curExp = value;
            OnChangeExp?.Invoke(_curExp, _maxExp);
            
            if (_maxExp <= _curExp && _levelUpRoutine == null) 
                _levelUpRoutine = StartCoroutine(CoLevelUp());
        }
    }
    
    private Coroutine _levelUpRoutine;

    public event Action<float, float> OnChangeExp;

    #region PlayerDefaultStat

    private float maxHealth = 50;
    private float health;

    private float moveSpeed = 3f; // 랭크당 이동속도 5% 증가
    private float magnetRadius = 1f; // 랭크당 획득반경 25% 증가
    private bool _hitDelay;

    /// <summary>
    /// 변경시 체력 변경 이벤트 호출
    /// </summary>
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            OnChangeHealth?.Invoke(Health, MaxHealth);
        }
    }
    
    /// <summary>
    /// 변경시 체력 변경 이벤트 호출
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            health = value;
            OnChangeHealth?.Invoke(Health, MaxHealth);
        }
    }
    
    public event Action<float, float> OnChangeHealth;
    
    #endregion

    internal Quaternion viewRotation; // 플레이어 방향

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
        Managers.Resource.Instantiate("UI/ExpUI");
        Managers.Item.InGameInit();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
        
    private void OnDrawGizmos()
    {
        // 자석 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
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

    #region Move

    /// <summary>
    /// Rigid.MovePosition 현재 속도(moveSpeed) 비례
    /// </summary>
    private void Move()
    {
        _rigid.MovePosition((Vector2) transform.position + _moveVector *
            playerStatRank.GetMoveSpeed(moveSpeed) * Time.fixedDeltaTime);
    }

    #endregion
    
    #region LevelUp

    /// <summary>
    /// 경험치 추가
    /// </summary>
    /// <param name="exp">추가할 경험치 양</param>
    public void AddExp(float exp)
    {
        Managers.Audio.FXPlayerAudioPlay(expPickUpClip);
        Exp += exp;
    }

    /// <summary>
    /// 현재 경험치가 최대 경험치 양보다 많을 경우 레벨업 이벤트 호출
    /// </summary>
    IEnumerator CoLevelUp()
    {
        while (_maxExp<=Exp)
        {
            onPlayerLevelUp.Invoke();
            
            while (Time.timeScale==0)
            {
                yield return null;
            }
            
            _maxExp *= 1.2f;
            Level++;
            yield return null;
        }

        _levelUpRoutine = null;
        yield return null;
    }

    #endregion

    #region ItemMagnet

    /// <summary>
    /// 자석 효과(아이템 끌어오기) 시작
    /// </summary>
    private void ItemMagnetStart()
    {
        StartCoroutine(CoItemMagnet());
    }

    /// <summary>
    /// Item 레이어 활용한 아이템 끌어오기
    /// </summary>
    IEnumerator CoItemMagnet()
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

    /// <summary>
    /// InputSystem 초기화
    /// </summary>
    private void InputSystemReset()
    {
        _playerInput = new V_PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += Move_performed;
        _playerInput.Player.Move.canceled += Move_canceled;
    }

    #endregion

    #region InputCallbackFunc

    /// <summary>
    /// Move 입력버튼 PressUp
    /// </summary>
    /// <param name="context">Vector2 Read</param>
    private void Move_canceled(InputAction.CallbackContext context)
    {
        _moveVector = Vector2.zero;
        _anim.SetBool(HashIsMove, false);
    }
    /// <summary>
    /// Move 버튼 현재 값 (아날로그)
    /// </summary>
    /// <param name="context">Vector2 Read</param>
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
    
    /// <summary>
    /// 피격 딜레이 코루틴
    /// </summary>
    /// <param name="time">무적 시간(초)</param>
    IEnumerator HitDealay(float time)
    {
        _hitDelay = true;
        yield return new WaitForSeconds(time);
        _hitDelay = false;
    }

    /// <summary>
    /// 플레이어 피격
    /// </summary>
    /// <param name="damage">플레이어가 받을 데미지</param>
    public void AttackChangeHealth(float damage)
    {
        if (_hitDelay) return;
        Health -= damage;
        if (Health <= 0)
            onPlayerDead.Invoke();
        StartCoroutine(HitDealay(0.05f));
    }
    
    /// <summary>
    /// 플레이어 회복
    /// </summary>
    /// <param name="heal">회복할 값</param>
    public void HealPlayer(float heal)
    {
        Health = Mathf.Min(Health + heal, MaxHealth);
    }
    #endregion
}