using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Image bar;
    void Start()
    {
        PlayerController.HealthChangedEvent += OnHealthChanged;
    }

    void OnHealthChanged(float _health, float _maxHealth)
    {
        bar.fillAmount = _health / _maxHealth;
    }
}
