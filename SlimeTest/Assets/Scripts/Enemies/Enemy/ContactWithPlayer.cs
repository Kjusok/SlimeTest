using UnityEngine;

public class ContactWithPlayer : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _enemy.AttackState(true);
            _enemiesAnimations.Attack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _enemy.AttackState(false);
            _enemiesAnimations.Attack(false);
        }
    }
}