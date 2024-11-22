using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GunType gunType;
    public int ammoCount;
    public int maxAmmo = 100;
    public int ReloadAmount = 20;
    public int shootingDistance = 200;

    [Header("Adjust the rate of fire")]
    [RangeAttribute(1, 10)]
    public float fireRate; 
    
    public bool allowButtonHold;
    public bool canShoot = true;
    public Entity.EntityMask entityMask;

    public GameObject bulletImpact;
    public Camera playerCam;
    private float nextShotTime;

    [SerializeField] private TrailRenderer bulletTracer;
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Animator gunAnimator;

    private RaycastHit target;
    private float timer;
    public float mouseButtonHoldTime;
    private const float holdDuration = 1.5f;

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
                OnShootPressed();
            }
        }
        else if ( allowButtonHold && !InputManager.Instance.shootAction.IsPressed() )
        {
            mouseButtonHoldTime = 0f;
        }
    }

    private void Shoot()
    {
        if ( ammoCount > 0 )
        {
            gunAnimator.SetTrigger("Fire");
            Vector3 shootDirection = playerCam.transform.forward;
            if ( Physics.Raycast(playerCam.transform.position, shootDirection, out RaycastHit target, shootingDistance) )
            {
                ammoCount--;

                GameObject bulletImpactLocation = Instantiate(bulletImpact, target.point, Quaternion.LookRotation(Vector3.up, target.normal));
                Destroy(bulletImpactLocation, 1f);

                GameObject flash = Instantiate(muzzleFlash, weaponMuzzle);
                Destroy(flash, 0.1f);

                TrailRenderer trail = Instantiate(bulletTracer, weaponMuzzle.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, target.point));

                if ( target.transform.TryGetComponent<Entity>(out Entity entity) )
                {
                    if ( entityMask.HasFlag(entity.entityType) )
                    {
                        entity.Damage(5f);
                    }
                }
            }
            else
            {
                Vector3 endPosition = playerCam.transform.position + shootDirection * shootingDistance;

                GameObject flash = Instantiate(muzzleFlash, weaponMuzzle);
                Destroy(flash, 0.1f);

                TrailRenderer trail = Instantiate(bulletTracer, weaponMuzzle.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, endPosition));
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

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 endPosition)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while ( time < 1f )
        {
            time += Time.deltaTime / trail.time;
            trail.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
        }

        trail.transform.position = endPosition;
        Destroy(trail.gameObject, trail.time);
    }

    public void AddAmmmo(int ammo)
    {
        if ( ammoCount > ammo)
        {
            ammoCount = maxAmmo;
        }
    }

    private void OnEnable()
    {
        InputManager.Instance.onShootPressed += OnShootPressed;
        InputManager.Instance.onReloadActionPressed += OnReloadPressed;
    }

    private void OnDisable()
    {
        InputManager.Instance.onShootPressed -= OnShootPressed;
        InputManager.Instance.onReloadActionPressed -= OnReloadPressed;

    }
}