using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Load scene 2 additively
            SceneManager.LoadScene(2, LoadSceneMode.Additive);

            // Start coroutine to unload scene 1 after scene 2 has been loaded
            StartCoroutine(UnloadCurrentScene());
        }
    }

    private IEnumerator UnloadCurrentScene()
    {
        // Wait for the next frame to ensure scene 2 has been loaded
        yield return null;

        // Unload the current active scene (scene 1)
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);
    }
}