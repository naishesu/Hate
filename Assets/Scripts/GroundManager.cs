using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

	public GameObject contenerCube, contenerCubeSpawner;
	public Transform[] Spawners; //contient les spawners pour les cubes
	public List <GameObject> cubeManagerList = new List<GameObject>(); //contient les cubes qui seront générés aléatoirement

	// Use this for initialization
	void Awake () 
	{
		int spawnerCount = contenerCubeSpawner.transform.childCount;
		Spawners = new Transform[spawnerCount];

		for (int i = 0; i < spawnerCount; i++)
		{
			Spawners[i] = contenerCubeSpawner.transform.GetChild(i);
		}

		foreach(Transform child in contenerCube.transform)
		{
			cubeManagerList.Add(child.gameObject);
		}
	}

	void Start ()
	{
		MakeGround(null);
		// Debug.Log("il y a " + cubeSpawned.Length + " cubes dans la liste de cubes qui ont été générés au start");
	}
	
	void MakeGround (Transform exception)
	{
		for (int i = 0; i < Spawners.Length; i++)
		{
			if (Spawners[i] != exception)
			{
				if(Spawners[i].childCount > 0)
				{
					Destroy(Spawners[i].GetChild(0).gameObject);
					Debug.Log("not exception");
					{
						
					}
				}
				
				Instantiate (ChooseCube(cubeManagerList), Spawners[i].position, Quaternion.identity, Spawners[i]);
			}
			else 
			{
				Debug.Log("exception");
			}
		}
		
		
		
		// for (int i = 0; i < cubeSpawned.Length; i++)
		// {
		// 	Transform child = cubeSpawned[i].transform.GetChild(0);
		// 	if (child != exception)
		// 	{
		// 		Destroy(cubeSpawned[i]);
		// 		GameObject target = Instantiate (ChooseCube(cubeManagerList), child.transform.position, Quaternion.identity, child.transform);
		// 	} 
		// }
		// // Debug.Log("il y a " + nbrCube + " cubes dans la liste de cubes qui ont été générés");
	}

	// void DestroyGround()
	// {
	// 	for (int i = 0; i < cubeSpawnList.Count; i++)
	// 	{
	// 		Destroy(cubeSpawnList[i]);
	// 		// Debug.Log("je destroy les cubes DESTROYGROUND");
	// 	}
	// }

	public GameObject ChooseCube (List <GameObject> targetList) 
	{
		int nbrCubeList = targetList.Count;							
		int idchooseCube = Random.Range (0, nbrCubeList); 			
		GameObject ChoosenCube = targetList[idchooseCube].gameObject; 
		// Debug.Log ("cube choisi aléatoirement " + ChoosenCube);						
		return ChoosenCube; 										

	}

	public void ResetGround(Transform cube)
	{
		// DestroyGround(cube);
		// Debug.Log("je destroy le ground");
		MakeGround(cube);
		Debug.Log ("je remake le ground");
	}

	void OnEnable ()
	{
		DetectorGround.OnEnter += ResetGround;
	}

	void OnDisable ()
	{
		DetectorGround.OnEnter -= ResetGround;
	}
}
