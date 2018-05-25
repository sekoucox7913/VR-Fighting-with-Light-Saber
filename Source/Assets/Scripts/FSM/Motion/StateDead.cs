using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Htc.Util;

public class StateDead : IState {

	public void Enter(ActorMonster entity)
	{
		entity.currentState = MotionState.DEAD;
		entity.agent.speed = 0;
		entity.agent.enabled = false;
		entity.m_Animator.SetInteger ("action",30);
		ActorPlayer.Instance.m_DeadMonsterCount++;
		BattleManager.Instance.EndMonster (entity);
		GameManager.Instance.m_PlayerScore++;
	}

	public void Exit(ActorMonster entity)
	{
		
	}

	public void Process(ActorMonster entity)
	{
		GameObject.Destroy (entity.gameObject, entity._monsterData.m_iDeadTime);
	}
}