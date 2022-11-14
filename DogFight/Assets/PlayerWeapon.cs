using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform leftFirePoint;
    public Transform rightFirePoint;

    public GameObject projectile;
    public float accuracy;

    public float timeBtwShot;
    float timeBefNextShot;

    void Update()
    {
        timeBefNextShot -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && timeBefNextShot <= 0)
        {
            Fire(leftFirePoint);
            Fire(rightFirePoint);
        }
    }

    void Fire(Transform firepoint)
    {
        timeBefNextShot = timeBtwShot;
        GameObject obj = Instantiate(projectile, firepoint.position, firepoint.rotation);
        obj.transform.Rotate(0, 0, Random.Range(-accuracy, accuracy));
    }
}
