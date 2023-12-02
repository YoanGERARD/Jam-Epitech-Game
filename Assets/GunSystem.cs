using System.Globalization;
using System.Numerics;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class GunSystem : MonoBehaviour
{
    public int damage, magazineSize, bulletsPerTap;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;
    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;   
    }
    public void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput () 
    {

        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    private void Shoot()
    {
        readyToShoot = false;

        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        UnityEngine.Vector3 direction = fpsCam.transform.forward + new UnityEngine.Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy)) {
            UnityEngine.Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Player"))
                UnityEngine.Debug.Log("touché");
                // rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
        }

        Instantiate(bulletHoleGraphic, rayHit.point, UnityEngine.Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, UnityEngine.Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
}