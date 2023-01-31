using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadJsonTest : MonoBehaviour
{
	[Serializable]
	public class SaveData
	{
		public int Id;
		public string Name;
		public int Hp;
	}

	// Start is called before the first frame update
	void Start()
	{
		var SavePath = Application.persistentDataPath + "/TestSave.json";
		Debug.Log("path: " + SavePath);

		// iOSでは下記設定を行わないとエラーになる
#if UNITY_IPHONE
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
		// 保存
		SaveData save = new();
		save.Id = 1;
		save.Name = "TestCharacter";
		save.Hp = 100;

		string saveJson = JsonUtility.ToJson(save);

		Debug.Log("saveJson:" + saveJson);

		using (StreamWriter streamWriter = new(SavePath))
		{
			streamWriter.Write(saveJson);
		}

		Debug.Log("[Save]Id:" + save.Id);
		Debug.Log("[Save]Name:" + save.Name);
		Debug.Log("[Save]Hp:" + save.Name);

		// 読み込み
		SaveData load;

		using (StreamReader streamReader = new(SavePath))
		{
			var loadJson = streamReader.ReadToEnd();
			load = JsonUtility.FromJson<SaveData>(loadJson);
		}

		Debug.Log("[Load]Id:" + load.Id);
		Debug.Log("[Load]Name:" + load.Name);
		Debug.Log("[Load]Hp:" + load.Hp);

	}


}
