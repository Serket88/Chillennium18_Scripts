using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{


    public GameObject projectile;
    public GameObject modelSpawnref;

    public float LightingTimer;
    public float WaterTimer;
    public float currentTime;
    public float currentTimeWater;


    private void Awake()
    {
        LightingTimer = 0.2f;
        WaterTimer = 0.02f;
        currentTime = 0;
        currentTimeWater = 0;
    }

    void Update()
    {

        currentTime += Time.deltaTime;
        currentTimeWater += Time.deltaTime;

        if (GetComponentInParent<SteamVR_TrackedController>().triggerPressed)
        {
            if (projectile.name == "RedBullet")
            {
                if (currentTime > LightingTimer)
                {
                    ShootProjectile(projectile.name);
                    currentTime = 0;
                }
            }
            else
            {
                if (currentTimeWater > WaterTimer)
                {
                    ShootProjectile(projectile.name);
                    currentTimeWater = 0;
                }
            }
        }
    }

    void ShootProjectile(string name)
    {
        //WaterSpray
        if (name == "BlueBullet")
        {
            GameObject bullet = Instantiate(projectile, modelSpawnref.transform.position, modelSpawnref.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20.0f, ForceMode.VelocityChange);
            Destroy(bullet, 2.0f);
        }

        //Lighting Shotgun
        if (name == "RedBullet")
        {
            GameObject Lbullet_01 = Instantiate(projectile, modelSpawnref.transform.position, modelSpawnref.transform.rotation) as GameObject;
            //spray bullets
            Lbullet_01.GetComponent<Rigidbody>().AddForce(transform.forward * 20.0f, ForceMode.VelocityChange);
       
            Destroy(Lbullet_01, 2f);
      
        }
    }
}


