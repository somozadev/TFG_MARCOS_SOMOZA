using UnityEngine;
using StateMachine.Dragon_Enemy;

public class DragonCallShootEvent : BatCallShootEvent
{
    private void Shoot()
    {
        DragonStateMachine stateMachine = GetComponent<DragonStateMachine>();
        Vector3 randomPos = new Vector3(stateMachine.shootingPoint.position.x + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.y + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.z);
        GameObject bullet = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * stateMachine.enemy.stats.ShootingRange * 1.7f, ForceMode.Impulse);
        stateMachine.enemy.conditions.isChasing = true; 
    }
}
