using UnityEngine;

public class CollectibleDataPersistence : MonoBehaviour
{
    public static CollectibleDataPersistence instance;
    public static int CollectiblesLeft;

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
    public void UpdateCollectiblesLeft(int collectiblesCount)
    {
        CollectiblesLeft = collectiblesCount;
    }
}
