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

    public GameObject miniMapOfMap2;
    public GameObject miniMapOfMap3;
    public GameObject miniMapOfMap4;
    public GameObject miniMapOfMap5;

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

        /*if(MapManager.instance.map2Active == true)
        {
            miniMapOfMap2.SetActive(true);
        }
        else
        {
            miniMapOfMap2.SetActive(false);
        }

        if (MapManager.instance.map3Active == true)
        {
            miniMapOfMap3.SetActive(true);
        }
        else
        {
            miniMapOfMap3.SetActive(false);
        }

        if (MapManager.instance.map4Active == true)
        {
            miniMapOfMap4.SetActive(true);
        }
        else
        {
            miniMapOfMap4.SetActive(false);
        }

        if (MapManager.instance.map5Active == true)
        {
            miniMapOfMap5.SetActive(true);
        }
        else
        {
            miniMapOfMap5.SetActive(false);
        }*/

        GameObject[] miniMaps = { miniMapOfMap2, miniMapOfMap3, miniMapOfMap4, miniMapOfMap5 };
        bool[] mapActiveStates = { MapManager.instance.map2Active, MapManager.instance.map3Active, 
            MapManager.instance.map4Active, MapManager.instance.map5Active };

        for (int i = 0; i < miniMaps.Length; i++)
        {
            miniMaps[i].SetActive(mapActiveStates[i]);
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
