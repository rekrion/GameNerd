using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHintPanel : MonoBehaviour
{
    [SerializeField] Text[] texts;
    [SerializeField] Button button;

    public void UpdateData(Hint[] hints)
    {
        for(int i = 0; i < hints.Length; i++)
        {
            if (hints[i].issued)
                texts[i].text = hints[i].text;
            else
                texts[i].text = "";
        }
    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
