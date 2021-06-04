using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Turtle_Enemy
{
    public class PatrolState : IState
    {
        public IState DoState(TurtleStateMachine stateMachine)
        {
            DoPatrol(stateMachine);
            if(stateMachine.enemy.conditions.isHitten && !stateMachine.enemy.conditions.isInvincible)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            
            else
                return this;
        }
        private void DoPatrol(TurtleStateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isIdle = false;
            stateMachine.enemy.conditions.isPatrol = true;
            stateMachine.enemy.conditions.isInvincible = false;
            stateMachine.navAgent.isStopped = false;
            stateMachine.SetPatrolAnim(true);

            if (stateMachine.currentPoint == null)
                stateMachine.currentPoint = stateMachine.points[0];
            if (stateMachine.navAgent.destination != stateMachine.currentPoint.Point)
                stateMachine.navAgent.SetDestination(stateMachine.currentPoint.Point);

            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.currentPoint.Point) <= stateMachine.navAgent.stoppingDistance )
            {
                stateMachine.currentPoint = stateMachine.currentPoint.nextPoint;
            }

        }

    }

}