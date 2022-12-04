using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
	public int Id;
	public string Name;
}

public class SaveLoadTest : MonoBehaviour
{
	private void Start()
	{	
		var SavePath = Application.persistentDataPath + "/save.bytes";

		Debug.Log("path: "+SavePath);

		// iOSでは下記設定を行わないとエラーになる
#if UNITY_IPHONE
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif

		// 保存
		SaveData save = new SaveData();
		save.Id = 1;
		save.Name = "TestData";

		using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, save);
		}

		Debug.Log("[Save]Id:" + save.Id);
		Debug.Log("[Save]Name:" + save.Name);

		// 読み込み
		SaveData load = null;
		using (FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
		{
			BinaryFormatter bf = new BinaryFormatter();
			load = bf.Deserialize(fs) as SaveData;
		}

		Debug.Log("[Load]Id:" + load.Id);
		Debug.Log("[Load]Name:" + load.Name);
	}
}
