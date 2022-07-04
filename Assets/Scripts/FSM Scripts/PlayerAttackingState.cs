using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{ 

    public override void EnterState(PlayerControler_FSM player)
    {
        player.uiSwitches.SetPlayerDataActive(true);

        switch(player.selectedCharacter.GetComponent<CharacterData>().weaponType)
        {
            case "rifle":
                RifleAttack(player);
                break;

            case "melee":
                break;

            case "shotgun":
                ShotgunAttack(player);
                break;

            case "machine gun":
                break;

            case "missile":
                break;

            default:
                throw new System.ArgumentException("Not Valid Weapon Type" + player.selectedCharacter.GetComponent<CharacterData>().weaponType);
        }
    }

    public override void ExitState(PlayerControler_FSM player)
    {
        player.uiSwitches.SetPlayerDataActive(false);
        player.uiSwitches.SetEnemyDataActive(false);
        player.attackedCharacter = null;
    }

    public override void Update(PlayerControler_FSM player)
    {
        
    }

    IEnumerator coroutineWaitToGoToIdleState(float dT, PlayerControler_FSM player)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(dT);
        player.TransitionToState(player.idleState);

    }

    void RifleAttack(PlayerControler_FSM player)
    {
        int damage = Random.Range(0, 100);
        Debug.Log("damage " + damage);
        int bodyPart = player.attackedCharacter.GetComponent<CharacterData>().DamageRandBodyPart(damage);

        float percentDamage = player.attackedCharacter.GetComponent<CharacterData>().HealthFraction(bodyPart);
        Debug.Log("percentDamage " + percentDamage);

        player.uiSwitches.enemyHealthBarControler.AnimateDamageBarsGoTo(bodyPart, percentDamage);

        player.CallStartCoroutine(player.camControler.GoToAttackingCamera(player.selectedCharacter.transform, player.attackedCharacter.transform, 1.5f));

        player.floatingTextControler.CreateFloatingText(damage.ToString(), player.attackedCharacter.transform);

        player.CallStartCoroutine(coroutineWaitToGoToIdleState(1.5f, player));
    }

    void ShotgunAttack(PlayerControler_FSM player)
    {
        int[] damageArray = new int[12];
        for (int i = 0; i < damageArray.Length; i++)
        {
            damageArray[i] = Random.Range(0, 10);
            player.attackedCharacter.GetComponent<CharacterData>().DamageRandBodyPart(damageArray[i]);
        }


        float percentDamage = 0f;
        for(int bodypart = 0; bodypart < 4; bodypart++)
        {
            percentDamage = player.attackedCharacter.GetComponent<CharacterData>().HealthFraction(bodypart);
            player.uiSwitches.enemyHealthBarControler.AnimateDamageBarsGoTo(bodypart, percentDamage);
        }

        player.CallStartCoroutine(player.camControler.GoToAttackingCamera(player.selectedCharacter.transform, player.attackedCharacter.transform, 1.5f));

        for (int i = 0; i < damageArray.Length; i++)
        {
            player.floatingTextControler.CreateFloatingText(damageArray[i].ToString(), player.attackedCharacter.transform);
        }

        player.CallStartCoroutine(coroutineWaitToGoToIdleState(1.5f, player));
    }
}
