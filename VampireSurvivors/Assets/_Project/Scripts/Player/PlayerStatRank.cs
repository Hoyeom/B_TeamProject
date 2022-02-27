
using UnityEngine;

public class PlayerStatRank
{
    #region Rank

    private int r_might;
    private int r_armor;
    private int r_maxHealth;
    private int r_recovery;
    private int r_cooldown;
    private int r_area;
    private int r_speed;
    private int r_duration;
    private int r_amounts;
    private int r_moveSpeed;
    private int r_magnet;

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

    public PlayerStatRank()
    {
        r_might = 0;
        r_armor = 0;
        r_maxHealth = 0;
        r_recovery = 0;
        r_cooldown = 0;
        r_area = 0;
        r_speed = 0;
        r_duration = 0;
        r_amounts = 0;
        r_moveSpeed = 0;
        r_magnet = 0;
    }

    #region GetValue

    public float GetMight(float p_might) => p_might + p_might * (r_might * might);
    public float GetArmor(float p_armor) => p_armor + (r_armor * armor);
    public float GetMaxHealth(float p_maxHealth) => p_maxHealth + p_maxHealth * (r_maxHealth * maxHealth);
    public float GetRecovery(float p_recovery) => p_recovery + (r_recovery * recovery);
    public float GetCooldown(float p_cooldown) => p_cooldown + p_cooldown * (r_cooldown * cooldown);
    public float GetArea(float p_area) => p_area + area * (r_area * area);
    public float GetSpeed(float p_speed) => p_speed + p_speed * (r_speed * speed);
    public float GetDuration(float p_duration) => p_duration + p_duration * (r_duration * duration);
    public float GetAmounts(float p_amounts) => p_amounts + (r_amounts * amounts);
    public float GetMoveSpeed(float p_moveSpeed) => p_moveSpeed + p_moveSpeed * (r_moveSpeed * moveSpeed);
    public float GetMagnet(float p_magnet) => p_magnet + magnet * (r_magnet * magnet);
    #endregion

    #region SetValue
    public void SetMight(int rank) => r_might = rank;
    public void SetArmor(int rank) => r_armor = rank;
    public void SetMaxHealthRank(int rank) => r_maxHealth = rank;
    public void SetRecovery(int rank) => r_recovery = rank;
    public void SetCooldown(int rank) => r_cooldown = rank;
    public void SetArea(int rank) => r_area = rank;
    public void SetSpeed(int rank) => r_speed = rank;
    public void SetDuration(int rank) => r_duration = rank;
    public void SetAmounts(int rank) => r_amounts = rank;
    public void SetMoveSpeed(int rank) => r_moveSpeed = rank;
    public void SetMagnet(int rank) => r_magnet = rank;
    #endregion
}