using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");

        for (int i = 0; i < roots.Length; i++)
        {
            roots[i].GetComponentInParent<Root_Control>().Unlock(true);
            roots[i].GetComponentInParent<Root_Control>().Reset_transform();
        }

        GameObject Ball = GameObject.FindGameObjectWithTag("Ball");
        Ball.GetComponent<Ball_Manager>().ResetPos();
        Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ResetLevel()
    {
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");

        for (int i = 0; i < roots.Length; i++)
        {
            roots[i].GetComponentInParent<Root_Control>().Unlock(true);
            roots[i].GetComponentInParent<Root_Control>().Reset_transform();
        }

        GameObject Ball = GameObject.FindGameObjectWithTag("Ball");
        Ball.GetComponent<Ball_Manager>().ResetPos();
        Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
