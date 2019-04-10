using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour
{

    [SerializeField]
    GameObject TopEyelid, Bottomeyelid;

    [SerializeField]
    float timer;

    bool blink = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        EyelidAnim();

    }

    void EyelidAnim()
    {
        if (timer <= 0)
        {
            blink = true;
        }

        if (blink == true)
        {

            TopEyelid.GetComponent<Animation>().Play();
            Bottomeyelid.GetComponent<Animation>().Play();
            blink = false;
            timer = Random.Range(2, 5);
        }
    }
}
