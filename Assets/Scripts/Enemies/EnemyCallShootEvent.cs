using UnityEngine;
using StateMachine.Bat_Enemy;
public class EnemyCallShootEvent : MonoBehaviour
{
    float radius = 0.1f;
    private void Shoot()
    {
        // transform.LookAt(GameManager.Instance.player.transform.position);
        BatStateMachine stateMachine = GetComponent<BatStateMachine>();
        stateMachine.navAgent.updateRotation = true;
        Vector3 randomPos = new Vector3(stateMachine.shootingPoint.position.x + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.y + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.z);
        GameObject bullet = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * stateMachine.enemy.stats.ShootingRange, ForceMode.Impulse);
        stateMachine.enemy.conditions.isChasing = true;
    }
}
