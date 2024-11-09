#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

public class CreateMaterialsForTextures : Editor
{
    [MenuItem("Tools/Create Materials For Textures")]
    static void CreateMaterials()
    {
        // Attempt to find the shader
        Shader shader = Shader.Find("Shader Graphs/_Main");
        if ( shader == null )
        {
            Debug.LogError("Shader 'Shader Graphs/_Main' not found. Please check the shader name.");
            return;
        }

        try
        {
            AssetDatabase.StartAssetEditing();

            // Get selected textures
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            foreach ( var tex in textures )
            {
                string path = AssetDatabase.GetAssetPath(tex);
                string materialPath = path.Substring(0, path.LastIndexOf(".")) + ".mat";

                // Check if material already exists
                if ( AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) != null )
                {
                    Debug.LogWarning("Material already exists: " + materialPath);
                    continue;
                }

                // Create and configure the material
                Material mat = new Material(shader)
                {
                    mainTexture = tex
                };

                // Save the material asset
                AssetDatabase.CreateAsset(mat, materialPath);
                Debug.Log("Material created: " + materialPath);
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif

