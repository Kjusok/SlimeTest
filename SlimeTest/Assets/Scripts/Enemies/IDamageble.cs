using System;

public interface IDamageble
{
    event Action<float> GotHit;
}