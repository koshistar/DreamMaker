using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private Animator anim;

    public GameObject StartGameObject;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            anim.SetTrigger("Start");
        }
    }

    public void StartGame()
    {
        StartGameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
