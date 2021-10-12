using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Banner : MonoBehaviour
{
    //private static Banner _instance;
    //public static Banner Instance { get { return _instance; } }

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOsAdUnitId = "Banner_iOS";
    // Start is called before the first frame update
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    string _adUnitId;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += this.OnLevelLoaded;
        SceneManager.sceneUnloaded += this.OnLevelUnload;
    }

    private void OnLevelUnload(Scene arg0)
    {

    }
    
    static bool startBanner;
    private void OnLevelLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (!PlayerPrefs.HasKey("ads") && arg0.name == "Menu")
        {
             if (Advertisement.isInitialized&& startBanner)
            {
                LoadBanner();
            }

            startBanner = true;
        }
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= this.OnLevelLoaded;
        SceneManager.sceneUnloaded -= this.OnLevelUnload;

        if (startBanner)
        {
            HideBannerAd();
        }
        
    }
    void Awake()
    {
        //    if (_instance != null && _instance != this)
        //    {
        //        Destroy(this.gameObject);
        //        return;

        //    }
        //    else
        //    {
        //        if (PlayerPrefs.HasKey("ads") == true)
        //        {
        //            Destroy(this.gameObject);
        //        }
        //            _instance = this;
        //    }
        Advertisement.Banner.SetPosition(_bannerPosition);
    }
    void Start()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
           ? _iOsAdUnitId
           : _androidAdUnitId;
    }

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(_adUnitId);

        Advertisement.Banner.SetPosition(_bannerPosition);
        startBanner = true;
    }
    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(_adUnitId);
    }
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        // Optionally execute additional code, such as attempting to load another ad.
    }

}
