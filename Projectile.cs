using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;

public class Projectile : MonoBehaviour 
{

	//When Instantiated always travel striaght
	public Side side;
    public float timer;

   void Awake()
    {
        timer = 0.0f;
        GetComponent<Collider>().isTrigger = true;
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeleteBounds")
        {
            Destroy(this.gameObject);
        }
    }

}
