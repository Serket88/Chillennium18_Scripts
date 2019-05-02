using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;

namespace CustomClasses
{
	public enum Side {red, blue};

  }

public class EnemySpawner : MonoBehaviour {

	 public List<GameObject> attackMarkers;
	 //List<GameObject> attendedMarkers;
	public float spawnRate;
	public GameObject enemyObj;

    public int MAXPLAYERS;

	public Side side;
	float timer;

	void Awake()
	{
		spawnRate = 10;
		timer = 0;
        MAXPLAYERS = 80000;
	}

	void Update()
	{
		//Debug.Log("RED " + StaticDatabase.RedPopulation);
		//Debug.Log("BLUE " + StaticDatabase.BluePopulation);

		//Spawning Enemys after every {spawnrate}
		if(timer > spawnRate)
		{
            //capping population of enemies
            if ((side == Side.red && StaticDatabase.RedPopulation < MAXPLAYERS) || (side == Side.blue && StaticDatabase.BluePopulation < MAXPLAYERS))
            {
                Instantiate(enemyObj, transform.position, transform.rotation);

                if(side == Side.red)
                {
                    StaticDatabase.RedPopulation++;
                }
                
                if(side == Side.blue){
                    StaticDatabase.BluePopulation++;
                }
            }

			timer = 0;
			
            //Capping speed of spawing
			if(spawnRate >= 2){
				spawnRate = spawnRate - 0.2f;
			}
		}

		timer += Time.deltaTime;
	}
}
