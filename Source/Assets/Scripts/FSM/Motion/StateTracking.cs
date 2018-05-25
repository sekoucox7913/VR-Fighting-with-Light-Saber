using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateTracking : IState {

	public void Enter(ActorMonster entity)
	{
		entity.currentState = MotionState.TRACKING;
	}

	public void Exit(ActorMonster entity)
	{
		
		ActorMonster monster = entity as ActorMonster;
		monster.previousState = entity.currentState;
	}

	public void Process(ActorMonster entity)	
	{
//		Debug.Log ("tracking =========== " + entity.currentState);
		entity.m_Animator.SetInteger ("action", 2);
		if (entity.agent.isStopped)
			entity.agent.isStopped = false;
		entity.agent.speed = entity._monsterData.m_iSpeed;
		entity.agent.SetDestination (entity.target.position);
	}
}