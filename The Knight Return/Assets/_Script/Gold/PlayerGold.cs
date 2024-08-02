using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] public int goldTotal;
    public int goldAdd;

    public TextMeshProUGUI goldTotalText;
    public TextMeshProUGUI goldAddText;

    private Coroutine goldAddCoroutine;
    private float timeSinceLastGoldAdded = 0f;

    private void Start()
    {
        goldTotalText.text = "Gold: " + goldTotal.ToString();
        goldAddText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        LootManager.Instance.OnGoldChange += HandleGold;
    }

    private void OnDisable()
    {
        LootManager.Instance.OnGoldChange -= HandleGold;
    }

    public void HandleGold(int newGold)
    {
        goldTotal += newGold;
        goldTotalText.text = "Gold: " + goldTotal.ToString();

        if (goldAddCoroutine != null)
        {
            StopCoroutine(goldAddCoroutine);
        }

        goldAddText.gameObject.SetActive(true);
        goldAdd += newGold;
        goldAddText.text = "+" + goldAdd.ToString();

        goldAddCoroutine = StartCoroutine(HideGoldAddText());
    }

    private IEnumerator HideGoldAddText()
    {
        yield return new WaitForSeconds(3f); 

        while (goldAdd > 0)
        {
            if (timeSinceLastGoldAdded >= 0.2f)
            {
                goldAdd--;
                goldAddText.text = "+" + goldAdd.ToString();
                timeSinceLastGoldAdded = 0f;
            }
            yield return null;
            timeSinceLastGoldAdded += Time.deltaTime;
        }

        goldAddText.gameObject.SetActive(false); 
    }

    public void ClearGold()
    {
        goldTotal = 0;
        goldAdd = 0;
        goldTotalText.text = "Gold: " + goldTotal.ToString();
        goldAddText.gameObject.SetActive(false);
    }

}
