using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GunType gunType;
    public int ammoCount;
    public int maxAmmo = 100;
    public int ReloadAmount = 20;

    [Header("Adjust the rate of fire")]
    [RangeAttribute(1, 10)]
    public float fireRate; 
    
    public bool allowButtonHold;
    public bool canShoot = true;
    public Entity.EntityMask entityMask;

    public GameObject bulletImpact;
    public Camera playerCam;
    private float nextShotTime;

    float minValue;
    float maxValue;

    [SerializeField] private TrailRenderer bulletTracer;
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Animator gunAnimator;

    private RaycastHit target;
    private float timer;

    public enum GunType
    {
        Pistol,
        Shotgun,
        Rifle
    }

    void Start()
    {

        ammoCount = maxAmmo;
        InputManager.Instance.onShootPressed += OnShootPressed;
        InputManager.Instance.onReloadActionPressed += OnReloadPressed;

        nextShotTime = 1f / fireRate; // 1 shot per second for 60 RPM
    }

    private void OnShootPressed()
    {
        if ( canShoot && ammoCount > 0 && timer >= nextShotTime )
        {
            Shoot();
        }
    }

    private void OnReloadPressed()
    {
        if ( canShoot )
        {
            StartCoroutine(Reload());
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ( allowButtonHold && InputManager.Instance.shootAction.IsPressed() )
        {
            if ( timer >= nextShotTime )
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if ( ammoCount > 0 )
        {
            gunAnimator.SetTrigger("Fire");
            Vector3 shootDirection = playerCam.transform.forward;

            if ( Physics.Raycast(playerCam.transform.position, shootDirection, out target, 200f) )
            {
                ammoCount--;

                // Instantiate bullet impact and other effects
                GameObject bulletImpactLocation = Instantiate(bulletImpact, target.point, Quaternion.LookRotation(Vector3.up, target.normal));
                Destroy(bulletImpactLocation, 1f);
                GameObject flash = Instantiate(muzzleFlash, weaponMuzzle);
                Destroy(flash, 0.1f);
                TrailRenderer trail = Instantiate(bulletTracer, weaponMuzzle.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, target));

                if ( target.transform.TryGetComponent<Entity>(out Entity entity) )
                {
                    if ( entityMask.HasFlag(entity.entityType) ) entity.Damage(5f);
                }
            }

            timer = 0;
        }
    }

    IEnumerator Reload()
    {
        gunAnimator.SetBool("Reload", true);
        canShoot = false;
        yield return new WaitForSeconds(1);
        gunAnimator.SetBool("Reload", false);
        canShoot = true;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;
        while ( time < 1f )
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
        }
        Trail.transform.position = hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
    public void AddAmmmo(int ammo)
    {
        if ( ammoCount > ammo)
        {
            ammoCount = maxAmmo;
        }
    }
}