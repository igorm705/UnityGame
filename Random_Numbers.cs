using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Numbers : MonoBehaviour
{
     const int ARRAY_SIZE = 7;

    //textMesh
    private TextMesh textMesh1;

    //central array of GameObjects
    public GameObject[] numbers = new GameObject[ARRAY_SIZE];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            //place cubes with numbers
            float temp_float = UnityEngine.Random.Range(-100.0f, 100.0f);
            int temp_int = (int)temp_float;
            textMesh1 = numbers[i].GetComponent<TextMesh>();
            textMesh1.text = temp_int.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
