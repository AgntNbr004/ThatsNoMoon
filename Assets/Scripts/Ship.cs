using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the driving script for the Ship
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    GameObject headsUpDisplay;

    // Declare constants to be used for calculating rotation and thrust
    const float ThrustForce = 3.0f;
    const float RotateDegreesPerSecond = 86.9f;

    // Declare variables for storing ship information
    Rigidbody2D shipRigidbody;
    Vector2 thrustDirection = new Vector2(1, 0);
    
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start ()
	{
        shipRigidbody = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update ()
	{
        // Get keyboard input from user
        float rotationModifier = Input.GetAxis("Rotate");
        
        // Rotate the ships "forward"
        if (rotationModifier != 0)
        {
            // Calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationModifier < 0)
                rotationAmount *= -1;

            // Rotate the ship accordingly
            transform.Rotate(Vector3.forward, rotationAmount);

            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.Play(AudioClipName.PlayerShot);

            GameObject bullet = Instantiate(prefabBullet);
            Transform bulletLocation = bullet.GetComponent<Transform>();
            bulletLocation.position = gameObject.transform.position;
            bulletLocation.rotation = gameObject.transform.rotation;

            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    /// <summary>
    /// FixedUpdate happens every time the Physics updates
    /// </summary>
    void FixedUpdate()
    {
        // Get keyboard input from user
        float thrustInput = Input.GetAxis("Thrust");

        // Move the ship accordingly
        if (thrustInput > 0)
            shipRigidbody.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
    }

    /// <summary>
    /// Destroy the ship when an Asteroid collides with it
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        headsUpDisplay.GetComponent<HUD>().StopGameTimer();
        AudioManager.Play(AudioClipName.PlayerDeath);
        Destroy(gameObject);
    }
}
