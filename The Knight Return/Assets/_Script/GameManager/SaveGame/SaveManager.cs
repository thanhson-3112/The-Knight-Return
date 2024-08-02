using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";
    private const string SAVE_4 = "save_4";
    private const string SAVE_5 = "save_5";
    private const string SAVE_6 = "save_6";
    private const string SAVE_7 = "save_7";
    private const string SAVE_8 = "save_8";
    private const string SAVE_9 = "save_9";

    private void Awake()
    {
        if (SaveManager.instance != null) Debug.LogError("Only 1 SaveManager allow");
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    protected virtual string GetSaveName1() { return SaveManager.SAVE_1; }
    protected virtual string GetSaveName2() { return SaveManager.SAVE_2; }
    protected virtual string GetSaveName3() { return SaveManager.SAVE_3; }
    protected virtual string GetSaveName4() { return SaveManager.SAVE_4; }
    protected virtual string GetSaveName5() { return SaveManager.SAVE_5; }
    protected virtual string GetSaveName6() { return SaveManager.SAVE_6; }
    protected virtual string GetSaveName7() { return SaveManager.SAVE_7; }
    protected virtual string GetSaveName8() { return SaveManager.SAVE_8; }
    protected virtual string GetSaveName9() { return SaveManager.SAVE_9; }

    public virtual void LoadSaveGame()
    {
        string jsonString1 = SaveSystem.GetString(this.GetSaveName1());
        string jsonString2 = SaveSystem.GetString(this.GetSaveName2());
        string jsonString3 = SaveSystem.GetString(this.GetSaveName3());
        string jsonString4 = SaveSystem.GetString(this.GetSaveName4());
        string jsonString5 = SaveSystem.GetString(this.GetSaveName5());
        string jsonString6 = SaveSystem.GetString(this.GetSaveName6());
        string jsonString7 = SaveSystem.GetString(this.GetSaveName7());
        string jsonString8 = SaveSystem.GetString(this.GetSaveName8());
        string jsonString9 = SaveSystem.GetString(this.GetSaveName9());

        PlayerGold.instance.FromJson(jsonString1);
        SoulManager.instance.FromJson(jsonString2);
        PlayerLife.instance.FromJson(jsonString3);
        PlayerMovement.instance.FromJson(jsonString4);
        PlayerShooting.instance.FromJson(jsonString5);
        MapManager.instance.FromJson(jsonString6);
        MiniMapManager.instance.FromJson(jsonString7);
        BossSave.instance.FromJson(jsonString8);
        ShopSave.instance.FromJson(jsonString9);


        Debug.Log("loadSaveGame " + jsonString1);
        Debug.Log("loadSaveGame " + jsonString2);
        Debug.Log("loadSaveGame " + jsonString3);
        Debug.Log("loadSaveGame " + jsonString4);
        Debug.Log("loadSaveGame " + jsonString5);
        Debug.Log("loadSaveGame " + jsonString6);
        Debug.Log("loadSaveGame " + jsonString7);
        Debug.Log("loadSaveGame " + jsonString8);
        Debug.Log("loadSaveGame " + jsonString9);


    }

    public virtual void SaveGame()
    {
        Debug.Log("SaveGame");
        string jsonString1 = JsonUtility.ToJson(PlayerGold.instance);
        string jsonString2 = JsonUtility.ToJson(SoulManager.instance);
        string jsonString3 = JsonUtility.ToJson(PlayerLife.instance);
        string jsonString4 = JsonUtility.ToJson(PlayerMovement.instance);
        string jsonString5 = JsonUtility.ToJson(PlayerShooting.instance);
        string jsonString6 = JsonUtility.ToJson(MapManager.instance);
        string jsonString7 = JsonUtility.ToJson(MiniMapManager.instance);
        string jsonString8 = JsonUtility.ToJson(BossSave.instance);
        string jsonString9 = JsonUtility.ToJson(ShopSave.instance);

        SaveSystem.SetString(this.GetSaveName1(), jsonString1);// gold
        SaveSystem.SetString(this.GetSaveName2(), jsonString2);// soul, current soul
        SaveSystem.SetString(this.GetSaveName3(), jsonString3);// maxHealth, health, respawnPoint
        SaveSystem.SetString(this.GetSaveName4(), jsonString4);// lockDash, lockDoubleJump, lockSlideWall
        SaveSystem.SetString(this.GetSaveName5(), jsonString5);// lockFireBall
        SaveSystem.SetString(this.GetSaveName6(), jsonString6);// Map Manager
        SaveSystem.SetString(this.GetSaveName7(), jsonString7);// MiniMap Manager
        SaveSystem.SetString(this.GetSaveName8(), jsonString8);// boss save
        SaveSystem.SetString(this.GetSaveName9(), jsonString9);// Shop Manager
    }


    public virtual void NewSaveGame()
    {
        Debug.Log("New Save Game");

        PlayerGold.instance.goldTotal = 0;
        SoulManager.instance.maxSoul= 6;
        SoulManager.instance.currentSoul = 0;

        PlayerLife.instance.maxHealth = 4;
        PlayerLife.instance.health = 4;
        PlayerLife.instance.respawnPoint = new Vector2(-283.04f, 114.50f);

        PlayerMovement.instance.lockDash = true;
        PlayerMovement.instance.lockDoubleJump = true;
        PlayerMovement.instance.lockSlideWall = true;

        PlayerShooting.instance.lockFireBall = true;

        MapManager.instance.map1Active = true;
        MapManager.instance.map2Active = false;
        MapManager.instance.map3Active = false;
        MapManager.instance.map4Active = false;
        MapManager.instance.map5Active = false;

        MiniMapManager.instance.lockMiniMap2 = true;
        MiniMapManager.instance.lockMiniMap3 = true;
        MiniMapManager.instance.lockMiniMap4 = true;
        MiniMapManager.instance.lockMiniMap5 = true;

        BossSave.instance.bossSaveBBA = false;
        BossSave.instance.bossSaveMH = false;
        BossSave.instance.bossSaveFP = false;
        BossSave.instance.bossSaveTusk = false;
        BossSave.instance.bossSaveRM = false;
        BossSave.instance.bossSaveNM = false;
        BossSave.instance.bossSavePM = false;
        BossSave.instance.bossSaveBoD = false;

        ShopSave.instance.shop1Active = true;
        ShopSave.instance.shop2Active = true;
        ShopSave.instance.shop3Active = true;
        ShopSave.instance.shop4Active = true;
        ShopSave.instance.shop5Active = true;
        ShopSave.instance.shop6Active = true;

        string jsonString1 = JsonUtility.ToJson(PlayerGold.instance);
        string jsonString2 = JsonUtility.ToJson(SoulManager.instance);
        string jsonString3 = JsonUtility.ToJson(PlayerLife.instance);
        string jsonString4 = JsonUtility.ToJson(PlayerMovement.instance);
        string jsonString5 = JsonUtility.ToJson(PlayerShooting.instance);
        string jsonString6 = JsonUtility.ToJson(MapManager.instance);
        string jsonString7 = JsonUtility.ToJson(MiniMapManager.instance);
        string jsonString8 = JsonUtility.ToJson(BossSave.instance);
        string jsonString9 = JsonUtility.ToJson(ShopSave.instance);

        SaveSystem.SetString(this.GetSaveName1(), jsonString1);// gold
        SaveSystem.SetString(this.GetSaveName2(), jsonString2);// soul, current soul
        SaveSystem.SetString(this.GetSaveName3(), jsonString3);// maxHealth, health, respawnPoint
        SaveSystem.SetString(this.GetSaveName4(), jsonString4);// lockDash, lockDoubleJump, lockSlideWall
        SaveSystem.SetString(this.GetSaveName5(), jsonString5);// lockFireBall
        SaveSystem.SetString(this.GetSaveName6(), jsonString6);// Map Manager
        SaveSystem.SetString(this.GetSaveName7(), jsonString7);// MiniMap Manager
        SaveSystem.SetString(this.GetSaveName8(), jsonString8);// boss save
        SaveSystem.SetString(this.GetSaveName9(), jsonString9);// Shop Manager
    }
}
