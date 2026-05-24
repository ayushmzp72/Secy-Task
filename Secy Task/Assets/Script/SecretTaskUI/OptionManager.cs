using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMainMenu()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex - 2;
        SceneManager.LoadScene(nextScene);
    }

    
}
