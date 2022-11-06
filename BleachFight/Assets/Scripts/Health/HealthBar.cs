﻿using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables

    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;

    #endregion

    #region Basic_Method

    private void Start()
    {
        totalHealth.fillAmount = playerHealth.CurrentHealth / 10.00f;//відображення здоров'я в сердцях
    }

    private void Update()
    {
        currentHealth.fillAmount = playerHealth.CurrentHealth / 10.00f;
    }

    #endregion
}
