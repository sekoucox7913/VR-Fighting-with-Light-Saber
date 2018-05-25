using System;

public interface IState
{
	// enter state
	void Enter(ActorMonster entity);

	// exit state
	void Exit(ActorMonster entity);

	// process state
	void Process(ActorMonster entity);
}
