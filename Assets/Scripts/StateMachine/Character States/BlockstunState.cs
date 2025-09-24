using UnityEngine;

public class BlockstunState : StunState
{
    public BlockstunState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.BLOCKSTUN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        stunTime = character.AtkTaken.Blockstun;

        character.GuardIntegrity -= character.AtkTaken.Damage;
        character.GetComponent<SpriteRenderer>().color = Color.blue;

        //character.Friction = .25f;
    }

    public override int StateAction()
    {
        nextStateId = base.StateAction();

        if (character.GuardIntegrity <= 0)
        {
            character.GuardBreak = true;
            return (int)GeneralStates.HITSTUN;
        }

        //  resets state when hit again
        //  unfortunately have to copy this because otherwise it would
        //  execute the base StartState method instead of the one of this state
        if (character.Hit)
        {
            StartState(prevStateId);
        }


        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.GetComponent<SpriteRenderer>().color = Color.white;
        //character.Friction = 1;
    }


}
