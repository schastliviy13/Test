using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Image bar;
    public void Awake()
    {
        entity.OnHealthChanged += Entity_OnHealthChanged;
    }

    private void Entity_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHp();
    }

    private void UpdateHp()
    {
       bar.fillAmount = entity.GetHealth() / 100;
    }
}
