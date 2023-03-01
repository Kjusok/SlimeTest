using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _speed;
    private float _timerForDead;
    private void Start()
    {
        _timerForDead = 0.3f;
        
    }

    public void Initialize (string text)
    {
        _text.text = text;
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y * _speed * Time.deltaTime, transform.position.z);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (_timerForDead > 0)
        {
            _timerForDead -= Time.deltaTime;
        }
        if(_timerForDead <= 0)
        {
            Destroy(gameObject);
        }
    }

}
