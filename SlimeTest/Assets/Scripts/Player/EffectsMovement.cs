using UnityEngine;

public class EffectsMovement : MonoBehaviour
{
    private const float SpawnFlashEffectTime = 0.3f;
    private const float SpawnDustEffectTime = 1f;
    private const float OffsetX = 0.58f;
    private const float OffsetY = 0.14f;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _dustEffectPrefab;
    [SerializeField] private GameObject _windEffectPrefab;

    private readonly Vector3 _positionOffset = new Vector3(0.2f, 1f);

    private float _timerSpawnEffectFlash;
    private float _timerDustEffectFlash;

    
    private void Update()
    {
        CheckSpawnEffectsTimer();
        CheckDustEffectsTimer();
    }

    
    private void CheckSpawnEffectsTimer()
    {
        if (_timerSpawnEffectFlash > 0)
        {
            _timerSpawnEffectFlash -= Time.deltaTime;
        }
        
        if (_timerSpawnEffectFlash < 0)
        {
            _timerSpawnEffectFlash = 0;

            SpawnWindEffects();
        }
    }

    private void CheckDustEffectsTimer()
    {
        if (_timerDustEffectFlash > 0)
        {
            _timerDustEffectFlash -= Time.deltaTime;
        }
        
        if (_timerDustEffectFlash < 0)
        {
            _timerDustEffectFlash = 0;

            SpawnDustEffects();
        }
    }

    private void SpawnDustEffects()
    {
        var dust = Instantiate(_dustEffectPrefab,
            new Vector3(transform.position.x - OffsetX,
            transform.position.y + OffsetY,
            transform.position.z),
            Quaternion.identity);
        dust.transform.SetParent(_canvas.transform, true);
    }

    private void SpawnWindEffects()
    {
        var dust = Instantiate(_windEffectPrefab, new Vector3 (0,0,-_positionOffset.y), Quaternion.identity);
        dust.transform.SetParent(_canvas.transform, false);
    }
    
    public void StartEffects()
    {
        _timerSpawnEffectFlash = SpawnFlashEffectTime;
        _timerDustEffectFlash = SpawnDustEffectTime;
    }
}