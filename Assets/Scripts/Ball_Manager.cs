using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Manager : MonoBehaviour
{

    private Root_Control lastroot = null;
    private Vector3 m_Startpos;

    // Start is called before the first frame update
    void Start()
    {
        m_Startpos= transform.position;
    }

    public void ResetPos()
    {
        StartCoroutine(timeReset());
    }

    private IEnumerator timeReset()
    {
        yield return new WaitForSeconds(0.5f);

        transform.position = m_Startpos;
        lastroot = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Root"))
        {
            if(lastroot == null)
            {
                lastroot = collision.gameObject.GetComponentInParent<Root_Control>();
                collision.gameObject.GetComponentInParent<Root_Control>().Unlock();
            }
            else if(lastroot.gameObject != collision.transform.parent.gameObject) 
            {
                lastroot.Unlock();
                lastroot = collision.gameObject.GetComponentInParent<Root_Control>();
                collision.gameObject.GetComponentInParent<Root_Control>().Unlock();
            }
            transform.parent = collision.transform.parent.transform;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Root"))
        {
            transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
