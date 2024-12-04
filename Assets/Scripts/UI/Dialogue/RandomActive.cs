//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActive : MonoBehaviour
{
    public List<GameObject> active = new List<GameObject>();

    private void Awake()
    {
        active[Random.Range(0, active.Count)].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
