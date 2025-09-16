using UnityEngine;

public class BlockState : State, IAttackInteraction
{

    private AttackProperties atkTaken;
    private bool hitFromLeft;

    public BlockState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.BLOCK;
    }

    public void PassToState(AttackProperties atkTaken, bool hitFromLeft)
    {
        this.atkTaken = atkTaken;
        this.hitFromLeft = hitFromLeft;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

    }

    public override int StateAction()
    {
        //  if holding crouch, character is blocking low
        bool blockLow = character.Direction.y < 0;

        if (character.Hit &&
            (character.Facingleft != hitFromLeft || atkTaken.Low != blockLow))
        {
            return (int)GeneralStates.HITSTUN;
        }
        else if(character.Hit)
        {
            Debug.Log("Blocked");
            character.Hit = false;
        }

            character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

        if (!character.Blocking)
        {
            return blockLow ? (int)GeneralStates.CROUCH:(int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.Hit = false;
    }
}
