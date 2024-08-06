using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSave : MonoBehaviour
{
    public static BossSave instance;

    [Header("Boss check")]
    public bool bossSaveBBA;
    public bool bossSaveMH;
    public bool bossSaveFP;
    public bool bossSaveTusk;
    public bool bossSaveRM;
    public bool bossSaveNM;
    public bool bossSavePM;
    public bool bossSaveBoD;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void UpdateBossMH(bool defeated)
    {
        bossSaveMH = defeated;
    }

    public void UpdateBossBBA(bool defeated)
    {
        bossSaveBBA = defeated;
    }

    public void UpdateBossFP(bool defeated)
    {
        bossSaveFP = defeated;
    }

    public void UpdateBossTusk(bool defeated)
    {
        bossSaveTusk = defeated;
    }

    public void UpdateBossRM(bool defeated)
    {
        bossSaveRM = defeated;
    }

    public void UpdateBossNM(bool defeated)
    {
        bossSaveNM = defeated;
    }

    public void UpdateBossPM(bool defeated)
    {
        bossSavePM = defeated;
    }

    public void UpdateBossBoD(bool defeated)
    {
        bossSaveBoD = defeated;
    }

    // Save game state
    public void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        bossSaveMH = obj.bossSaveMH;
        bossSaveBBA = obj.bossSaveBBA;
        bossSaveFP = obj.bossSaveFP;
        bossSaveTusk = obj.bossSaveTusk;
        bossSaveRM = obj.bossSaveRM;
        bossSaveNM = obj.bossSaveNM;
        bossSavePM = obj.bossSavePM;
        bossSaveBoD = obj.bossSaveBoD;
    }
}

