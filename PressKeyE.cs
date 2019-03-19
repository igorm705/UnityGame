using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyE : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        var bc = this.GetComponent<PressKeyE>();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
        }
    }
}
