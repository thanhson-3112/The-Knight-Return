using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceGameCheck : MonoBehaviour
{
    public static ChoiceGameCheck Instance;
    public bool isNewGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
