using UnityEngine;

public class DefenseState : State
{
    private float aggressionTimer;
    private float destinationSwitchTimer;
    private float blockChanceRNG;

    //  chooses a spot to move to randomly within combat range
    private float randomTarget;

    public DefenseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.DEFENSIVE;
        stateMach = ((EnemyScript)c).BehaviorStateMach;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        aggressionTimer = Random.Range(((EnemyScript)character).AggresionTimerMin, ((EnemyScript)character).AggresionTimerMax);
        blockChanceRNG = Random.Range(0, 10);

        SwitchDestination();

        //  maybe instead of this, have enemy go into a separate state
        //  reuse knockdownstate? have some kind of reaction getting up?
        if (character.StateMach.CurrentState != (int)GeneralStates.KNOCKDOWN)
        {
            character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
        }

        character.Direction.x = 0;
    }

    private void SwitchDestination()
    {
        // Debug.Log("switched");

        //  this looks awful, i should probably do this better later
        //  if this is too unreadable for you, basically it just chooses a random spot within the engagement range
        //  on either to the left or right of the player
        randomTarget = ((EnemyScript)character).PlayerToLeft ? 
                        Random.Range(((EnemyScript)character).Target,
                        ((EnemyScript)character).CombatRangeMax)
                        :
                        Random.Range(((EnemyScript)character).CombatRangeMin,
                        ((EnemyScript)character).Target);


        // randomTarget = Random.Range(((EnemyScript)character).CombatRangeMin, ((EnemyScript)character).CombatRangeMax);

        randomTarget = Mathf.RoundToInt(randomTarget);

        destinationSwitchTimer = stateMach.StateTime + Random.Range(0, 1f); ;
    }

    public override int StateAction()
    {

        if (!((EnemyScript)character).InCombatRange)
        {
            return (int)BehaviorStates.CHASE;
        }

        //  movement
        if (Mathf.RoundToInt(character.transform.position.x) < randomTarget)
        {
            character.Blocking = false;
            character.Direction.x = 1;
        }
        else if (Mathf.RoundToInt(character.transform.position.x) > randomTarget)
        {
            character.Blocking = false;
            character.Direction.x = -1;
        }
        else
        {
            //  50% block chance (since 0 counts)
            character.Blocking = blockChanceRNG <= ((EnemyScript)character).BlockChance;
            character.Direction.x = 0;

            //  blocking the direction the player is in
            if (character.StateMach.CurrentState != (int)GeneralStates.KNOCKDOWN)
            {
                character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
            }
        }

        if (destinationSwitchTimer < stateMach.StateTime)
        {
            SwitchDestination();
        }

        if (aggressionTimer < stateMach.StateTime)
        {
            return (int)BehaviorStates.OFFENSIVE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
        character.Blocking = false;
    }
}
