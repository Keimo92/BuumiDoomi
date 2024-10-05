using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{

//We'll use these eventually.
//public GunType gunType;
//public int bullets;

public KeyCode ShootKey = KeyCode.Mouse0;
public bool shooting;
public bool allowButtonHold;
public bool canShoot = true;

public KeyCode ReloadKey = KeyCode.R;
public bool reloading;

public GameObject bulletImpact;
public Camera playerCam;
[SerializeField] private TrailRenderer bulletTracer;
[SerializeField] private Transform weaponMuzzle;
[SerializeField] private GameObject muzzleFlash;
[SerializeField] private Animator gunAnimator;
//For the shooting sounds.
//public AudioSource weaponSoundSource;
private RaycastHit rayHit;



    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        CheckShootInput();
    }

    void CheckShootInput() 
    {

        //"allowButtonHold" will later be used to create automatic guns.
        if (allowButtonHold) shooting = Input.GetKey(ShootKey);
		else shooting = Input.GetKeyDown(ShootKey);

        if (shooting && canShoot)
		{
			Shoot();
		}

        if(reloading = Input.GetKey(ReloadKey))
        {
            StartCoroutine(Reload());
        }

    }

private void Shoot()
	{
		//weaponSoundSource.pitch = Random.Range(0.9f, 1.1f);
		//weaponSoundSource.PlayOneShot(weaponSoundSource.clip);

        //The actual bullet comes straight out of the player's face, Trail itself comes out of the gun.
        gunAnimator.SetTrigger("Fire");
		Vector3 shootDirection = playerCam.transform.forward;
			if (Physics.Raycast(playerCam.transform.position, shootDirection, out rayHit, 200f))
			{
                //We can use this to make bullet holes and such.
				GameObject bulletImpactLocation = Instantiate(bulletImpact, rayHit.point, Quaternion.LookRotation(Vector3.up, rayHit.normal));
                Destroy(bulletImpactLocation, 1f);
                //Creating Muzzle Flash
                GameObject flash = Instantiate(muzzleFlash, weaponMuzzle);
                Destroy(flash, 0.1f);
                //Creating the tracer trail
				TrailRenderer trail = Instantiate(bulletTracer, weaponMuzzle.transform.position, Quaternion.identity);
				StartCoroutine(SpawnTrail(trail, rayHit));
                //Debug stuff
				Debug.Log("Player shot at: " + rayHit.transform.name);
                Debug.DrawRay(transform.position, shootDirection * rayHit.distance, Color.green, 1f);
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
