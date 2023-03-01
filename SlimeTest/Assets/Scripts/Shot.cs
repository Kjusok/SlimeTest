using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private const float TimeForDestroyShot = 5;

    [SerializeField] private int _speed;
    //[SerializeField] private ParticleSystem _sparksEffect;

    private float _lifeTimer = TimeForDestroyShot;

    public int Damage;


    private void Update()
    {
        _lifeTimer -= Time.deltaTime;

        if (_lifeTimer <= 0)
        {
            Destroy(gameObject);
        }

        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        // other.GetComponent<Warrior>().TakeDamage(Damage);
        var shot = other.GetComponent<Enemy>();

        if (shot)
        {
            shot.TakeDamage(Damage);

            Destroy(gameObject);


        }
        // SpawnSparksEffect();
    }

    //private void SpawnSparksEffect()
    //{
    //    var effect = Instantiate(_sparksEffect.gameObject, transform.position, Quaternion.identity);
    //    Destroy(effect, _sparksEffect.main.startLifetime.constant);
    //}
}
