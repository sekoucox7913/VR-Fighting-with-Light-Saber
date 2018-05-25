using UnityEngine;
using System;
using System.Collections;

static public class  MotionStateSet 
{
	static public StateIdle stateIdle = new StateIdle();
	static public StateTracking stateTracking = new StateTracking();
	static public StateDead stateDead = new StateDead();
	static public StateAttacking stateAttacking = new StateAttacking();
	static public StateHit stateHit = new StateHit();
	static public StateVictory stateVictory = new StateVictory();
}

public class FSMMotion : FSMParent
{
	public FSMMotion()
	{
		theFSM.Add(MotionState.IDLE, MotionStateSet.stateIdle);
		theFSM.Add(MotionState.TRACKING, MotionStateSet.stateTracking);
		theFSM.Add(MotionState.DEAD, MotionStateSet.stateDead);
		theFSM.Add(MotionState.ATTACKING, MotionStateSet.stateAttacking);
		theFSM.Add(MotionState.HIT, MotionStateSet.stateHit);
		theFSM.Add (MotionState.VICTORY, MotionStateSet.stateVictory);
	}

	//
	public override void ChangeStatus(ActorMonster entity,string newState)
	{
		if (entity.currentState == MotionState.DEAD) {
			return;
		}

		if (theFSM.ContainsKey (newState) == false) {
			Debug.LogError ("error state in Motion FSM.");
			return;
		}

		if (entity.currentState != newState) {
			theFSM [entity.currentState].Exit (entity);
			theFSM [newState].Enter (entity);
		}
		theFSM [newState].Process (entity);
	}
}