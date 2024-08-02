using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public static PlayerGold instance;

    [SerializeField] public int goldTotal;
    public int goldAdd;

    public TextMeshProUGUI goldTotalText;
    public TextMeshProUGUI goldAddText;

    private Coroutine goldAddCoroutine;
    private float timeSinceLastGoldAdded = 0f;

    private void Awake()
    {
        if (PlayerGold.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerGold.instance = this;
    }

    private void Start()
    {
        goldAddText.gameObject.SetActive(false);
    }

    private void Update()
    {
        goldTotalText.text = ": " + goldTotal.ToString();
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

    // tru vang khi mua do
    public void GoldMinus(int val)
    {
        goldTotal -= val;
    }

    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.goldTotal = obj.goldTotal;
    }

}
