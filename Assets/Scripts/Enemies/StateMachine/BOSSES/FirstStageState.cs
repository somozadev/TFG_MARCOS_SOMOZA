using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.BossCentipede
{
    public class FirstStageState : IState
    {
        private bool callRotR = true;
        private int layerMask;
        private RaycastHit hit1, hit2, hit3, hit4, hit5;
        [SerializeField] bool mid_hit, left_hit, right_hit, middle_left_hit, middle_right_hit;
        [SerializeField] bool is_rotating_left, is_rotating_right, is_rotating_180;
        [SerializeField] float hitDistance = 12f;


        public IState DoState(BossCentipedeStateMachine stateMachine)
        {
            layerMask = LayerMask.GetMask("SceneLevel", "Player", "BulletIgnorer");
            DoStage1(stateMachine);
            return stateMachine.firstStageState;
        }

        private void DoStage1(BossCentipedeStateMachine stateMachine)
        {
            stateMachine.movement.MoveToPlayer();//RunFromPlayer();
            // stateMachine.movement.Move();
            // stateMachine.movement.Forward();
            // Raycasts(stateMachine);
            // ManageRotations(stateMachine);
        }
        private void Raycasts(BossCentipedeStateMachine stateMachine)
        {
            SetRay(ref hit1, hitDistance, -1 * stateMachine.movement.head.transform.forward, ref mid_hit, stateMachine); //forward raycast
            SetRay(ref hit2, hitDistance, -1 * (stateMachine.movement.head.transform.forward + stateMachine.movement.head.transform.right * Mathf.Cos(25)), ref right_hit, stateMachine);//right limit raycast
            SetRay(ref hit3, hitDistance, -1 * (stateMachine.movement.head.transform.forward + stateMachine.movement.head.transform.right * -Mathf.Cos(25)), ref left_hit, stateMachine);//left limit raycast
            SetRay(ref hit4, hitDistance, -1 * (stateMachine.movement.head.transform.forward + stateMachine.movement.head.transform.right * Mathf.Sin(10)), ref middle_right_hit, stateMachine);//MiddleRight limit raycast
            SetRay(ref hit5, hitDistance, -1 * (stateMachine.movement.head.transform.forward + stateMachine.movement.head.transform.right * -Mathf.Sin(10)), ref middle_left_hit, stateMachine);//MIddleLeft limit raycast

        }
        private void SetRay(ref RaycastHit hit, float distance, Vector3 direction, ref bool conditionalChanger, BossCentipedeStateMachine stateMachine)
        {
            if (Physics.Raycast(stateMachine.movement.head.transform.position, direction, out hit, distance, layerMask))
            {
                // Debug.Log(hit.transform.gameObject.layer);
                // Debug.Log(" LayerMask.GetMask(Player):" +  LayerMask.GetMask("Player"));
                if (hit.transform.gameObject.layer != 13) //LayerMask.GetMask("Player") should be 13 but somehow marks 8192
                {
                    Debug.DrawRay(stateMachine.movement.head.transform.position, direction * hit.distance, Color.red);
                    conditionalChanger = true;
                    Debug.Log("SOMETHITTING");

                }
                else
                {

                }
            }
            else
            {
                Debug.DrawRay(stateMachine.movement.head.transform.position, direction * distance, Color.green);
                conditionalChanger = false;
            }

        }

        private void ManageRotations(BossCentipedeStateMachine stateMachine)
        {
            // if (mid_hit || (mid_hit && middle_left_hit && middle_right_hit && left_hit && right_hit) && !is_rotating_right && !is_rotating_left)
            //     StartCoroutine(Turn180());

            if ((left_hit || middle_left_hit || (left_hit && middle_left_hit)) && !is_rotating_right && !is_rotating_180)
                stateMachine.movement.RotLeft();
            else if ((right_hit || middle_right_hit || (right_hit && middle_right_hit)) && !is_rotating_left && !is_rotating_180)
                stateMachine.movement.RotRight();
            else if (!rotCalled)
            {
                rotCalled = true;
                stateMachine.StartCoroutine(noR(stateMachine));
            }
        }
        bool rotCalled = false;
        private IEnumerator noR(BossCentipedeStateMachine stateMachine)
        {
            stateMachine.movement.NoRot();
            yield return new WaitForSeconds((float)Random.Range(0.4f, 2.1f));
            rotCalled = false;

        }
        //private void SetPursuit()
        //{
        //    enemy.conditions.isPursuitRange = true;
        //    enemy.conditions.isChasing = true;
        //    enemy.conditions.isPatrol = false;
        //}
    }

}
