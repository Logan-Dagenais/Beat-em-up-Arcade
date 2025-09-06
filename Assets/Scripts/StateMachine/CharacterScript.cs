using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Vector2 direction;
    public bool attacking;
    public bool blocking;

    //  states that both player and enemies should have
    public enum GeneralStates
    {
        IDLE,
        WALK,
        CROUCH,
        AIR,
        ATTACK,
        HITSTUN,
        KNOCKDOWN,
        BLOCK,
        DODGE
    }

}
