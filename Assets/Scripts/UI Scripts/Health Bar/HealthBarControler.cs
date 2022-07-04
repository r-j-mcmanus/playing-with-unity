using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControler : MonoBehaviour
{
    private GameObject bodyHealthBar = null;
    private GameObject lArmHealthBar = null;
    private GameObject rArmHealthBar = null;
    private GameObject legsHealthBar = null;

    // Start is called before the first frame update
    void Awake()
    {
        bodyHealthBar = gameObject.transform.GetChild(0).gameObject;
        lArmHealthBar = gameObject.transform.GetChild(1).gameObject;
        rArmHealthBar = gameObject.transform.GetChild(2).gameObject;
        legsHealthBar = gameObject.transform.GetChild(3).gameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthBars(CharacterData cd)
    {
        SetHealthBarValue(bodyHealthBar, cd.HealthFraction(BodyPart.body));
        SetHealthBarValue(lArmHealthBar, cd.HealthFraction(BodyPart.lArm));
        SetHealthBarValue(rArmHealthBar, cd.HealthFraction(BodyPart.rArm));
        SetHealthBarValue(legsHealthBar, cd.HealthFraction(BodyPart.legs));
    }

    private void SetHealthBarValue(GameObject healthBar, float healthFrac)
    {
        healthFrac = Mathf.Clamp(healthFrac, 0.0f, 1.0f);

        healthBar.GetComponent<Image>().fillAmount = healthFrac;
    }

    public void AnimateDamageBarsGoTo(int bodyPart, float percentDamage)
    {
        percentDamage = 1 - percentDamage;

        switch (bodyPart)
        {
            case 0:
                StartCoroutine(CoroutineAnimateDamageBars(bodyHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 1:
                StartCoroutine(CoroutineAnimateDamageBars(lArmHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 2:
                StartCoroutine(CoroutineAnimateDamageBars(rArmHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 3:
                StartCoroutine(CoroutineAnimateDamageBars(legsHealthBar.GetComponent<Image>(), percentDamage));
                break;
            default:
                Debug.Log("bodyPart " + bodyPart + " not a real body part in AnimateDamageBars");
                break;
        }
    }

    public void AnimateDamageBarsSubtract(int bodyPart, float percentDamage)
    {
        switch (bodyPart)
        {
            case 0:
                StartCoroutine(CoroutineAnimateDamageBars(bodyHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 1:
                StartCoroutine(CoroutineAnimateDamageBars(lArmHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 2:
                StartCoroutine(CoroutineAnimateDamageBars(rArmHealthBar.GetComponent<Image>(), percentDamage));
                break;
            case 3:
                StartCoroutine(CoroutineAnimateDamageBars(legsHealthBar.GetComponent<Image>(), percentDamage));
                break;
            default:
                Debug.Log("bodyPart " + bodyPart + " not a real body part in AnimateDamageBars");
                break;
        }
    }

    public IEnumerator CoroutineAnimateDamageBars(Image healthBar, float percentDamage)
    {
        percentDamage = Mathf.Clamp(percentDamage, 0, healthBar.fillAmount);
        //Debug.Log("healthBar.fillAmount " + healthBar.fillAmount);
        //Debug.Log("percentDamage " + percentDamage);
        float percentRate = 2.0f;
        while (percentDamage > percentRate * Time.deltaTime)
        {
            healthBar.GetComponent<Image>().fillAmount -= percentRate * Time.deltaTime;
            //Debug.Log("fill amount " + healthBar.GetComponent<Image>().fillAmount);
            percentDamage -= percentRate * Time.deltaTime;
            yield return 0;
        }
        healthBar.GetComponent<Image>().fillAmount -= Mathf.Max(percentDamage,0);
    }
}
