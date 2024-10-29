using System.Collections;
using System.Threading;
using UnityEngine;

public class Shooty : MonoBehaviour
{

    public GunType gunType;
    //We'll use these eventually.
    
    public int AmmoCount;

    public int ReloadAmount = 20;

    public KeyCode ShootKey = KeyCode.Mouse0;
    public bool shooting;
    public bool allowButtonHold;
    public float fireRate;
    public float nextShot;
    public bool canShoot = true;
    public Entity.EntityMask entityMask;

    public KeyCode ReloadKey = KeyCode.R;
    public bool reloading;

    public GameObject bulletImpact;
    public Camera playerCam;
    [SerializeField] private TrailRenderer bulletTracer;
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Animator weaponHolderAnimator;
    //For the shooting sounds.
    //public AudioSource weaponSoundSource;
    private RaycastHit target;
    private float timer;

    public enum GunType
    {
        Pistol,
        Shotgun,
        Rifle
    }


    // Start is called before the first frame update
    void Start()
    {
        AmmoCount = 200;
    }

    // Update is called once per frame
    void Update()
    {
        CheckShootInput();
    }

    void CheckShootInput()
    {

        /*

         switch(gunType)
         {
        case Guntype.Pistol
        break;
        case Guntype.Shotgun
        break;
        case Guntype.Rifle
       break;
         }

         */

        //"allowButtonHold" will later be used to create automatic guns.
        if (allowButtonHold) shooting = Input.GetKey(ShootKey);
        else shooting = Input.GetKeyDown(ShootKey);

        if (shooting && canShoot && !allowButtonHold)
        {
            Shoot();
        }

        //Swear I'll make these better when we get to making multiple weapons
        timer += Time.deltaTime;
        nextShot = 1 / fireRate;
        if (allowButtonHold && shooting && timer >= nextShot)
        {
            Shoot();
        }

        if (reloading = Input.GetKey(ReloadKey))
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        //weaponSoundSource.pitch = Random.Range(0.9f, 1.1f);
        //weaponSoundSource.PlayOneShot(weaponSoundSource.clip);

        if ( AmmoCount > 0 )
        {

        //The actual bullet comes straight out of the player's face, Trail itself comes out of the gun.
        gunAnimator.SetTrigger("Fire");
        Vector3 shootDirection = playerCam.transform.forward;
        if (Physics.Raycast(playerCam.transform.position, shootDirection, out target, 200f))
        {
            AmmoCount--;

            //We can use this to make bullet holes and such.
            GameObject bulletImpactLocation = Instantiate(bulletImpact, target.point, Quaternion.LookRotation(Vector3.up, target.normal));
            Destroy(bulletImpactLocation, 1f);
            //Creating Muzzle Flash
            GameObject flash = Instantiate(muzzleFlash, weaponMuzzle);
            Destroy(flash, 0.1f);
            //Creating the tracer trail
            TrailRenderer trail = Instantiate(bulletTracer, weaponMuzzle.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, target));
            //Debug stuff
            Debug.Log("Player shot at: " + target.transform.name);
            Debug.DrawRay(transform.position, shootDirection * target.distance, Color.green, 1f);
            if (target.transform.TryGetComponent<Entity>(out Entity entity))
            {
                if(entityMask.HasFlag(entity.entityType)) entity.Damage(5f);
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
        while (time < 1f)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
        }
        Trail.transform.position = hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }

}
