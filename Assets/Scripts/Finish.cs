using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject Finish_Screen;
    public GameObject[] deactivate;
    public string next_Level_Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < deactivate.Length; i++)
            {
                deactivate[i].active = false;
            }
            StartCoroutine(ShowScreen());
        }
    }

    private IEnumerator ShowScreen()
    {
        Finish_Screen.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(next_Level_Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
