using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClick : MonoBehaviour
{
    [SerializeField] GameObject onObject;
    [SerializeField] GameObject offObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate()
    {
        onObject.SetActive(true);
        offObject.SetActive(false);
    }
}
