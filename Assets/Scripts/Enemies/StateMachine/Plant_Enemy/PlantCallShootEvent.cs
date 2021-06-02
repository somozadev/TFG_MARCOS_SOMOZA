using UnityEngine;
using StateMachine.Plant_Enemy;
using System.Collections;
using System.Collections.Generic;

public class PlantCallShootEvent : BatCallShootEvent
{
    private void Shoot()
    {
        PlantStateMachine stateMachine = GetComponent<PlantStateMachine>();
        Vector3 randomPos = new Vector3(stateMachine.shootingPoint.position.x + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.y + UnityEngine.Random.Range(-radius, radius), stateMachine.shootingPoint.position.z);

        StartCoroutine(WaitToLaunchMore(stateMachine, randomPos));
        // GameObject bullet = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        // bullet.GetComponent<PlantBullet>().Launch();

    }

    private IEnumerator WaitToLaunchMore(PlantStateMachine stateMachine, Vector3 randomPos)
    {
        GameObject bullet = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        bullet.GetComponent<PlantBullet>().Launch();
        yield return new WaitForSeconds(0.2f);
        GameObject bullet2 = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        bullet2.GetComponent<PlantBullet>().Launch();
        yield return new WaitForSeconds(0.2f);
        GameObject bullet3 = GameObject.Instantiate(stateMachine.bulletPrefab, randomPos, Quaternion.identity);
        bullet3.GetComponent<PlantBullet>().Launch();
    }
}
