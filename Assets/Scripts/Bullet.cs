using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main driver for propelling the bullet.
/// </summary>
public class Bullet : MonoBehaviour
{
    const float stayAlive = 2f;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start ()
	{
        Timer deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = stayAlive;
        deathTimer.Run();
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update ()
	{
		if (gameObject.GetComponent<Timer>().Finished)
        {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// This tells the bullet which direction it should go
    /// </summary>
    /// <param name="thatWay">The direction being input</param>
    public void ApplyForce(Vector2 thatWay)
    {
        const float magnitude = 5f;
        Rigidbody2D bulletBody = gameObject.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(magnitude * thatWay, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
