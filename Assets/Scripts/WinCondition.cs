using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject[] enemys;
    public int numEnemys;
    // Start is called before the first frame update
    void Start()
    {
        numEnemys = enemys.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemys <= 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    public void RestarEnemigo()
    {
           numEnemys--;
    }
}
