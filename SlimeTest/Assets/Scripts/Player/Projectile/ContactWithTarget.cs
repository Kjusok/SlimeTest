using UnityEngine;

/// 1) Нанисение урона снарядом
/// 2) Косание со врагом
/// 3) Создание эффекта взырва снаряда

public class ContactWithTarget:MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private GameObject _explosionPrefab;

    
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeDamage(_projectile.Damage);

            Destroy(gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}