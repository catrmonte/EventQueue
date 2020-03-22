using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    void Update()
    {
        // Press S to shoot
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventBus.AddToQueue("Shoot");
        }

        // Eventually press L to launch
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventBus.AddToQueue("Launch");
        }

        // Eventually press R to reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventBus.AddToQueue("Reload");
        }

        // Eventually press e to reset rocket
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventBus.AddToQueue("Reset");
        }

        // Press F to light firework
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventBus.AddToQueue("Light");
        }
    }
}