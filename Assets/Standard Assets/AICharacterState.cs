using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public abstract class AICharacterState {
    public bool Injured { get; set; }
    public bool Blinded { get; set; }
    public bool Scared { get; set; }

    public abstract void Injure(AICharacterControl context);

    public abstract void Heal(AICharacterControl context);

    public abstract void Scare(AICharacterControl context);

    public abstract void Blind(AICharacterControl context);

    public abstract void Abandon(AICharacterControl context);

    public abstract void TalkTo(AICharacterControl context);
}


public class AICharacterStateFollow : AICharacterState
{
    public override void Abandon(AICharacterControl context)
    {
        context.SetState(new AICharacterStateDead());
        context.IsDead = true;
    }

    public override void Blind(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Heal(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Injure(AICharacterControl context)
    {
        context.SetState(new AICharacterStateStand());
        context.IsInjured = true;
    }

    public override void Scare(AICharacterControl context)
    {
        context.SetState(new AICharacterStateStand());
    }

    public override void TalkTo(AICharacterControl context)
    {
        if(!context.IsDead) context.SetState(new AICharacterStateFollow());
    }
}


public class AICharacterStateStand : AICharacterState
{
    public override void Abandon(AICharacterControl context)
    {
        context.SetState(new AICharacterStateDead());
    }

    public override void Blind(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Heal(AICharacterControl context)
    {
        context.SetState(new AICharacterStateFollow());
    }

    public override void Injure(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Scare(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void TalkTo(AICharacterControl context)
    {
        context.SetState(new AICharacterStateFollow);
        context.moveState = MoveState.Follow;
    }
}

public class AICharacterStateDead : AICharacterState
{
    public override void Abandon(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Blind(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Heal(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Injure(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void Scare(AICharacterControl context)
    {
        throw new NotImplementedException();
    }

    public override void TalkTo(AICharacterControl context)
    {
        throw new NotImplementedException();
    }
}