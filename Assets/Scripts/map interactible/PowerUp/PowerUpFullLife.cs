using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFullLife : PowerUp
{
    public override void TakePowerUp(PlayerController actor)
    {
        base.TakePowerUp(actor);
        actor.ChangeHp(actor.maxHealth);
    }
}
