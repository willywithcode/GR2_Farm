using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : Singleton<BodyManager>
{
    public Player player;
    [SerializeField] private Slider SliderHeath;
    [SerializeField] private Slider SliderHunger;

    public void setHeath(int heath)
    {
        SliderHeath.value = heath;
    }

    public void setHunger(int hunger)
    {
        SliderHunger.value = hunger;
    }

    //private void Start()
    //{
    //    //UpdateFillHunger();
    //    //StartCoroutine(ReduceHungerRoutine());
    //}

    public void SetMaxParameter(int maxHeath, int maxHunger)
    {
        SliderHeath.maxValue = maxHeath;
        SliderHeath.value = maxHeath;
        SliderHunger.maxValue = maxHunger;
        SliderHunger.value = maxHunger;
    }
    //public void UpHunger(int Boots)
    //{
    //    int value = Boots + Hunger;
    //    Hunger = value > maxHunger? maxHunger: value;
    //    UpdateFillHunger();
    //}

    //private IEnumerator ReduceHungerRoutine()
    //{
    //    yield return new WaitForSeconds(3f);
    //    ReduceHunger(5);
    //    UpdateFillHunger() ;
    //    StartCoroutine(ReduceHungerRoutine());
    //}
    //public void ReduceHunger(int amount)
    //{
    //    if(Hunger > amount)
    //    {
    //        Hunger -= amount;

    //    }
    //    else
    //    {
    //        Hunger = 0;
    //    }
    //    UpdateFillHunger();
    //}
    //public void UpdateFillHunger()
    //{
    //    SliderHunger.fillAmount = Mathf.Clamp01((float)Hunger / (float)maxHunger);
    //}
}
