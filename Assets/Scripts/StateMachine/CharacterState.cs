using UnityEngine;

abstract public class CharacterState : State
{
    protected bool animPlaying;
    protected float animTiming;
    [SerializeField] protected string animName;

    public CharacterState(CharacterScript c) : base(c)
    {
        //  theoretically, all names of animations should match the state scripts
        animName = GetType().Name;
        animName = animName.Substring(0, animName.Length - 5);
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        animPlaying = false;

        character.Anim.Play(animName);
    }

    public override int StateAction()
    {
        //  annoying i have to do things this way, for some reason putting
        //  this functionality in start still results in the clip info
        //  being of the previous playing animation
        if (!animPlaying)
        {
            AnimatorClipInfo[] clipInfo = character.Anim.GetCurrentAnimatorClipInfo(0);
            animTiming = clipInfo.Length > 0 ? clipInfo[0].clip.length : .5f;
            animPlaying = true;
        }

        return nextStateId;
    }
}
