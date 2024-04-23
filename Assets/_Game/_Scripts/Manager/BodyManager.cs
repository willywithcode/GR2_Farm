using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : Singleton<BodyManager>
{
    private int maxHunger = 100;
    [SerializeField] private Image SliderHunger;
    public int Hunger;      
    private void Start()
    {
        Hunger = maxHunger;
        UpdateFillHunger();
        StartCoroutine(ReduceHungerRoutine());
    }
    public void UpHunger(int Boots)
    {
        int value = Boots + Hunger;
        Hunger = value > maxHunger? maxHunger: value;
        UpdateFillHunger();
    }

    private IEnumerator ReduceHungerRoutine()
    {
        yield return new WaitForSeconds(3f);
        ReduceHunger(5);
        UpdateFillHunger() ;
        StartCoroutine(ReduceHungerRoutine());
    }
    public void ReduceHunger(int amount)
    {
        if(Hunger > amount)
        {
            Hunger -= amount;

        }
        else
        {
            Hunger = 0;
        }
        UpdateFillHunger();
    }
    public void UpdateFillHunger()
    {
        SliderHunger.fillAmount = Mathf.Clamp01((float)Hunger / (float)maxHunger);
    }
}
