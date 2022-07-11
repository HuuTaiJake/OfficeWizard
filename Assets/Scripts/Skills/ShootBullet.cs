using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ShootBullet : NetworkBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    void Update()
    {
        if (this.isLocalPlayer && Input.GetKeyDown(KeyCode.F))
        {
            this.CmdShoot();
        }
    }
    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.right * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
}