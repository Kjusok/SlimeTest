using UnityEngine;
/// <summary>
/// Атака игрока
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void Attack()
    {
        _player.SpawnProjectile();
    }
}
