using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider theSlider;
    public Gradient gradient;
    public Image fill;
    public int maxHealth;
    public int currentHealth;
    public CreatureAttribute playerAttribute;

    public HealthBar healthbar;

    void Start()
    {
        playerAttribute = GameObject.FindGameObjectWithTag("Player").GetComponent<CreatureAttribute>();
        maxHealth = playerAttribute.maxHealth;
        SetMaxHealth(maxHealth);
    }
    void Update()
    {
        currentHealth = playerAttribute.currentHealth;
        SetHealth(currentHealth);
    }

    public void SetMaxHealth(int health)
    {
        theSlider.maxValue = health;
        theSlider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        theSlider.value = health;

        fill.color = gradient.Evaluate(theSlider.normalizedValue);
    }



}
