using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizerUI : MonoBehaviour
{
    TextMeshProUGUI textField;
    public LocalizedString localizedString;
    // Start is called before the first frame update
    void Start()
    {
        CSVLocalized();
    }


    private void CSVLocalized()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = localizedString.value;
    }
    private void JsonLocalized()
    {
        //textField = GetComponent<TextMeshProUGUI>();
        //string value = LocalizationManager.instance.GetLocalizedValue(key);
        //textField.text = value;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
