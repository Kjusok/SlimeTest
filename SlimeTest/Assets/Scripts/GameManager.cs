using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Slime _slime;

    [SerializeField] private Text _cashText;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;

    private int _cash;
    private bool _isPaused;
    public bool GameIsPaused => _isPaused;
    public bool GameIsStarted
    {
        get; private set;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AwakePanelYouDied()
    {
        _isPaused = true;
        _panelYouDied.SetActive(true);
        _restartButton.SetActive(true);
    }

    public void AwakeFinishPanel()
    {
        _isPaused = true;
        _restartButton.SetActive(true);
        _panelYouWin.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddCoinsToCash(int coins)
    {
        _cash += coins;
        string scoreTextWithZero = string.Format("{0:D6}", _cash);

        _cashText.text = scoreTextWithZero;
       

    }

    private void RemoveCash(int coins)
    {
        
        _cash -= coins;
        string scoreTextWithZero = string.Format("{0:D6}", _cash);

        _cashText.text = scoreTextWithZero;
    }

    public void UpDamageButton()
    {
        if(_cash >= 25)
        {
            _slime.UpDamage();
            RemoveCash(25);
        }
    }

    public void UpSpeedAtackButton()
    {
        if (_cash >= 25)
        {
            _slime.UpSpeedAtack();
            RemoveCash(25);
        }
    }
    public void UpHealthButton()
    {
        if (_cash >= 25)
        {
            RemoveCash(25);
            _slime.UpHealth();
        }
    }

    public void RecoveryHealthButton()
    {
        if (_cash >= 10)
        {
            _slime.RecoveryHP();
            RemoveCash(10);
        }
    }
    public void PressStartButton()
    {
        _slime.StartMovement();
        GameIsStarted = true;
        _startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
