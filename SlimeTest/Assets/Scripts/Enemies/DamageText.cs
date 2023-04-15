using UnityEngine;

[RequireComponent(typeof(IDamageble))]
public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMovementToUp _prefabTextMovementToUpDamage;
    [SerializeField] private Vector3 _textOffset = new Vector3(0.2f, 0.5f);
    [SerializeField] private Canvas _canvas;

    private IDamageble _damageble;

    
    protected void Awake()
    {
        _damageble = GetComponent<IDamageble>();
    }

    protected void OnEnable()
    {
        _damageble.GotHit += OnHitted;
    }

    protected void OnDisable()
    {
        _damageble.GotHit -= OnHitted;
    }

    private void OnHitted(float damage)
    {
        var text = Instantiate(_prefabTextMovementToUpDamage,
            new Vector3(transform.position.x + (Random.Range(-_textOffset.x, _textOffset.x)),
                transform.position.y + _textOffset.y, transform.position.z),
            Quaternion.identity);
        
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize($"{damage}");
    }
}