using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public ArmyManager manager;
    protected GameObject target;
    
    [SerializeField] protected float damagePoints;
    [SerializeField] protected float healthPoints;
    [SerializeField] protected float attackRate;

    protected GameObject GetNearestEnemyUnit() => manager.GetNearestEnemyUnit(transform.position);
}
