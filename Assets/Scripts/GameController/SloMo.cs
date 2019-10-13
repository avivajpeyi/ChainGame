
using System;
using System.Collections;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class SloMo : MonoBehaviour
{
    public float fullSpeed = 1;
    public float slowSpeed = 0.3f;

    public Image SlowMoImage;
    private GameObject SlowMoPanel;
    private Color currentColor;

    public AudioSource gameAudioSource;
    
    public bool m_SlowMo;


    private void Update()
    {

        if (m_SlowMo)
        {
            SlowMoPanel.SetActive(true);
            FlickerSlowMoImage();
            
        }
        else
        {
            SlowMoPanel.SetActive(false);
        }
    }

    void Start()
    {
        currentColor = SlowMoImage.color;
        SlowMoPanel = SlowMoImage.gameObject;
        m_SlowMo = false;
        
    }

    void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void ChangeSpeed()
    {
        // toggle slow motion state
        m_SlowMo = !m_SlowMo;
        
        Time.timeScale = m_SlowMo ? slowSpeed : fullSpeed;
        
        // toggle low pitch audio
        AdjustAudioPitch();
    }

    void AdjustAudioPitch()
    {
        if (m_SlowMo)
            gameAudioSource.pitch = 0.9f;
        else
            gameAudioSource.pitch = 1f;
    }


    public void StartSlowmo()
    {
        if (!m_SlowMo)
        {
            ChangeSpeed();
        }
    }
    
    public void EndSlowmo()
    {
        if (m_SlowMo)
        {
            ChangeSpeed();
        }
    }

    void FlickerSlowMoImage()
    {
        
        SlowMoImage.color = Color.Lerp(
            currentColor,
            new Color(currentColor.r, currentColor.g, currentColor.b, 0.05f), 
            Mathf.PingPong(Time.time, 1)
            );
    }


}