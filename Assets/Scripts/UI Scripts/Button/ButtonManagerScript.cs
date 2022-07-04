using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagerScript : MonoBehaviour
{
    public Button actionMenuAttackButton = null;

    private bool _actionMenuAttackButtonClicked;
    public bool actionMenuAttackButtonClicked { 
        get { return _actionMenuAttackButtonClicked; } 
    }

    // Start is called before the first frame update
    void Start()
    {
        _actionMenuAttackButtonClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugConfermPress(string buttonName)
    {
        Debug.Log("Button " + buttonName + " clicked");
    }

    public void setActionMenuAttackButtonClicked()
    {
        _actionMenuAttackButtonClicked = true;
    }
}
