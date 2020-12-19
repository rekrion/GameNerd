using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject PanelSetting;
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject SettingButton;
    [SerializeField] GameObject ShopButton;
    [SerializeField] GameObject PanelShop;
    [SerializeField] GameObject Background;
    [SerializeField] GameObject PanelMenu;
    [SerializeField] GameObject CloseButton;
    Animator anim;

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

    public void ShopOpen()
    {
        if (PanelShop != null)

        {
            Animator animator = PanelShop.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpenShop = animator.GetBool("openShop");
                animator.SetBool("openShop", !isOpenShop);
            }

        }
    }

    public void GameScene (int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);
    }

    public void Volume(AudioClip clip)
    {


    }    
}
