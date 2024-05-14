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

    public void SetMaxParameter(int maxHeath, int maxHunger)
    {
        SliderHeath.maxValue = maxHeath;
        SliderHeath.value = maxHeath;
        SliderHunger.maxValue = maxHunger;
        SliderHunger.value = maxHunger;
    }

}
