using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyE : MonoBehaviour
{
    const int ARRAY_SIZE = 2;

    public GameObject[] camera1 = new GameObject[ARRAY_SIZE];

    public GameObject NumbersController;
    // Start is called before the first frame update
    void Start()
    {
        var bc = this.GetComponent<PressKeyE>();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKey("e"))
        {
            //change cameras
            camera1[0].SetActive(true);
            camera1[1].SetActive(false);

            //activate "Update function" of "NumbersControllersScript"
            var start = NumbersController.GetComponent<NumbersControllersScript>();
            start.start_sort = true;
             
        }      
    }
}
