using UnityEngine;

public class EnemyContactWithPlayer : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _enemy.IsAttack = true;
            _enemiesAnimations.Attack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _enemy.IsAttack = false;
            _enemiesAnimations.Attack(false);
        }
    }
}