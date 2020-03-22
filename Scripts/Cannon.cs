using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    private bool m_isQuitting;
    private int cannonBalls = 3;
    public Text text;

    private void OnEnable()
    {
        EventBus.StartListening("Shoot", ShootCannon);
        EventBus.StartListening("Reload", ReloadCannon);
    }

    private void OnApplicationQuit()
    {
        m_isQuitting = true;
    }

    private void OnDisable()
    {
        if (m_isQuitting == false)
        {
            EventBus.StopListening("Shoot", ShootCannon);
            EventBus.StopListening("Reload", ReloadCannon);
        }
    }

    void ShootCannon()
    {
        if (cannonBalls > 0) {
            text.text = "Cannon ball fired! " + cannonBalls + " remaining.";
            cannonBalls--;
        }
        else
        {
            text.text = "No cannon balls to fire.";
        }
    }

    void ReloadCannon()
    {
        cannonBalls = 3;
        text.text = "Reloaded cannon.";
    }
}