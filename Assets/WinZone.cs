using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public GameObject youWinPrefab;
    public GameObject youLosePrefab;

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay2D");

        if (other.tag == "Player")
        {
            Debug.Log("YOU WIN!");
            other.gameObject.SetActive(false);
            YouWin();
        }
    }

    private void YouWin()
    {
        youWinPrefab.SetActive(true);
    }

    private void YouLose()
    {
        youLosePrefab.SetActive(true);
    }
}
