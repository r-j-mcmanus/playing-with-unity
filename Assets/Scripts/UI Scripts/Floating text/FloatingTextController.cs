using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;

//logic in 
//https://www.youtube.com/watch?v=fbUOG7f3jq8&ab_channel=GameGrind

//change to text mesh pro 
//https://www.youtube.com/watch?v=D33d4w89vTs&ab_channel=Brackeys


public class FloatingTextController : MonoBehaviour
{
    private GameObject popupText = null;
    private RectTransform parent = null; 

    private void Awake()
    {
        //set the parent to the ui group
        parent = GameObject.Find("PopUpCanvas").GetComponent<RectTransform>();
    }

    public void CreateFloatingText(string text, Transform location)
    {
        if (popupText==null)
        {
            popupText = Resources.Load<GameObject>("prefab/PopTextHolder");
        }

        GameObject instance = Instantiate(popupText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        Vector2 randOffset = new Vector2(Random.Range(-30f, 30f), Random.Range(-50f, 50f));

        instance.transform.SetParent(parent, false);
        instance.transform.position = screenPos + randOffset;

        GameObject child = instance.transform.GetChild(0).gameObject;

        child.GetComponent<Text>().text = text;


        Animator anim = child.GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(instance, clipInfo[0].clip.length - 0.1f);
    }
}
