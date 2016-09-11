using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class UnityAnalyticsSimple : MonoBehaviour
{

    int totalPotions = 5;
    int totalCoins = 100;
    string weaponID = "Weapon_102";


    void Start()
    {
        //TEST CUSTOM EVENT ANALYTICS
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
            { "potions", totalPotions
        },
            { "coins", totalCoins },
            { "activeWeapon", weaponID }
          });

        //TEST MONETIZATION
        Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);

        //USER ATTRIBUTES
        Gender gender = Gender.Female;
        Analytics.SetUserGender(gender);

        int birthYear = 2014;
        Analytics.SetUserBirthYear(birthYear);
    }
}
