using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressesGameOver : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        GameManager.instance.ActivateMainMenu();
    }
}
