using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static readonly List<String> collectedFruits = new List<String>();
    private float health;
    public float startHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        health = startHealth;


    }
    public void PlayerTakesDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0f)
        {
            Death();
        }
    }
    void Death()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
