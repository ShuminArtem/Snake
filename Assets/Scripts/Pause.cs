using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject deathPanel;

    public void OnPaused()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void OnPlay()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void OnDeath()
    {
        deathPanel.SetActive(true);
    }
   
}
