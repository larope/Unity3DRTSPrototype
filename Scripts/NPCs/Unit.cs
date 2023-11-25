using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected ArmyManager manager;
    protected GameObject target;

    [SerializeField] protected float damagePoints;
    [SerializeField] protected float healthPoints;
}
