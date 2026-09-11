using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class PlayerNeeds : MonoBehaviour,IDamagable

{
    public Image FadeScreen4;
    
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float hungerHealthdecay;
    public float thirstHealthdecay;

    public UnityEvent onTakeDamage;

    public static PlayerNeeds instance;

    //singleton
    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        //initial values ​​at the beginning of the game  (currentValue = startValue)
        health.currentValue = health.startValue;
        hunger.currentValue = hunger.startValue;
        thirst.currentValue = thirst.startValue;
        sleep.currentValue = sleep.startValue;

    }

    // Update is called once per frame
    void Update()
    {

        //Change in bars over time
        hunger.Subtrack(hunger.decayRate*Time.deltaTime);
        thirst.Subtrack(thirst.decayRate*Time.deltaTime);
        sleep.Subtrack(sleep.regenrate*Time.deltaTime);

        //Health bar decreases when hungry
        if (hunger.currentValue == 0.0f)
        {
            health.Subtrack(hungerHealthdecay * Time.deltaTime);
        }

        //when thirsty the life bar decreases
        if (thirst.currentValue == 0.0f)
        {
            health.Subtrack(thirstHealthdecay * Time.deltaTime);
        }

        //character death
        if (health.currentValue == 0.0f)
        {
            Die();
        }

        //Updating UI bars The % value received with GetPercentage is in the UI section.
        //Will allow bar images to change horizontally.
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Drink(float amount)
    {
        thirst.Add(amount);
    }
    public void Sleep(float amount)
    {
        sleep.Subtrack(amount);
    }
    public void TakePhysicDamage(int amount)
    {
        health.Subtrack(amount);
        onTakeDamage?.Invoke();  //? --> if onTakeDamage event occurs
                                 //.invoke executes the method
    }

    public void Die()
    {
        FadeScreen4.GetComponent<Animation>().Play("you_died");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
        
    }
    
    
    
    
    
}

[System.Serializable]
public class Need
{   
    [HideInInspector]
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float regenrate;
    public float decayRate;
    public Image uiBar;

    // The Add and Subtrack methods are located under the Needs tab.
    // Determines the max and min points of hp, energy, thirst and sleep bars..
    public void Add(float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Subtrack(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0);
    }
    // Gives the percentage of the bar
    public float GetPercentage()
    {
        return currentValue / maxValue;
    }
}

public interface IDamagable
{
    void TakePhysicDamage(int damageAmount);
}