using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public static MiniMapManager instance;

    public GameObject miniMap2;
    public GameObject miniMap3;
    public GameObject miniMap4;
    public GameObject miniMap5;

    private float holdTimer;
    private bool isMinimapActive = false;

    public bool lockMiniMap2 = true;
    public bool lockMiniMap3 = true;
    public bool lockMiniMap4 = true;
    public bool lockMiniMap5 = true;

    private void Awake()
    {
        if (MiniMapManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        MiniMapManager.instance = this;
    }

    void Start()
    {
        miniMap2.SetActive(false);
        miniMap3.SetActive(false);
        miniMap4.SetActive(false);
        miniMap5.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            holdTimer += Time.deltaTime;

 
            if (holdTimer >= 1f && !isMinimapActive)
            {
                if (!lockMiniMap2)
                {
                    miniMap2.SetActive(true);
                }
                if (!lockMiniMap3)
                {
                    miniMap3.SetActive(true);
                }
                if (!lockMiniMap4)
                {
                    miniMap4.SetActive(true);
                }
                if (!lockMiniMap5)
                {
                    miniMap5.SetActive(true);
                }
                isMinimapActive = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            holdTimer = 0f; 
            miniMap2.SetActive(false);
            miniMap3.SetActive(false);
            miniMap4.SetActive(false);
            miniMap5.SetActive(false);
            isMinimapActive = false; 
        }
    }

    public void UnLockMiniMap2()
    {
        lockMiniMap2 = false;
    }
    public void UnLockMiniMap3()
    {
        lockMiniMap3 = false;
    }
    public void UnLockMiniMap4()
    {
        lockMiniMap4 = false;
    }
    public void UnLockMiniMap5()
    {
        lockMiniMap5 = false;
    }

    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.lockMiniMap2 = obj.lockMiniMap2;
        this.lockMiniMap3 = obj.lockMiniMap3;
        this.lockMiniMap4 = obj.lockMiniMap4;
        this.lockMiniMap5 = obj.lockMiniMap5;

    }

}
