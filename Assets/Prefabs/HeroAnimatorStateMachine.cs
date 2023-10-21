using System;

[Serializable]
public class HeroAnimatorStateMachine<T> : AnimatorStateMachine<T> where T : Enum
{
    public void PullTrigger (int trigger) 
    {
        animator.SetTrigger(trigger);
    }
}


