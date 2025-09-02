using UnityEngine;

public interface IUnitBase
{
    abstract void Attack(Unit target);
    abstract void TakeDamage(int dmg);
    abstract void Move(int move);
    abstract void Die();
    abstract void Spawn(string _faction, bool _isOnTopTrack);
}