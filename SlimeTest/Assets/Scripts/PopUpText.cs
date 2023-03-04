using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    private const float TimeForDeath = 0.3f;

    [SerializeField] private Text _text;
    [SerializeField] private float _speed;

    private float _timerForDeath;


    private void Start()
    {
        _timerForDeath = TimeForDeath;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (_timerForDeath > 0)
        {
            _timerForDeath -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(string text)
    {
        _text.text = text;
    }
}
