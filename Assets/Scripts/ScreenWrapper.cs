using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScreenWrapper : MonoBehaviour
{
    /// <summary>
    /// This code is called when the object leaves the viewable area.
    /// </summary>
    void OnBecameInvisible()
    {
        Vector2 position = gameObject.transform.position;
        if (position.x > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
            gameObject.transform.position = position;
        }
        else if (position.x < ScreenUtils.ScreenLeft)
        {
            position.x *= -1;
            gameObject.transform.position = position;
        }

        if (position.y > ScreenUtils.ScreenTop)
        {
            position.y *= -1;
            gameObject.transform.position = position;
        }
        else if (position.y < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
            gameObject.transform.position = position;
        }
    }                                                                                               
}
