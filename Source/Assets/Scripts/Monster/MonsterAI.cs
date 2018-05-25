using UnityEngine;
using System.Collections;

namespace Game.AI
{
	public class MonsterAI
	{
		string curState = MotionState.IDLE;
		ActorMonster entity;
		float distance = 0;

		public MonsterAI(){
			
		}

		public void CheckState(ActorMonster actor) {			
			entity = actor;
			if (entity.target == null)
				return;
			if (entity.currentState == MotionState.DEAD)
				return;
			if (ActorPlayer.Instance.currentHP <= 0) {
				entity.m_bInHitAnimation = false;
				entity.GotoState (MotionState.VICTORY);
				return;
			}			
			if (entity.m_bInHitAnimation)
				return;
			
			curState = MotionState.TRACKING;		
			distance = Vector3.Distance (entity.transform.position, entity.target.position);
//			Debug.Log ("distance===============   " + distance);

			if (CheckAttackRange())
				curState = MotionState.ATTACKING;

			if (IsDead ())
				curState = MotionState.DEAD;

			entity.GotoState (curState);
		}

		public bool IsDead()
		{
			return (entity.currentHP <= 0);
		}

		public bool CheckAttackRange()
		{
			return distance < entity._monsterData.m_iAttackRange;
		}
	}
}