using UnityEngine;
using UnityEngine.SceneManagement;
/// 1) Запустить нужную сцену

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}