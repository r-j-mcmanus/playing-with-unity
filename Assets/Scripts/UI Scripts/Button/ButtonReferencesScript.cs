using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReferencesScript : MonoBehaviour
{
    public Button actionMenuAttack = null;

    void Start()
    {
        gameObject.SetActive(false);
    }
}
