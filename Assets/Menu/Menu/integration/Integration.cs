using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Integration : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private static Integration _instance;
    public static Integration Instance { get { return _instance; } }

    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    [HideInInspector]
    public bool stopIntegration;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += this.OnLevelLoaded;
    }

    static bool startInteg;
    static int countScen;
    private void OnLevelLoaded(Scene arg0, LoadSceneMode arg1)
    {
        countScen++;
        if (!PlayerPrefs.HasKey("ads") && countScen > 4)
        {
            countScen = 0;
            if (Advertisement.isInitialized && startInteg)
            {
                ShowAd();
            }
            startInteg = true;
        }

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= this.OnLevelLoaded;
    }
    void Awake()
    {

        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;

        //}
        //else
        //{

        //    _instance = this;
        //}



        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
         ? _iOsAdUnitId
         : _androidAdUnitId;

        //Disable button until ad is ready to show
        //   _showAdButton.interactable = false;

    }



    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);

    }

    // Show the loaded content in the Ad Unit: 
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }
    // Implement Load Listener and Show Listener interface methods:  
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        startInteg = true;
        //ShowAd();
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {


    }
}