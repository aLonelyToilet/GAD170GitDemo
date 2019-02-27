using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for converting a battle result into xp to be awarded to the player.
/// 
/// TODO:
///     Respond to battle outcome with xp calculation based on;
///         player win 
///         how strong the win was
///         stats/levels of the dancers involved
///     Award the calculated XP to the player stats
///     Raise the player level up event if needed
/// </summary>
public class XPHandler : MonoBehaviour
{
    int xpRequired = 50;

    private void OnEnable()
    {
        GameEvents.OnBattleConclude += GainXP;
    }

    private void OnDisable()
    {
        GameEvents.OnBattleConclude -= GainXP;
    }

    public void GainXP(BattleResultEventData data)
    {
        Debug.Log(data.outcome);

        Debug.Log(data.player.level);
        Debug.Log(data.player.xp);

        //checking if player won and then assigning xp
        if (data.outcome == 1)
        {
            data.player.xp += 10;
        }

        else
        {
            data.player.xp += 2;
        }

        if (data.player.xp >= xpRequired)
        {
            data.player.level++;
            Debug.Log("Level is now: " + data.player.level);
            int pointsToAdd = 10;
            StatsGenerator.AssignUnusedPoints(data.player, pointsToAdd);
            GameEvents.PlayerLevelUp(data.player.level);
            xpRequired += (xpRequired * (data.player.level / 2));
            Debug.Log(xpRequired);
        }

        // if data.outcome is 1 then player won, otherwise player lost

         
    }
}
