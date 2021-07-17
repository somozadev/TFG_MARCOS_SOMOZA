using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Orc_Enemy
{
    public class PatrolTargetController : MonoBehaviour
    {
        [SerializeField] float hitDistance = 3f;
        [SerializeField] bool mid_hit, left_hit, right_hit, middle_left_hit, middle_right_hit;
        [SerializeField] bool is_rotating_left, is_rotating_right, is_rotating_180;
        private int layerMask;
        private RaycastHit hit1, hit2, hit3, hit4, hit5;
        private Enemy enemy;

        [SerializeField] private Vector3 startPos;

        private void Start()
        {
            startPos = transform.localPosition;
            enemy = GetComponentInParent<Enemy>();
            layerMask = LayerMask.GetMask("SceneLevel", "Player", "BulletIgnorer");
        }


        private void Update()
        {
            if (enemy.conditions.isPatrol)
            {
                Raycasts();
                ManageRotations();
            }
        }
        private void Raycasts()
        {
            SetRay(ref hit1, hitDistance, transform.forward, ref mid_hit); //forward raycast
            SetRay(ref hit2, hitDistance, (transform.forward + transform.right * Mathf.Cos(25)), ref right_hit);//right limit raycast
            SetRay(ref hit3, hitDistance, (transform.forward + transform.right * -Mathf.Cos(25)), ref left_hit);//left limit raycast
            SetRay(ref hit4, hitDistance, (transform.forward + transform.right * Mathf.Sin(10)), ref middle_right_hit);//MiddleRight limit raycast
            SetRay(ref hit5, hitDistance, (transform.forward + transform.right * -Mathf.Sin(10)), ref middle_left_hit);//MIddleLeft limit raycast

        }
        private void SetRay(ref RaycastHit hit, float distance, Vector3 direction, ref bool conditionalChanger)
        {
            if (Physics.Raycast(transform.position, direction, out hit, distance, layerMask))
            {
                // Debug.Log(hit.transform.gameObject.layer);
                // Debug.Log(" LayerMask.GetMask(Player):" +  LayerMask.GetMask("Player"));
                if (hit.transform.gameObject.layer != 13) //LayerMask.GetMask("Player") should be 13 but somehow marks 8192
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    conditionalChanger = true;
                }
                else
                    SetPursuit();
            }
            else
            {
                Debug.DrawRay(transform.position, direction * distance, Color.green);
                conditionalChanger = false;
            }

        }
        private void ManageRotations()
        {
            if (mid_hit || (mid_hit && middle_left_hit && middle_right_hit && left_hit && right_hit) && !is_rotating_right && !is_rotating_left)
                StartCoroutine(Turn180());

            else if ((left_hit || middle_left_hit || (left_hit && middle_left_hit)) && !is_rotating_right && !is_rotating_180)
                StartCoroutine(TurnLeft());
            else if ((right_hit || middle_right_hit || (right_hit && middle_right_hit)) && !is_rotating_left && !is_rotating_180)
                StartCoroutine(TurnRight());
        }


        private void SetPursuit()
        {
            enemy.conditions.isPursuitRange = true;
            enemy.conditions.isChasing = true;
            enemy.conditions.isPatrol = false;
        }


        private IEnumerator TurnLeft()
        {
            is_rotating_left = true;
            for (int i = 0; i < 9; i++)
            {
                enemy.transform.Rotate(new Vector3(0, 1, 0));
                yield return new WaitForEndOfFrame();
            }
            is_rotating_left = false;
        }
        private IEnumerator TurnRight()
        {
            is_rotating_right = true;
            for (int i = 0; i < 9; i++)
            {
                enemy.transform.Rotate(new Vector3(0, -1, 0));
                yield return new WaitForEndOfFrame();
            }
            is_rotating_right = false;
        }
        private IEnumerator Turn180()
        {
            is_rotating_180 = true;
            enemy.transform.Rotate(new Vector3(0, 180, 0));
            yield return new WaitForEndOfFrame();
            is_rotating_180 = false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, .2f);
        }

    }
}