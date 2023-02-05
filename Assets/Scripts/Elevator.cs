using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Sequence sequence;
    private float halfPlaytime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(5f, halfPlaytime).SetLoops(-1, LoopType.Yoyo);
        //sequence.Append(transform.DOMoveY(10f, halfPlaytime).SetLoops(-1,LoopType.Yoyo));
        //sequence.Append(transform.DOMoveY(-5f, halfPlaytime).SetDelay(halfPlaytime).SetLoops(-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
