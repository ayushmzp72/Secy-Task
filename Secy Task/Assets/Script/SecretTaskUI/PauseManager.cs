using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(nextScene);
    }
}
