using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerControler_FSM player)
    {
        player.selectedCharacter = null;
    }

    public override void ExitState(PlayerControler_FSM player)
    {
        player.uiSwitches.SetPlayerDataActive(false);
        player.uiSwitches.SetEnemyDataActive(false);
    }

    public override void Update(PlayerControler_FSM player)
    {
        player.mouseOverRayCast.CheckMouseOver();

        player.uiSwitches.SetEnemyDataActive(false);
        player.uiSwitches.SetPlayerDataActive(false);
        bool mouseOverPlayerCharacter = false;


        if (player.mouseOverRayCast.mouseOverObject != null)
        {
            foreach (GameObject ec in player.enemyCharacters)
            {
                if (GameObject.ReferenceEquals(ec, player.mouseOverRayCast.mouseOverObject))
                {
                    player.uiSwitches.enemyHealthBarControler.SetHealthBars((CharacterData)ec.GetComponent<CharacterData>());
                    player.uiSwitches.SetEnemyDataActive(true);

                    break;
                }
            }

            foreach (GameObject pc in player.playerCharacters)
            {
                if (GameObject.ReferenceEquals(pc, player.mouseOverRayCast.mouseOverObject))
                {
                    player.uiSwitches.playerHealthBarControler.SetHealthBars((CharacterData)pc.GetComponent<CharacterData>());
                    player.uiSwitches.SetPlayerDataActive(true);

                    mouseOverPlayerCharacter = true;

                    break;
                }
            }
        }


        if (mouseOverPlayerCharacter && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("should be changing state!!");
            player.selectedCharacter = player.mouseOverRayCast.mouseOverObject;
            player.selectedCharacter.GetComponent<CharacterData>().RecordTurnStartPosition();
            player.TransitionToState(player.playerSelectedState);
            return;
        }
    }
}
