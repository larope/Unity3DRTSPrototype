using System.Collections;
using UnityEngine;

public class Tower : Building
{ 
    private enum towerState
    {
        searchEnemy,
        attack
    }

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private Transform _horizontalRotationObject;
    [SerializeField] private Transform _verticalRotationObject;
    
    [SerializeField] private towerState _state;

    private void Start()
    {
        StartCoroutine(SearchEnemyIE());
    }

    IEnumerator SearchEnemyIE()
    {
        GameObject enemy = null;
        while (enemy == null)
        {
            enemy = GetNearestEnemyUnit();
            yield return new WaitForSeconds(0.3f);
        }

        target = enemy;
        ChangeState(towerState.attack);
    }
    IEnumerator AttackIE()
    {
        while (target != null)
        {
            Attack();
            yield return new WaitForSeconds(1/attackRate);
        }
        ChangeState(towerState.attack);
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(_bullet, _shootingPoint.position, Quaternion.identity);
        bullet.GetComponent<HomingProjectile>().setTarget(target.transform);
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

    private void Update()
    {
        if (_state == towerState.attack)
        {
            _verticalRotationObject.LookAt(new Vector3(target.transform.position.x, _verticalRotationObject.position.y, target.transform.position.z));
            _horizontalRotationObject.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));

        }
    }
}
