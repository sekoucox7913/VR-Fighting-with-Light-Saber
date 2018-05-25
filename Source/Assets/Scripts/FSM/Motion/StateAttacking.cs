using UnityEngine;
using System.Collections;
//using Htc.Util;
//using Htc.GameData;

public class StateAttacking : IState {

	public void Enter(ActorMonster entity)
	{
		entity.currentState = MotionState.ATTACKING;
		entity.agent.isStopped = true;
	}

	public void Exit(ActorMonster entity)
	{
	}

	public void Process(ActorMonster entity)
	{
		LookAtPlayer (entity);
		int percent = Random.Range (0, 100);
		if (percent < 20) {
			entity.m_Animator.SetInteger ("action",20);	
			return;
		}
		if (entity.m_bInAttackAnimation)
			return;
		int rand = Random.Range (7, (7 + entity._monsterData.m_iAttackAnimCount));
		entity.m_Animator.SetInteger ("action",rand);
	}

	void LookAtPlayer(ActorMonster entity)
	{
		if (entity.m_bInAttackAnimation)
			return;
		Vector3 lookPos = new Vector3 (entity.target.position.x, entity.transform.position.y, entity.target.position.z);
		entity.transform.LookAt (lookPos);
	}
}