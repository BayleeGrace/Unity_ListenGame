using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingChanges : MonoBehaviour
{
    public TMP_Dropdown characterChoiceDropdown;
    public TMP_Dropdown mapChoiceDropdown;

    public void Start()
    {
        GameManager.instance.currentMapInt = 2;
    }

    public void OnCharacterChoiceChange()
    {
        if (characterChoiceDropdown.value == 0)
        {
            GameManager.instance.playerOneIsYokai = true;
        }
        else if (characterChoiceDropdown.value >= 1)
        {
            GameManager.instance.playerOneIsYokai = false;
        }
    }

    public void OnMapChoiceChange()
    {
        if (mapChoiceDropdown.value == 0)
        {
            int index = Random.Range(1,2);
            GameManager.instance.currentMapInt = index;
        }
        else if (mapChoiceDropdown.value == 1)
        {
            GameManager.instance.currentMapInt = 2;
        }
        else if (mapChoiceDropdown.value == 2)
        {
            GameManager.instance.currentMapInt = 1;
        }
    }

    public void OnMultiplayerChange()
    {
        if (GameManager.instance.isMultiplayer == false)
        {
            GameManager.instance.isMultiplayer = true;
        }
        else if (GameManager.instance.isMultiplayer == true)
        {
            GameManager.instance.isMultiplayer = false;
        }
    }

    public void OnTimeLimitChange()
    {

    }
}
