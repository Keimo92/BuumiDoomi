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

    private void OnEnable()
    {
        GameManager.OnLevelLoaded += PopulateCollectiblesList;
        Collectible.OnCollectiblePickedUp += RemoveCollectible;
    }

    private void OnDisable()
    {
        GameManager.OnLevelLoaded -= PopulateCollectiblesList;
        Collectible.OnCollectiblePickedUp -= RemoveCollectible;
    }

    public void PopulateCollectiblesList()
    {
        collectibles.Clear();
        collectibles.AddRange(FindObjectsOfType<Collectible>());

        Debug.Log($"There is total of :  {collectibles.Count} collectibles remaining ");

        foreach ( Collectible collectible in collectibles )
        {
            Collectible.OnCollectiblePickedUp += RemoveCollectible;
        }
    }

    private void RemoveCollectible(Collectible collectible)
    {
        if ( collectibles.Contains(collectible) )
        {
            collectibles.Remove(collectible);
            Debug.Log($"Collectible removed. Remaining: {collectibles.Count}");
        }
    }

    public int GetCollectiblesLeft()
    {
        return collectibles.Count;
    }
}
