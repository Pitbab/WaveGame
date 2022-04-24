using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : Interactible
{
    [SerializeField] private GunData gunData;

    [SerializeField] private Transform rightHandHolder;
    [SerializeField] private Transform leftHandHolder;
    [SerializeField] private List<Transform> SpreadPattern = new List<Transform>();


    private int currentMag;
    private int currentReserve;
    private float lastShot;
    
    public bool isReloading { get; private set; }
    public bool isPickedUp;
    private PlayerController owner;

    #region Animation variables

    [SerializeField] private GameObject highlight;
    private const float maxTranslation = 0.5f;
    private const float rotationSpeed = 80f;
    private Vector3 startingPos;

    #endregion
    


    private void Start()
    {
        startingPos = transform.position;
        lastShot = gunData.fireRate;
        currentMag = gunData.magCapacity;
        currentReserve = gunData.maxReserve;
        isPickedUp = false;
    }

    private void Update()
    {
        if (lastShot < gunData.fireRate)
        {
            lastShot += Time.deltaTime;
        }

        if (!isPickedUp)
        {
            transform.position = startingPos + Vector3.up * maxTranslation * Mathf.Cos(Time.time);
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        
    }

    public void Shoot()
    {
        if (!(lastShot >= gunData.fireRate)) return;

        if (currentMag == 0)
        {
            Reload();
            return;
        }
        
        lastShot = 0;
        foreach (var choke in SpreadPattern)
        {
            owner.bulletPool.ShootABullet(choke.position, choke.rotation, gunData);
            currentMag -= 1;
            owner.playerUi.SetGunMag(currentMag, currentReserve);
        }

    }

    public override void Interact(PlayerController actor)
    {

        if (!isPickedUp)
        {
            base.Interact(actor);

            highlight.SetActive(false);
            owner = actor;
            isPickedUp = true;
            owner.playerUi.SetGunMag(currentMag, currentReserve);
            
            transform.position = actor.gunHolderRef.position;
            transform.rotation = actor.transform.rotation;
            transform.parent = actor.gunHolderRef;
            actor.SetCurrentGun(this);

            foreach (var col in gameObject.GetComponents<Collider>())
            {
                col.enabled = false;
            }

        }
        
    }

    public void Drop()
    {
        foreach (var col in gameObject.GetComponents<Collider>())
        {
            col.enabled = true;
        }
        
        owner = null;
        
        highlight.SetActive(true);
        isPickedUp = false;
        transform.parent = null;
        startingPos = transform.position;
    }

    public void Reload()
    {
        Debug.Log("reloading");
        isReloading = true;
        StartCoroutine(ReloadTime());

    }

    private IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(gunData.reloadTime);

        int result = currentReserve - (gunData.magCapacity - currentMag );
        
        if (result >= 0)
        {
            currentReserve -= gunData.magCapacity - currentMag;
            currentMag = gunData.magCapacity;

        }
        else
        {
            currentMag = gunData.magCapacity + result;
            currentReserve = 0;
        }



        //currentMag = gunData.magCapacity;
        owner.playerUi.SetGunMag(currentMag, currentReserve);
        isReloading = false;
    }
    
}
