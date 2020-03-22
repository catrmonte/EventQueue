using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firework : MonoBehaviour
{
    private bool m_isQuitting;
    public Text text;

    private void OnEnable()
    {
        EventBus.StartListening("Light", LightFirework);
    }

    private void OnApplicationQuit()
    {
        m_isQuitting = true;
    }

    private void OnDisable()
    {
        if (m_isQuitting == false)
        {
            EventBus.StopListening("Light", LightFirework);
        }
    }

    void LightFirework()
    {
        text.text = "Lit firework!";
    }
}