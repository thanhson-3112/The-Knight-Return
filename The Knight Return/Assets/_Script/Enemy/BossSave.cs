using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSave : MonoBehaviour
{
    public static BossSave instance;
    public IntoBossRoomBBA bossBBA;
    public IntoBossRoomMH bossMH;

    public IntoBossRoomFP bossFP;
    public IntoBossRoomTusk bossTusk;

    public IntoBossRoomRM bossRM;
    public IntoBossRoomNM bossNM;

    public IntoBossRoomPM bossPM;
    public IntoBossRoomBoD bossBoD;

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
        if (BossSave.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        BossSave.instance = this;
    }


    public void UpdateBossMH()
    {
        bossSaveMH = bossMH.isBossDefeatedMH;

    }
    public void UpdateBossBBA()
    {
        bossSaveBBA = bossBBA.isBossDefeatedBBA;
    }

    public void UpdateBossFP()
    {
        bossSaveFP = bossFP.isBossDefeatedFP;
    }

    public void UpdateBossTusk()
    {
        bossSaveTusk = bossTusk.isBossDefeatedTusk;
    }

    public void UpdateBossRM()
    {
        bossSaveRM = bossRM.isBossDefeatedRM;
    }

    public void UpdateBossNM()
    {
        bossSaveNM = bossNM.isBossDefeatedNM;
    }

    public void UpdateBossPM()
    {
        bossSavePM = bossPM.isBossDefeatedPM;
    }

    public void UpdateBossBoD()
    {
        bossSaveBoD = bossBoD.isBossDefeatedBoD;
    }

    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.bossSaveMH = obj.bossSaveMH;
        this.bossSaveBBA = obj.bossSaveBBA;

        this.bossSaveFP = obj.bossSaveFP;
        this.bossSaveTusk = obj.bossSaveTusk;

        this.bossSaveRM = obj.bossSaveRM;
        this.bossSaveNM = obj.bossSaveNM;

        this.bossSavePM = obj.bossSavePM;
        this.bossSaveBoD = obj.bossSaveBoD;

    }
}
