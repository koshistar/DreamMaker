using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LookAt : MonoBehaviour
{
    public GameObject target;

    //public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(target.transform.position);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") && nextScene != null) 
    //     {
    //         SceneManager.LoadScene(nextScene);
    //     }
    // }
}
