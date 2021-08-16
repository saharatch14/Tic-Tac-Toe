using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] whoturnUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        whoturnUI[0].SetActive(false);
        whoturnUI[1].SetActive(false);
        whoturnUI[2].SetActive(false);
        whoturnUI[3].SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
