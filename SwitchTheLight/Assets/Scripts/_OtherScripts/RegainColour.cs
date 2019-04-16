using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegainColour : MonoBehaviour
{

    public float _radius, _scaleFactor;

    // Start is called before the first frame update
    void Start()
    {

        Mathf.Clamp(_radius, 0, 100);

    }

    // Update is called once per frame
    void Update()
    {
        ExpandColourRadius();
    }

    void ExpandColourRadius()
    {
        Shader.SetGlobalFloat("Mask_Radius", _radius);

        if (_radius != 100)
        {
            _radius += _scaleFactor * Time.deltaTime;
        }
    }
}
