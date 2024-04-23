using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHungerModifierSO : CharacterStatModifiersSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        BodyManager.Instance.UpHunger((int)value);
    }
}
