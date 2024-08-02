using UnityEngine;
using System.Collections;
using System.IO;

public static class SaveSystem {
	
	private static string file;
	private static bool loaded;
	private static DataState data;

	public static void Initialize(string fileName) // load du lieu tu file
    {
		if(!loaded)
		{
			file = fileName;
			if(File.Exists(GetPath())) Load(); else data = new DataState();
			loaded = true;
		}
	}

    static string GetPath()
	{
		return Application.persistentDataPath + "/" + file;
	}

	static void Load()
	{
		data = SerializatorBinary.LoadBinary(GetPath());
		Debug.Log("[SaveGame] --> Loading the save file: " + GetPath());
	}

	public static void ReplaceItem(string name, string item)
	{
		bool j = false;
		for(int i = 0; i < data.items.Count; i++)
		{
			if(string.Compare(name, data.items[i].Key) == 0)
			{
				data.items[i].Value = Crypt(item);
				j = true;
				break;
			}
		}

		if(!j) data.AddItem(new SaveData(name, Crypt(item)));
	}


	public static void SaveToDisk() // ghi du lieu vao file
    {
		if(data.items.Count == 0) return;
		SerializatorBinary.SaveBinary(data, GetPath());
		Debug.Log("[SaveGame] --> Save game data: " + GetPath());
	}

    public static void SetString(string name, string val)
    {
        if (string.IsNullOrEmpty(name)) return;
        ReplaceItem(name, val);
    }

    public static string GetString(string name)
	{
		if(string.IsNullOrEmpty(name)) return string.Empty;
		return iString(name, string.Empty);
	}

	static string iString(string name, string defaultValue)
	{
		for(int i = 0; i < data.items.Count; i++)
		{
			if(string.Compare(name, data.items[i].Key) == 0)
			{
				return Crypt(data.items[i].Value);
			}
		}

		return defaultValue;
	}


	static string Crypt(string text)
	{
		string result = string.Empty;
		foreach(char j in text) result += (char)((int)j ^ 42);
		return result;
	}
}
