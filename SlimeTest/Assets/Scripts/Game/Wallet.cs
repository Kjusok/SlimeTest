using System;
using UnityEngine;
using UnityEngine.UI;
/// 1) Добовлять и убирать очки (монетки) из кошелька

public class Wallet : MonoBehaviour
{
    [SerializeField] private Text _walletText;

    private int _coins;

    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            ChangeScore();
        }
    }
    public event Action<int> Changed;

    
    private void ChangeScore()
    {
        _walletText.text = string.Format("{0:D6}", Coins);
        
        Changed?.Invoke(Coins);
    }
}