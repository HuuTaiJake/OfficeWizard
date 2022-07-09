using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootSphere : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject g = Instantiate(ball, firePoint.position, firePoint.rotation);
            g.GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
        }
    }
}
