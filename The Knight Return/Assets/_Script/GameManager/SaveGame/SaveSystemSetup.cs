using UnityEngine;
using System.Collections;
using System.IO;

public class SaveSystemSetup : MonoBehaviour
{
    [SerializeField] private string fileName = "Profile.bin";
    [SerializeField] private bool dontDestroyOnLoad;

    void Awake()
    {
        SaveSystem.Initialize(fileName);
        if (dontDestroyOnLoad)
        {
            if (transform.parent != null)
            {
                transform.parent = null;
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveToDisk();
    }
}
