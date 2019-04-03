using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public Button restartButton;
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);

    }

    void RestartGame()
    {
        Debug.Log("restarted!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }



}