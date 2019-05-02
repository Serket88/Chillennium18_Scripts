using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;

public class EnemyOnTrigger : MonoBehaviour {


    public int health = 30;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlatformBounds")
        {
            //Debug.Log("IN PLATFORM");
            GetComponentInParent<EnemyBehav>().isAttacking = true;
            GetComponentInParent<Rigidbody>().isKinematic = true;
            StartCoroutine(GetComponentInParent<EnemyBehav>().AttackBase());
        }

        //Projectile kill
        Side s = GetComponentInParent<EnemyBehav>().side;
        if ((other.tag == "RedBullet" && s == Side.red) || (other.tag == "BlueBullet" && s == Side.blue))
        {
            //Debug.Log("Detecting Bullet");
            Destroy(other.gameObject);
            if (s == Side.red)
            {
                StaticDatabase.totalKilled++;
               // GameObject.Find("LevelManager").GetComponent<LevelManager>().PlaySound();
                Destroy(transform.parent.gameObject);
            }
            if (s == Side.blue)
            {
                health--;
                if(health <= 0)
                {
                    StaticDatabase.totalKilled++;
                    //GameObject.Find("LevelManager").GetComponent<LevelManager>().PlaySound();
                    Destroy(transform.parent.gameObject);
                }
            }   
        }
	}
}

