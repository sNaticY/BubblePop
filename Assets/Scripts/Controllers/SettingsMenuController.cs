using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _vibrationButton;
    [SerializeField] private Button _confirmButton;

    [SerializeField] private GameObject _soundToggleOn;
    [SerializeField] private GameObject _soundToggleOff;

    [SerializeField] private GameObject _vibrationToggleOn;
    [SerializeField] private GameObject _vibrationToggleOff;

    private bool _isSoundEnable = true;
    private bool _isVibrationEnable = true;

    public void Initialize()
    {
        _isSoundEnable = !PlayerPrefs.HasKey("SoundEnable") || PlayerPrefs.HasKey("SoundEnable") && PlayerPrefs.GetInt("SoundEnable") == 1;
        _isVibrationEnable = !PlayerPrefs.HasKey("VibrationEnable") || PlayerPrefs.HasKey("VibrationEnable") && PlayerPrefs.GetInt("VibrationEnable") == 1;
        
        SetSoundEnable(_isSoundEnable);
        SetVibrationEnable(_isVibrationEnable);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _soundButton.onClick.AddListener(OnSoundButtonClicked);
        _vibrationButton.onClick.AddListener(OnVibrationButtonClicked);
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    private void OnConfirmButtonClicked()
    {
        gameObject.SetActive(false);
        PlayerPrefs.Save();
    }

    private void OnVibrationButtonClicked()
    {
        _isVibrationEnable = !_isVibrationEnable;
        SetVibrationEnable(_isVibrationEnable);
        
    }

    private void OnSoundButtonClicked()
    {
        _isSoundEnable = !_isSoundEnable;
        SetSoundEnable(_isSoundEnable);
    }

    private void SetSoundEnable(bool enable)
    {
        _soundToggleOn.SetActive(enable);
        _soundToggleOff.SetActive(!enable);
        Contexts.sharedInstance.input.isSoundOn = enable;
        PlayerPrefs.SetInt("SoundEnable", enable ? 1 : 0);
    }

    private void SetVibrationEnable(bool enable)
    {
        _vibrationToggleOn.SetActive(enable);
        _vibrationToggleOff.SetActive(!enable);
        Contexts.sharedInstance.input.isVibrationOn = enable;
        PlayerPrefs.SetInt("VibrationEnable", enable ? 1 : 0);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        _soundButton.onClick.RemoveAllListeners();
        _vibrationButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.RemoveAllListeners();
    }
}
