using UnityEngine;
/// <summary>
/// 1) Уничтожение по таймеру объекта
/// </summary>
public class DestroyByTime : MonoBehaviour
{
   [SerializeField] private float _duration;

   private float _timer;


   private void Awake()
   {
       _timer = _duration;
   }

   private void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
