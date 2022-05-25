public class PlayerStatRank
{
    #region Rank

    private int _might; // 공격력
    private int _armor; // 방어력
    private int _maxHealth; // 최대 체력
    private int _recovery; // 체력 재생
    private int _cooldown; // 쿨타임
    private int _area; // 범위
    private int _speed; // 프리펩 이동 속도
    private int _duration; // 프리펩 지속시간
    private int _amounts; // 프리펩 개수
    private int _moveSpeed; // 플레이어 이동 속도
    private int _magnet; // 경험치 자석 효과 범위

    #endregion

    #region IncreaseValue

    private float might = 0.05f; // 랭크당 공격력 5% 증가
    private float armor = 1; // 랭크당 피격 데미지 1 감소
    private float maxHealth = 0.10f; // 랭크당 체력 10% 증가
    private float recovery = 0.1f; // 랭크당 체력 회복 0.1 증가
    private float cooldown = .025f; // 랭크당 쿨타임 2.5% 감소
    private float area = 0.05f; // 랭크당 범위 5% 증가
    private float speed = 0.1f; // 랭크당 투사체 속도 10% 증가
    private float duration = 0.15f; // 랭크당 지속시간 15% 증가
    private float amounts = 1; // 랭크당 투사체 개수 1 증가
    private float moveSpeed = 0.05f; // 랭크당 이동속도 5% 증가
    private float magnet = 0.25f; // 랭크당 획득반경 25% 증가

    #endregion


    
    public PlayerStatRank(int might = 0, int armor = 0, int maxHealth = 0, int recovery = 0, int cooldown = 0,
        int area = 0, int speed = 0, int duration = 0, int amounts = 0, int moveSpeed = 0, int magnet = 0)
    {
        _might = might;
        _armor = armor;
        _maxHealth = maxHealth;
        _recovery = recovery;
        _cooldown = cooldown;
        _area = area;
        _speed = speed;
        _duration = duration;
        _amounts = amounts;
        _moveSpeed = moveSpeed;
        _magnet = magnet;
    }

    #region GetValue

    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetMight(float p_might) => p_might + p_might * (_might * might);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetArmor(float p_armor) => p_armor + (_armor * armor);

    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>public float GetMaxHealth(float p_maxHealth) => p_maxHealth + p_maxHealth * (_maxHealth * maxHealth);
    public float GetRecovery(float p_recovery) => p_recovery + (_recovery * recovery);
    
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetCooldown(float p_cooldown) => p_cooldown - p_cooldown * (_cooldown * cooldown);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetArea(float p_area) => p_area + p_area * (_area * area);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetSpeed(float p_speed) => p_speed + p_speed * (_speed * speed);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetDuration(float p_duration) => p_duration + p_duration * (_duration * duration);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetAmounts(float p_amounts) => p_amounts + (_amounts * amounts);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetMoveSpeed(float p_moveSpeed) => p_moveSpeed + p_moveSpeed * (_moveSpeed * moveSpeed);
    /// <summary>
    /// 아이템과 플레이어의 랭크가 계산된 값
    /// </summary>
    public float GetMagnet(float p_magnet) => p_magnet + magnet * (_magnet * magnet);

    #endregion

    #region SetValue

    public void SetMight(int rank) => _might = rank;
    public void SetArmor(int rank) => _armor = rank;
    public void SetMaxHealthRank(int rank) => _maxHealth = rank;
    public void SetRecovery(int rank) => _recovery = rank;
    public void SetCooldown(int rank) => _cooldown = rank;
    public void SetArea(int rank) => _area = rank;
    public void SetSpeed(int rank) => _speed = rank;
    public void SetDuration(int rank) => _duration = rank;
    public void SetAmounts(int rank) => _amounts = rank;
    public void SetMoveSpeed(int rank) => _moveSpeed = rank;
    public void SetMagnet(int rank) => _magnet = rank;

    #endregion
}