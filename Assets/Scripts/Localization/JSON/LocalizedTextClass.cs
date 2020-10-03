using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextClass : MonoBehaviour
{
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textField = GetComponent<TextMeshProUGUI>();
        textField.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
