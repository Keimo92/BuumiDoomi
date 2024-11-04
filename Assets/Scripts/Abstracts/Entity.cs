using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public abstract class Entity : MonoBehaviour
{
    [Header("Basic Setup")]
    [SerializeField] float currentHealth; //Will be overridden by maxHealth on Start()
    [SerializeField] float maxHealth;
    [SerializeField] Renderer entityGfx;
    public EntityMask entityType; //Holds the entity type so we can filter entities based on the type. DON'T SET MULTIPLE TYPES OTHERWISE THIS WONT WORK CORRECTLY


    [Header("On Death Instantiated Prefabs")]
    [SerializeField] List<GameObject> onDeathPrefabs = new List<GameObject>();

    [Header("On Hit Material")]
    [SerializeField] Material onHitMaterial;
    [SerializeField] private float onHitMaterialTime;
    protected bool OnHitMaterialEnabled = false;
    [System.Flags]
    public enum EntityMask
    {
        None    = 0,
        Enemy   = 1,
        Player  = 2,
        Object  = 4
    }

    private void Awake()
    {
        OnHitMaterialEnabled = false;
        currentHealth = maxHealth;
    }
    private void Start()
    {
        //Set current health
        currentHealth = maxHealth;

        //If mesh renderer is not set. Then try to get it from current gameobject
        if(entityGfx == null)
        {
            entityGfx = GetComponentInChildren<MeshRenderer>();
        }
    }

    //Damage function. Called by other scripts when they want to deal damage to entity.
    public virtual void Damage(float damage)
    {
        //Add the onhitmaterial to the object if we have access to mesh renderer and onHitMaterial
        if(onHitMaterial && entityGfx && !OnHitMaterialEnabled) StartCoroutine(HitMaterialEnable());
        
        //If current health reaches 0 or below we kill this entity
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
    }

    //Kill function. Can be also called by other scripts if we want to kill this entity.
    public virtual void Kill()
    {
        //Instantiate OnDeath prefabs
        foreach(GameObject prefab in onDeathPrefabs)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    //Health getter function
    public float GetHealth() { return currentHealth; }
    public void AddHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    IEnumerator HitMaterialEnable()
    {
        //Add onhit material to the object
        OnHitMaterialEnabled = true;
        List<Material> materials = entityGfx.materials.ToList();
        materials.Add(onHitMaterial);
        entityGfx.materials = materials.ToArray();
        yield return new WaitForSeconds(onHitMaterialTime); //Wait for onHitMaterialTime and remove the material
        materials.Remove(onHitMaterial);
        OnHitMaterialEnabled = false;
        entityGfx.materials = materials.ToArray();
    }
}
