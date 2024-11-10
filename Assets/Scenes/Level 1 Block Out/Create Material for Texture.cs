// CreateMaterialsForTextures.cs
// C#
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

#if (UNITY_EDITOR)
public class CreateMaterialsForTextures : Editor
{
	[MenuItem("Tools/CreateMaterialsForTextures")]
	static void CreateMaterials ()
	{
		try
		{
			AssetDatabase.StartAssetEditing();
			var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
			foreach(var tex in textures)
			{
				string path = AssetDatabase.GetAssetPath(tex);
				path = path.Substring(0,path.LastIndexOf("."))+".mat";
				if (AssetDatabase.LoadAssetAtPath(path,typeof(Material)) != null)
				{
					Debug.LogWarning("Can't create material, it already exists: " + path);
					continue;
				}
				var mat = new Material(Shader.Find("Shader Graphs/_Main"));
				mat.mainTexture = tex;
				AssetDatabase.CreateAsset(mat,path);
			}
		}
		finally
		{
			AssetDatabase.StopAssetEditing();
			AssetDatabase.SaveAssets();
		}
	}
}
#endif