using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.Audio;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text XpText;
    [SerializeField] private Text LevelText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject inventory;
    [SerializeField] private AudioClip menuBtnsound;

    public AudioMixer audio;
    private AudioSource audioSource;

    public void SetVolume(float volume)
    {
        audio.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(XpText);
        Assert.IsNotNull(LevelText);
        Assert.IsNotNull(ScoreText);
        Assert.IsNotNull(menu);
        Assert.IsNotNull(inventory);
        Assert.IsNotNull(menuBtnsound);
    }

    private void Update()
    {
        UpdateScore();
        UpdateLevel();
        UpdateXp();
    }

    public void UpdateScore()
    {
        ScoreText.text = GameManager.Instance.CurrentPlayer.Score1.ToString();
    }

    public void UpdateLevel()
    {
        LevelText.text = GameManager.Instance.CurrentPlayer.Lvl.ToString();
    }

    public void UpdateXp()
    {
        XpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp;
    }

    public void MenubtnSound()
    {
        audioSource.PlayOneShot(menuBtnsound);
        togglemenu();
        if (inventory.activeSelf)
        {
            toggleInventory();
        }
        if (option.activeSelf)
        {
            toggleOption();
        }
    }

    public void InventorybtnSound()
    {
        audioSource.PlayOneShot(menuBtnsound);
        toggleInventory();
    }

    public void toggleOption()
    {
        option.SetActive(!option.activeSelf);
        if (inventory.activeSelf)
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }
    public void toggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
        if (option.activeSelf) {
            option.SetActive(!option.activeSelf);
        }
        
    }
    public void togglemenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
