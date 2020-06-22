using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData = null;
    public Vector2 firePoint = Vector2.zero;
    Transform projectileTransform;

    public float fireRate = 0;
    float timeToFire = 0;

    SpriteRenderer sr;

    void Start()
    {   if (weaponData == null) Debug.LogError("No weaponData");
        fireRate = weaponData.fireRate;
        projectileTransform = GameObject.Find("Projectiles").transform;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weaponData.usedSprite;
    }


    void Update()
    {
        //Shoot
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1")) Shoot1();
            else if (Input.GetButtonDown("Fire2")) Shoot2();
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot1();
            }
            else if (Input.GetButton("Fire2") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot2();
            }
        }
    }

    void Shoot1()
    {
        Debug.Log("Shoot1");
        Instantiate(weaponData.projectilePrefab, transform.position+(Vector3)firePoint, transform.rotation, projectileTransform);
    }

    void Shoot2()
    {
        Debug.Log("Shoot2");
    }


}
