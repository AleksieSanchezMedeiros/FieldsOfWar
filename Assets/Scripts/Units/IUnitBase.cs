using UnityEngine;

public interface IUnitBase
{
    abstract void Attack(Unit target);
    abstract void TakeDamage(int dmg);
    abstract void Move();
    abstract void Die();
    abstract void Spawn();
}