using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image HealthBarImage;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    public void SetHealthBarValue(float value)
    {
        value=Mathf.Clamp(value,0.0f,1.0f);

        HealthBarImage.fillAmount = value;
    }
}
