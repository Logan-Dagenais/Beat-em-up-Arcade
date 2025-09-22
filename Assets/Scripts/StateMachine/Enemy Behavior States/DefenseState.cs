using UnityEngine;

public class DefenseState : State
{
    private float aggressionTimer;
    private float destinationSwitchTimer;
    private float blockChance;

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

        aggressionTimer = Random.Range(1f, 6f);
        blockChance = Random.Range(0, 10);

        SwitchDestination();

        character.Direction.x = 0;
    }

    private void SwitchDestination()
    {
        // Debug.Log("switched");

        //  this looks awful, i should probably do this better later
        //  if this is too unreadable for you, basically it just chooses a random spot within the engagement range
        //  on either to the left or right of the player
        randomTarget = ((EnemyScript)character).PlayerToLeft ? 
                        Random.Range(((EnemyScript)character).PlayerTransform.transform.position.x,
                        ((EnemyScript)character).PlayerTransform.transform.position.x + ((EnemyScript)character).CombatRange)
                        :
                        Random.Range(((EnemyScript)character).PlayerTransform.transform.position.x - ((EnemyScript)character).CombatRange,
                        ((EnemyScript)character).PlayerTransform.transform.position.x);

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
            character.Blocking = blockChance <= 4;
            character.Direction.x = 0;

            //  blocking the direction the player is in
            character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
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
