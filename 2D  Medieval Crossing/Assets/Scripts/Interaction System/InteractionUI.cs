using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text actionText;

    public void SetText(string text)
    {
        actionText.text = text;
    }
}
