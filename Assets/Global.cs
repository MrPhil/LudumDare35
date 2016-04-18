using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Global
{
    public static Global Instance
    {
        get;
        private set;
    }

    public bool IsPlaying = true;
    public float PlayerMaxSpeed = 3.0f;

    private static Object thisLock = new Object();

    static Global()
    {
        if (Instance == null)
        {
            lock (thisLock)
            {
                if (Instance == null)
                {
                    Instance = new Global();
                }
            }
        }
        else
        {
            Debug.LogError("Global already exists!!");
        }
    }
}
