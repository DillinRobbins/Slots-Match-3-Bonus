using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour
{
    public void ResetButtonSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
