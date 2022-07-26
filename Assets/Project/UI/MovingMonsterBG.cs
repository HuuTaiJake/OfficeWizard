using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMonsterBG : MonoBehaviour
{
    public float speed;
    public GameObject movingPoint1;
    public GameObject movingPoint2;
    private bool movingTo1 = true;
    private Vector3 moveTo1Scale;
    private Vector3 moveTo2Scale;
    // Start is called before the first frame update
    void Start()
    {
        moveTo1Scale = transform.localScale;
        moveTo2Scale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(movingTo1 == true)
        {
            MoveTo1();
            if (transform.position == movingPoint1.transform.position)
            {
                movingTo1 = false;
            }
        }
        else
        {
            MoveTo2();
            if (transform.position == movingPoint2.transform.position)
            {
                movingTo1 = true;
            }
        }

    }
    public void MoveTo1()
    {
        transform.localScale = moveTo1Scale;
        transform.position = Vector3.MoveTowards(transform.position, movingPoint1.transform.position, speed * Time.deltaTime);
    }
    public void MoveTo2()
    {
        transform.position = Vector3.MoveTowards(transform.position, movingPoint2.transform.position, speed * Time.deltaTime);
        transform.localScale = moveTo2Scale;
    }
}
