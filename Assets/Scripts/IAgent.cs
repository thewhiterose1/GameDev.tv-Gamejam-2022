public interface IAgent
{
    public float health { get; set; }
    public float baseAttack { get; set; }
    public float moveSpeed { get; set; }
    public string name { get; set; }

    void AttackNearest();

    void Idle();

    void DefaultAttack();

    void Die();
}