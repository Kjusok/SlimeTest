using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 1) Движение текст вверх
/// 2) Уничтожение объекта
/// </summary>
public class PopUpText : MonoBehaviour
{
    [SerializeField] private float _timeForDeath = 0.3f;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Text _text;

    private float _timer;


    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
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
