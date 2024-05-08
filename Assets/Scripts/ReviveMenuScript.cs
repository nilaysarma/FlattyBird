using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveMenuScript : MonoBehaviour
{
    public LogicScript logic;

    public void CloseReviveMenu()
    {
        logic.CloseReviveMenu();
    }
}
