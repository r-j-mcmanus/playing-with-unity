using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchesScript : MonoBehaviour
{
    public GameObject actionMenuUI = null;
    public GameObject playerDataUI = null;
    public GameObject enemyDataUI = null;
    public GameObject DataUIPanel = null;

    public HealthBarControler playerHealthBarControler = null;
    public HealthBarControler enemyHealthBarControler = null;

    public GameObject damageText = null;

    //public TextFaceScreenScript dmgText = null;

    // Start is called before the first frame update
    void Start()
    {
        actionMenuUI.SetActive(false);
        playerDataUI.SetActive(false);
        enemyDataUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActionMenuActive(bool val)
    {
        actionMenuUI.SetActive(val);
    }

    public void SetPlayerDataActive(bool val)
    {
        playerDataUI.SetActive(val);
        DataUIPanel.SetActive(val | enemyDataUI.activeInHierarchy);
    }

    public void SetEnemyDataActive(bool val)
    {
        enemyDataUI.SetActive(val);
        DataUIPanel.SetActive(val | playerDataUI.activeInHierarchy);
    }

    public void centerUIElementOnObject(GameObject go)
    {
        //Debug.Log(cam.WorldToScreenPoint(target.position));
    }
}
