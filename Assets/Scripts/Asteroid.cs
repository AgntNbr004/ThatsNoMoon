using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the code for the Asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    // Add the sprites for random generation
    [SerializeField]
    Sprite[] asteroids;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
	{
        SpriteRenderer asteroidSprite = GetComponent<SpriteRenderer>();
        asteroidSprite.sprite = asteroids[Random.Range(0, asteroids.Length)];
	}

    /// <summary>
    /// 
    /// </summary>
    public void Initialize(Direction thatWay)
    {
        Vector3 startLocation = new Vector3();
        float angle = 0.0f;

        switch (thatWay)
        {
            case Direction.Up:
                startLocation.Set(
                    Random.Range(ScreenUtils.ScreenLeft, ScreenUtils.ScreenRight),
                    ScreenUtils.ScreenBottom,
                    startLocation.z);
                angle = Random.Range(75, 105);
                break;
            case Direction.Left:
                startLocation.Set(
                    ScreenUtils.ScreenRight,
                    Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop),
                    startLocation.z);
                angle = Random.Range(165, 195);
                break;
            case Direction.Down:
                startLocation.Set(
                    Random.Range(ScreenUtils.ScreenLeft, ScreenUtils.ScreenRight),
                    ScreenUtils.ScreenTop,
                    startLocation.z);
                angle = Random.Range(255, 285);
                break;
            default:
                startLocation.Set(
                    ScreenUtils.ScreenLeft,
                    Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop),
                    startLocation.z);
                angle = Random.Range(345, 375);
                if (angle > 360)
                    angle -= 360;
                break;
        }
        angle *= Mathf.Deg2Rad;

        Transform asteroidLocation = GetComponent<Transform>();
        asteroidLocation.position = startLocation;

        StartMoving(angle);
    }

    /// <summary>
    /// This actually gets an object moving
    /// </summary>
    public void StartMoving(float angle)
    {
        Rigidbody2D asteroidBody = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        asteroidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            AudioManager.Play(AudioClipName.AsteroidHit);

            Vector3 size = gameObject.GetComponent<Transform>().localScale;
            size.x /= 2f;
            size.y /= 2f;
            gameObject.transform.localScale = size;

            if (gameObject.GetComponent<Transform>().localScale.x >= 0.25)
            {
                CircleCollider2D hit = gameObject.GetComponent<CircleCollider2D>();
                hit.radius /= 2;

                for (int i = 0; i < 2; i++)
                {
                    GameObject lilGuy = Instantiate(gameObject);
                    lilGuy.GetComponent<Asteroid>().StartMoving(Random.Range(0f, 2 * Mathf.PI));
                }
            }

            Destroy(gameObject);
        }
    }
}
