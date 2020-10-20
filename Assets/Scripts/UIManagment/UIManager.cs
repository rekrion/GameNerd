using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject PanelSetting;
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject SettingButton;
    [SerializeField] GameObject ShopButton;




    public void SettingOpen()
    {
        

        if (PanelSetting != null)

        {
            Animator animator = PanelSetting.GetComponent<Animator>();

            if (animator != null)
            {
                bool isOpenSetting = animator.GetBool("openSetting");
                animator.SetBool("openSetting", !isOpenSetting);
            }

        }

    }
}

