using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmBoss : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOMoveY(45, 1));
        mySequence.Append(transform.DOMoveX(0, 1));
        mySequence.Append(transform.DOMoveY(0, 1));
        mySequence.Append(transform.DOMoveX(transform.position.x, 1));
        mySequence.Append(transform.DOMoveY(transform.position.y, 1));

        mySequence.SetLoops(-1);
        //transform.DOMove(new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.DOMove(new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), 1f);
    }
}
