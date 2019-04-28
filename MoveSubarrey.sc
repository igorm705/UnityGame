using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSubarrey : MonoBehaviour
{
    //object of number
    public GameObject number;
    public Vector3 target;//definition of new vector of target place
    public float speed = 1.0f;    //speed                              
    

    // Start is called before the first frame update
    void Start()
    {
        //disable this script 
        this.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        number.transform.position = Vector3.MoveTowards(number.transform.position, target, step);

        if(target == number.transform.position)
        {
            this.enabled = false;
        }
    }
}
