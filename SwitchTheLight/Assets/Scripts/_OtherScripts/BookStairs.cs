using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStairs : MonoBehaviour
{

    private Collider[] children;

    [SerializeField]
    private float _DissolveAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Collider>();

        foreach (Collider item in children)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_DissolveAmount", _DissolveAmount);

        AppearSlowly();
    }


    void AppearSlowly()
    {
        _DissolveAmount = Mathf.Clamp(_DissolveAmount - 0.1f * Time.deltaTime, 0, 1);

        if (_DissolveAmount == 0)
        {
            foreach (Collider item in children)
            {
                item.enabled = true;
            }
        }
    }

}
