using UnityEngine;
using UnityEngine.Advertisements;

public class Init : MonoBehaviour, IUnityAdsInitializationListener
{
    public Integration integration;
    internal bool NoAdsInit;
    private static Init _instance;
    public static Init Instance { get { return _instance; } }

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("ads"))
        {
            NoAdsInit = true;
        }
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //else
        //{
        //    _instance = this;
        //}

        InitializeAds();
    }

    public void InitializeAds()
    {
        if (PlayerPrefs.HasKey("ads") == false)
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
            if (Advertisement.isInitialized == false)
                Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
        }

    }
    public void OnInitializationComplete()
    {

               integration.LoadAd();

    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}