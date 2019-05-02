using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;
using UnityEngine.AI;

public class EnemyBehav : MonoBehaviour {
	public Transform focusMarker;

	public List<GameObject> markerList;

	public Side side;

	NavMeshAgent _agent;

	public float attackRecharge;

    public bool isAttacking;

	public float walkingSpeed = 3.0f;

    public int health = 75;



	   void Start()
	   {
		   isAttacking = false;
		   attackRecharge = 3.0f;
		   focusMarker = DetermineFocus();
           
        
	   }

		void Update()
		{

        transform.LookAt(focusMarker);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
		}


		//TODO
		void AnimationController()
		{
			if(!isAttacking)
			{
				//playWalkAnimation
			}
			else{
				//playAttackAnimation
			}
		}

		void OnCollisionEnter(Collision other)
		{
			if((other.gameObject.name == "RedBullet" && side == Side.blue) || (other.gameObject.name == "BlueBullet" && side == Side.red))
			{
                if(side == Side.red)
                {
                    StaticDatabase.RedPopulation--;
                    StaticDatabase.totalKilled++;
                    health--;
                    if(health <= 0)
                    {
                    Debug.Log("redDestroyed");
                    Destroy(this.gameObject);
                     }
                }

                 else if(side == Side.blue)
                {
                    StaticDatabase.BluePopulation--;
                    StaticDatabase.totalKilled++;
                    Destroy(this.gameObject);
            }
            
        }

            //destroy projectile if its hiting own type
            if(((other.gameObject.name == "RedBullet") && side == Side.red) || (other.gameObject.name == "BlueBullet" && side == Side.blue))
            {
                Destroy( other.gameObject);
            }
		}

		void FixedUpdate()
		{

			if(!isAttacking)
			{

			//transform.position = Vector3.MoveTowards(transform.position, focusMarker.position, Time.deltaTime * walkingSpeed);
			//MOVETOWARDS OBJECT
			if(Vector3.Distance(transform.position, focusMarker.position) > 0.2f)
			{
                //transform.LookAt(focusMarker);
                GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 0.5f, ForceMode.Force);
               
            }

            //Keep blue people on cieling
			if(side == Side.red)
			{
			    Vector3 direction = new Vector3(0, 1, 0);
                GetComponent<Rigidbody>().AddForce (10 * direction);
			} 

		  }
		}




	public Transform DetermineFocus()
	{
		markerList = new List<GameObject>();

		//Find which spawn point closest to

		Vector3 tran1 = GameObject.Find("RedNorth_SP").transform.position;
		Vector3 tran2 = GameObject.Find("RedSouth_SP").transform.position;
        Vector3 tran22 = GameObject.Find("RedWest_SP").transform.position;
        Vector3 tran3 = GameObject.Find("BlueSouth_SP").transform.position;
		Vector3 tran4 = GameObject.Find("BlueNorth_SP").transform.position;
        Vector3 tran44 = GameObject.Find("BlueWest_SP").transform.position;


        if (transform.position == tran1)
		{
			markerList.Add(GameObject.Find("rednorth_attackMarker_01"));
			markerList.Add(GameObject.Find("rednorth_attackMarker_02"));
			markerList.Add(GameObject.Find("rednorth_attackMarker_03"));
			markerList.Add(GameObject.Find("rednorth_attackMarker_04"));
			markerList.Add(GameObject.Find("rednorth_attackMarker_05"));
			markerList.Add(GameObject.Find("rednorth_attackMarker_06"));
			side = Side.red;			
		}

		if(transform.position == tran2)
		{
			markerList.Add(GameObject.Find("redsouth_attackMarker_01"));
			markerList.Add(GameObject.Find("redsouth_attackMarker_02"));
			markerList.Add(GameObject.Find("redsouth_attackMarker_03"));
			markerList.Add(GameObject.Find("redsouth_attackMarker_04"));
			markerList.Add(GameObject.Find("redsouth_attackMarker_05"));
			markerList.Add(GameObject.Find("redsouth_attackMarker_06"));
			side = Side.red;
		}

        if(transform.position == tran22)
        {
            markerList.Add(GameObject.Find("redwest_attackMarker_01"));
            markerList.Add(GameObject.Find("redwest_attackMarker_02"));
            markerList.Add(GameObject.Find("redwest_attackMarker_03"));
            markerList.Add(GameObject.Find("redwest_attackMarker_04"));
            markerList.Add(GameObject.Find("redwest_attackMarker_05"));
            markerList.Add(GameObject.Find("redwest_attackMarker_06"));
            side = Side.red;
        }

		if(transform.position == tran3)
		{
			markerList.Add(GameObject.Find("bluesouth_attackMarker_01"));
			markerList.Add(GameObject.Find("bluesouth_attackMarker_02"));
			markerList.Add(GameObject.Find("bluesouth_attackMarker_03"));
			markerList.Add(GameObject.Find("bluesouth_attackMarker_04"));
			markerList.Add(GameObject.Find("bluesouth_attackMarker_05"));
			markerList.Add(GameObject.Find("bluesouth_attackMarker_06"));
			side = Side.blue;
		}

		if(transform.position == tran4)
		{
			markerList.Add(GameObject.Find("bluenorth_attackMarker_01"));
			markerList.Add(GameObject.Find("bluenorth_attackMarker_02"));
			markerList.Add(GameObject.Find("bluenorth_attackMarker_03"));
			markerList.Add(GameObject.Find("bluenorth_attackMarker_04"));
			markerList.Add(GameObject.Find("bluenorth_attackMarker_05"));
			markerList.Add(GameObject.Find("bluenorth_attackMarker_06"));
			side = Side.blue;
		}

        if (transform.position == tran44)
        {
            markerList.Add(GameObject.Find("bluewest_attackMarker_01"));
            markerList.Add(GameObject.Find("bluewest_attackMarker_02"));
            markerList.Add(GameObject.Find("bluewest_attackMarker_03"));
            markerList.Add(GameObject.Find("bluewest_attackMarker_04"));
            markerList.Add(GameObject.Find("bluewest_attackMarker_05"));
            markerList.Add(GameObject.Find("bluewest_attackMarker_06"));
            side = Side.blue;
        }

        int index = Random.Range(0, markerList.Count);
		return markerList[index].transform;
	}

		public IEnumerator AttackBase()
		{
			while(true)
			{
				if(side == Side.red)
				{
					StaticDatabase.RedPlatform -= 1;
                    //Debug.Log("REDHEALTH " + StaticDatabase.RedPlatform);

				}
				else{
					StaticDatabase.BluePlatform -= 1;
                    //Debug.Log("BLUEHEALTH " + StaticDatabase.BluePlatform);
				}

				yield return new WaitForSeconds(attackRecharge);

			}
		}
	}
