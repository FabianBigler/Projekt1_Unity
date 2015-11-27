using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public abstract class AICharacterState {
    public virtual void Injure(AICharacterControl context) { }

    public virtual void Heal(AICharacterControl context) { }

    public virtual void Scare(AICharacterControl context) { }

    public virtual void Blind(AICharacterControl context) { }

    public virtual void Abandon(AICharacterControl context) { }

    public virtual void TalkTo(AICharacterControl context) { }

    public virtual void Kill(AICharacterControl context) {
        context.IsDead = true;
        context.SetState(new AICharacterStateDead());
        context.playerController.Tell("Kelly", "I'm...sorry...I...failed...");
        context.Skeleton.SetActive(true);
        context.Body.SetActive(false);
        context.moveState = MoveState.Stand;
    }
}


public class AICharacterStateFollow : AICharacterState
{
    public override void Abandon(AICharacterControl context)
    {
        context.SetState(new AICharacterStateDead());
        context.IsDead = true;
    }

    public override void Injure(AICharacterControl context)
    {
        context.SetState(new AICharacterStateStand());
        context.playerController.Tell("Kelly", "This...thing hit me! I'm not feeling well...");
        context.IsInjured = true;
    }

    public override void Scare(AICharacterControl context)
    {
        context.SetState(new AICharacterStateStand());
        context.IsScared = true;
        context.playerController.Tell("Kelly", "Oh...I'm so scared");
        context.moveState = MoveState.Stand;
        
    }

    public override void TalkTo(AICharacterControl context)
    {
        if (!context.IsDead)
            context.IsScared = false;
            context.moveState = MoveState.Follow;
            context.SetState(new AICharacterStateFollow());
    }
}


public class AICharacterStateStand : AICharacterState
{
    public override void Abandon(AICharacterControl context)
    {
        context.SetState(new AICharacterStateDead());
    }

    public override void Heal(AICharacterControl context)
    {
        context.SetState(new AICharacterStateFollow());
    }

    public override void TalkTo(AICharacterControl context)
    {
        context.SetState(new AICharacterStateFollow());
        context.IsScared = false;
        context.moveState = MoveState.Follow;
    }
}

public class AICharacterStateDead : AICharacterState
{
    public override void TalkTo(AICharacterControl context)
    {
        //maybe add a joke answer
    }
}