using UnityEngine;
using System.Collections;
using CnControls;

public class BasicWeaponShooting : MonoBehaviour {

	public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    
    bool isShooting = false;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    
    private PlayerMovement playerMovement;
    private Vector3 aimMovement;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        playerMovement = this.transform.parent.GetComponent<PlayerMovement>();
    }
    
    void Update ()
    {
        timer += Time.deltaTime;
        
        aimMovement = new Vector3(CnInputManager.GetAxis("HorizontalAim"), 0.0f, CnInputManager.GetAxis("VerticalAim"));

        if( aimMovement.x != 0 || aimMovement.z != 0 )
        {
            isShooting = true;
         
            playerMovement.RotationUpdate(aimMovement);
            
            if(timer >= timeBetweenBullets && Time.timeScale != 0)
                Shoot ();
        }
        
        else
            isShooting = false;

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            //if(enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            //}
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}