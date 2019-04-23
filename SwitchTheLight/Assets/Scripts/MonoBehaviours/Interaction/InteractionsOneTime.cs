using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsOneTime : MonoBehaviour
{


    public bool Once = true;

    [SerializeField]
    private ReactionCollection reaction;

    private CanvasGroup Panel;

    private void Awake()
    {
        Panel = FindObjectOfType<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Once)
        {
            Panel.blocksRaycasts = true;
            reaction.React();
            Once = false;
            Panel.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
    }
}
