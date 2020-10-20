using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public GameObject PanelMenu;

    public void OpenSetting()
    {
        if (PanelMenu != null)

        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpenSetting = animator.GetBool("openSetting");
                animator.SetBool("openSetting", !isOpenSetting);
            }

        }
    }
}