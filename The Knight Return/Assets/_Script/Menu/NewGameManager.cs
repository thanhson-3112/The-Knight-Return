using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance;

    public bool newGame;

    void Start()
    {
        newGame = ChoiceGameCheck.Instance.isNewGame;
        Debug.Log("New Game: " + newGame);

        if (newGame == true)
        {
            SaveManager.instance.NewSaveGame();
        }
    }

}
