using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitAndThenQuit : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(WaitThenQuit());
    }

    private IEnumerator WaitThenQuit()
    {
        yield return new WaitForSeconds(3.0f);

        Global.Instance.IsPlaying = false;
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
