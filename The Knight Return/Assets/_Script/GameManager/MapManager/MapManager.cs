using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public GameObject map5;

    public bool map1Active;
    public bool map2Active;
    public bool map3Active;
    public bool map4Active;
    public bool map5Active;

    private void Awake()
    {
        if (MapManager.instance != null)
        {
            Debug.LogError("Ch? ???c phép có 1 MapManager");
        }
        MapManager.instance = this;
    }

    void Start()
    {
        UpdateMapActiveStates();
    }

    void Update()
    {
        // Cap nhap trang thai map
        map1Active = map1.activeSelf;
        map2Active = map2.activeSelf;
        map3Active = map3.activeSelf;
        map4Active = map4.activeSelf;
        map5Active = map5.activeSelf;
    }

    void UpdateMapActiveStates()
    {
        // su dung bien bool de xet trang thai active
        map1.SetActive(map1Active);
        map2.SetActive(map2Active);
        map3.SetActive(map3Active);
        map4.SetActive(map4Active);
        map5.SetActive(map5Active);
    }

    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.map1Active = obj.map1Active;
        this.map2Active = obj.map2Active;
        this.map3Active = obj.map3Active;
        this.map4Active = obj.map4Active;
        this.map5Active = obj.map5Active;

        UpdateMapActiveStates(); 
    }

}
