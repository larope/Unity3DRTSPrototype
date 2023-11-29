using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerBullet : MonoBehaviour, HomingProjectile
{
    [SerializeField] private float _damage;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;

    private Transform _target;

    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _minSpeed;
    }

    public void setTarget(Transform target)
    {
        _target = target;
    }

    private float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        time = Math.Clamp(time+Time.deltaTime/_acceleration, 0, 1);
        float t = -time * time * time + 1;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _currentSpeed * Time.deltaTime);
        _currentSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, t);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable IDamageableComponent = other.gameObject.GetComponent<IDamageable>();
        if (IDamageableComponent != null && !other.gameObject.CompareTag(gameObject.tag)) IDamageableComponent.TakeDamage(_damage);
        Explode();
    }

    private void Explode() => Destroy(gameObject);
    
}