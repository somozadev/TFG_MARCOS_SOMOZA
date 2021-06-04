using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Golem_Enemy
{
    public class PatrolTargetController : MonoBehaviour
    {
        [SerializeField] float hitDistance = 3f;
        [SerializeField] bool mid_hit;
        private int layerMask;
        private RaycastHit hit;
        [SerializeField] private Vector3 startPos;

        private Enemy enemy;



        private void Start()
        {
            startPos = transform.localPosition;
            enemy = GetComponentInParent<Enemy>();
            layerMask = LayerMask.GetMask("SceneLevel", "Player");
        }


        private void Update()
        {
            if (enemy.conditions.isPatrol)
            {
                SetRay(ref hit, hitDistance, transform.forward, ref mid_hit);
                if (mid_hit)
                {
                    StartCoroutine(TurnRight());
                }
            }
        }

        private void SetRay(ref RaycastHit hit, float distance, Vector3 direction, ref bool conditionalChanger)
        {
            if (Physics.Raycast(transform.position, direction, out hit, distance, layerMask))
            {
                Debug.Log(hit.transform.gameObject.layer);
                if (hit.transform.gameObject.layer != 13) //LayerMask.GetMask("Player") should be 13 but somehow marks 8192
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    conditionalChanger = true;
                }
                else
                {
                    // SetPursuit();
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * distance, Color.green);
                conditionalChanger = false;
            }

        }
        private IEnumerator TurnRight()
        {
            for (int i = 0; i < 9; i++)
            {
                enemy.transform.Rotate(new Vector3(0, -1, 0));
                yield return new WaitForEndOfFrame();
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, .2f);
        }

    }
}