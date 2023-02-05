using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Root_Control : MonoBehaviour
{
    public float speed = 1.0f;
    public float release_speed = 3.0f;
    public float release_time = 0.1f;
    public float release_angle = 90f;
    public bool left = false;

    private GameObject Player_Ball;
    private float Timer = 0f;
    private bool unlock = false;
    private float lerp_var = 0f;
    private Slider slider;
    private Vector3 m_Starttransform;

    // Start is called before the first frame update
    void Start()
    {
        Player_Ball = GameObject.Find("Player_Ball");
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        m_Starttransform = transform.rotation.eulerAngles;
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music_Manage>().PlayMusic();
    }

    public void Unlock(bool reset = false)
    {
        if (unlock || reset)
        {
            unlock = false;
        }
        else
        {
            unlock = true;
            Timer = 0f;
        }
    }

    public void Reset_transform()
    {
        transform.DORotate(m_Starttransform, 0.5f).SetEase(Ease.OutBounce, 0.4f);
        Timer = 0f;
    }

    private void FixedUpdate()
    {
        if (unlock)
        {
            if (Input.GetAxis("Rotate") != 0)
            {
                transform.Rotate(0, 0, 0.01f * Input.GetAxis("Rotate") * speed);
            }
            if (Input.GetAxis("Tension") != 0)
            {
                lerp_var += Input.GetAxis("Tension") * 0.01f;
                if (lerp_var < 0.78f && lerp_var > 0.76f)
                {
                    if (Input.GetAxis("Tension") > 0) lerp_var = 0.78f;
                    if (Input.GetAxis("Tension") < 0) lerp_var = 0.76f;
                }
                release_speed = Mathf.Lerp(200f, 900f, lerp_var);
                release_time = Mathf.Lerp(1f, 0.2f, lerp_var);
                release_angle = Mathf.Lerp(70f, 90f, lerp_var);
                slider.value = lerp_var;
            }
            if (Input.GetAxis("Fire") != 0)
            {
                if (Timer > 0.6f || Timer == 0f)
                {
                    if (!left)
                    {
                        transform.DORotate(new Vector3(0f, 0f, transform.rotation.eulerAngles.z - release_angle), release_time, RotateMode.Fast).SetEase(Ease.OutBounce, 0.6f, release_time * 2);
                    }
                    else
                    {
                        transform.DORotate(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + release_angle), release_time, RotateMode.Fast).SetEase(Ease.OutBounce, 0.6f, release_time * 2);
                    }
                    StartCoroutine(ApplyForce());
                    Timer = Time.deltaTime;
                    GetComponent<AudioSource>().Play();
                }
                else
                {
                    Timer += Time.deltaTime;
                }
            }
            if(slider.value != lerp_var)
            {
                lerp_var = slider.value;
                if (lerp_var < 0.78f && lerp_var > 0.76f)
                {
                    lerp_var = 0.78f;
                    slider.value = lerp_var;
                }
                release_speed = Mathf.Lerp(200f, 900f, lerp_var);
                release_time = Mathf.Lerp(1f, 0.2f, lerp_var);
                release_angle = Mathf.Lerp(70f, 90f, lerp_var);
            }
        }
    }

    private IEnumerator ApplyForce()
    {
        yield return new WaitForSeconds(release_time - (release_time * 0.75f));

        if (Player_Ball.transform.parent != null)
        {
            if (!left)
            {
                Player_Ball.GetComponent<Rigidbody2D>().AddForce((transform.up + (transform.right * -0.25f)) * release_speed);
            }
            else
            {
                Player_Ball.GetComponent<Rigidbody2D>().AddForce((transform.up + (transform.right * 0.25f)) * release_speed);
            }
        }
    }
}
