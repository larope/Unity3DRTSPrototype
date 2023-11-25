using System.Collections;
using UnityEngine;

public class Tower : Building
{ 
    private enum towerState
    {
        searchEnemy,
        attack
    }
    [SerializeField] private Transform _horizontalRotationObject;
    [SerializeField] private Transform _verticalRotationObject;

    [SerializeField] private towerState _state;

    IEnumerator SearchEnemyIE()
    {
        GameObject enemy = null;
        while (enemy == null)
        {
            enemy = GetNearestEnemyUnit();
            yield return new WaitForSeconds(300);
        }

        target = enemy;
        ChangeState(towerState.attack);
    }
    IEnumerator AttackIE()
    {
        IDamageable targetIDamageable = target.GetComponent<IDamageable>();
        while (target != null)
        {
            Attack(targetIDamageable);
            yield return new WaitForSeconds(attackRate);
        }
        ChangeState(towerState.attack);
    }

    private void Attack(IDamageable targetIDamageable)
    {
        targetIDamageable.TakeDamage(damagePoints);
    }
    private void ChangeState(towerState state)
    {
        _state = state;
        switch (_state)
        {
            case towerState.searchEnemy: 
                StopAllCoroutines();
                StartCoroutine(SearchEnemyIE());
                break;
            case towerState.attack:
                StopAllCoroutines();
                StartCoroutine(AttackIE());
                break;
        }
    }
}
