using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;
using System;

public class Shredder : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 7f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        FindObjectOfType<Pause>().OnDeath();
        StartCoroutine(WaitAndLoad());
        FindObjectOfType<Snake>().DeathSound();
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }
}
