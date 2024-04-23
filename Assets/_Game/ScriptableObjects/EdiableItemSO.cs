using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdiableItemSO : ItemSO, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> modifiersData = new List<ModifierData>();

    public string ActionName => "Consume";

    public bool PerformAction(GameObject character)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    bool PerformAction(GameObject character);
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifiersSO statModifier;
    public float value;
}
