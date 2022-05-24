public interface IAgent
{
    public int health { get; set; }
    public int baseAttack { get; set; }
    public float moveSpeed { get; set; }
    public string name { get; set; }

    void AttackNearest();

    void Idle();

    void DefaultAttack();

    void Die();
}