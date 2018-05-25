using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVictory : IState
{
	public void Enter(ActorMonster entity)
	{
		entity.currentState = MotionState.VICTORY;
		entity.agent.isStopped = true;
		entity.m_Animator.SetInteger ("action", 31);
	}

	public void Exit(ActorMonster entity)
	{
		
	}

	public void Process(ActorMonster entity)
	{
		
	}
}

