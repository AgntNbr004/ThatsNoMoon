using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This spawns an Asteroid
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start ()
	{
        foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            GameObject newAsteroid = Instantiate(prefabAsteroid);
            newAsteroid.GetComponent<Asteroid>().Initialize(direction);
        }
	}
}
