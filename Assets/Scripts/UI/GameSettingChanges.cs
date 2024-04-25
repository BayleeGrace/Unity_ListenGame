using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingChanges : MonoBehaviour
{
    public TMP_Dropdown characterChoiceDropdown;

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

    public void OnMultiplayerChange()
    {

    }

    public void OnTimeLimitChange()
    {

    }
}
