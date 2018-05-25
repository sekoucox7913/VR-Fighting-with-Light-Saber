using UnityEngine;
using System.Collections;
//using Htc.GameData;

public class StateIdle : IState
{
	public void Enter(ActorMonster entity)
	{
//		entity.currentState = MotionState.IDLE;

//		//if (entity is ActorPlayer) {
//		//	ActorPlayer.Instance.SetTouchPadDelayTime = 0f;            
//		//}
//
//		if (entity is ActorDragon) {
//			ActorDragon actorDragon = entity as ActorDragon;
//			actorDragon.aiPath.target = null;
//			actorDragon.InvokeRepeating ("Think", 0, 5f);
//		}
//
//		if (entity is ActorMonster) {
//			ActorMonster monster = entity as ActorMonster;
//			monster.agent.speed = 2;
//			monster.InvokeRepeating ("Think", 0, 1);
//		}
	}

	public void Exit(ActorMonster entity)
	{
//		if (entity is ActorDragon) {
//			ActorDragon actorDragon = entity as ActorDragon;
//			actorDragon.CancelInvoke ("Think");
//		}
//
//		if (entity is ActorMonster) {
//			ActorMonster monster = entity as ActorMonster;
//			monster.previousState = entity.currentState;
//			monster.CancelInvoke ("Think");
//		}
	}

	public void Process(ActorMonster entity)
	{
	}
}
