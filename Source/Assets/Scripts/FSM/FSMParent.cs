using System;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMParent
{

	protected Dictionary<string, IState> theFSM = new Dictionary<string, IState>();

	public FSMParent()
	{
	}

	public virtual void ChangeStatus(ActorMonster entity,string newState)
	{
	}
}
