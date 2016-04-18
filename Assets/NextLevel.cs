using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int NextLevelIndex = 0;
    public bool IsQuit = false;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (IsQuit || SceneManager.sceneCountInBuildSettings < NextLevelIndex)
            {

                SceneManager.LoadScene("Level999-Goodbye", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(NextLevelIndex, LoadSceneMode.Single);
            }
        }
    }
}
