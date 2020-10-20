using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenu : StateMachineBehaviour
{
    public GameObject PanelMenu;
    public void OpenShop()
    {
        if (PanelMenu != null)

        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if (animator !=null)
            {
                bool isOpenShop = animator.GetBool("AnimationPanelMenu");
                animator.SetBool("AnimationPanelMenu", !isOpenShop);
            }    
           
        }

    }
}
