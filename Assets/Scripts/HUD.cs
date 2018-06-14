using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the main driver for the HUD
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    Text textField;

    float timeElapsed = 0f;
    bool timerRunning = true;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start ()
	{
        textField.text = "0";
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update ()
	{
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            textField.text = ((int)timeElapsed).ToString();
        }
	}

    public void StopGameTimer()
    {
        timerRunning = false;
    }
}
