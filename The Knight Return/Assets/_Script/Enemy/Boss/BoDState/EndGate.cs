using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGate : MonoBehaviour
{
    public TMP_Text displayText;

    private bool isPlayerInside = false;

    public void Start()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Update()
    {
        displayText = GameObject.FindGameObjectWithTag("UpArrow").GetComponent<TMP_Text>();
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an up arrow");
            SceneManager.LoadScene(8);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            displayText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            displayText.gameObject.SetActive(false);
        }
    }
}
