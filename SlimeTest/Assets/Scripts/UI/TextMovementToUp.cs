using UnityEngine;
using UnityEngine.UI;

public class TextMovementToUp : MonoBehaviour
{
    [SerializeField] private float _timeForDeath = 0.3f;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Text _text;

    private float _timer;
    

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(string text)
    {
        _timer = _timeForDeath;
        _text.text = text;
    }
}
