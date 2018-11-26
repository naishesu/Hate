using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectorGround : MonoBehaviour {

	public delegate void OnAction(Transform cube);
	public static event OnAction OnEnter;
	public Player player;
	public GroundManager groundManager;

	private float dammageRate;
	public float rate;

	public int pv = 50 ;
	public int maxPv = 50;

	void Awake ()
	{
		player = GetComponent<Player>();
		groundManager = FindObjectOfType<GroundManager>();
	}

	private void Update()
	{
		Death();
	}

	void OnTriggerEnter(Collider coll)
	{
		if (player.hasMoved && groundManager.Spawners.Contains(coll.transform.parent))
		{

			// groundManager.groundManagerList.Remove(spawnCube);
			// Debug.Log("j'ai retiré le parent du cube sur lequel le joueur est");

			// groundManager.cubeSpawnList.Remove(coll.gameObject);
			// Debug.Log ("j'ai retiré le cube sur lequel le joueur est de la liste");
			OnEnter(coll.transform.parent);
			Debug.Log(coll.transform.parent.name);
			player.hasMoved = false;
			Debug.Log("has moved false");
		}
		
		switch (coll.transform.gameObject.tag)
		{
			case "KillerCube":
			pv -= 5;
			Debug.Log("PV -1 Enter");			
			break;

			case "HealerCube":
			pv += 3;
			Debug.Log("PV +1 Enter");
			if (pv >= maxPv)
			{
				pv = maxPv;
			}
			break;
		}
	}

	void OnTriggerStay(Collider coll)
	{
		dammageRate -= Time.deltaTime;

		if (dammageRate <= 0)
		{
			switch (coll.transform.gameObject.tag)
			{
				case "KillerCube":
				pv -= 5;
				Debug.Log("PV -1 Stay");
				dammageRate = rate;
				break;

				case "HealerCube":
				pv += 5;
				Debug.Log("PV +1 Stay");
				dammageRate = rate;
				if (pv >= maxPv)
				{
					pv = maxPv;
				}
				break;
			}
		}	
	}

	// void OnTriggerExit(Collider coll)
	// {
	// 	groundManager.cubeSpawnList.Add(coll.gameObject);
	// 	Debug.Log ("j'ai remis le cube dans la liste");
			
	// 	GameObject spawnCube = coll.transform.parent.gameObject;
	// 	groundManager.groundManagerList.Add(spawnCube);
	// 	Debug.Log("j'ai remis le parent du cube sur lequel le joueur est");
	// }

	
	void Death()
	{
		if (pv <= 0)
		{
			pv = 0;
			Destroy(this.gameObject);
			Debug.Log("You died");
		}
	}
}
