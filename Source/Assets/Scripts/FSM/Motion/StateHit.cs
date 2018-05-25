using UnityEngine;
using System.Collections;
//using Htc.GameData;

public class StateHit : IState {

	public void Enter(ActorMonster entity)
	{
		entity.currentState = MotionState.HIT;
	}

	public void Exit(ActorMonster entity)
	{
	}

	public void Process(ActorMonster entity)
	{
		entity.agent.speed = 0;
		entity.m_Animator.SetInteger ("action",12);
	}    
}