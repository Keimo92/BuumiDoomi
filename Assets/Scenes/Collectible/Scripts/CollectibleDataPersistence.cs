using System.Collections.Generic;
using UnityEngine;

public class CollectibleDataPersistence : MonoBehaviour
{
    public static CollectibleDataPersistence instance;

    private List<Collectible> collectibles = new List<Collectible>();

    private void Awake()
    {
        if ( instance != null && instance != this )
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Called to update each collectible in the scene.
    public void UpdateCollectiblesList()
    {
        collectibles.Clear(); // Clear previous collectibles from previous scene
        collectibles.AddRange(FindObjectsOfType<Collectible>());
        Debug.Log($"Collectibles found: {collectibles.Count}");
    }

    // Returns the number of remaining collectibles
    public int GetCollectiblesLeft()
    {
        return collectibles.FindAll(c => c != null && c.gameObject.activeInHierarchy).Count;
    }
    private void OnEnable()
    {
        GameManager.OnLevelLoaded += UpdateCollectiblesList;
    }

    private void OnDisable()
    {
        GameManager.OnLevelLoaded -= UpdateCollectiblesList;
    }
}
