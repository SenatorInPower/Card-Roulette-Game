using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing; //���������� � ���������, ����� �������� ����� ���������� �������
using UnityEngine.SceneManagement;
public class IAPCore : MonoBehaviour, IStoreListener //��� ��������� ��������� �� Unity Purchasing
{
   

    //private static IAPCore _instance;
    //public static IAPCore Instance { get { return _instance; } }

    private IStoreController m_StoreController;          //������ � ������� Unity Purchasing
    private IExtensionProvider m_StoreExtensionProvider; // ���������� ������� ��� ���������� ���������

    public static string noads = "noads"; //����������� - nonconsumable
                                          //  public static string vip = "vip"; //����������� - nonconsumable ��� ����� ���� ��������
    public static string coin3000 = "3000coin"; //������������ - consumable
    public static string coin5000 = "5000coin"; //������������ - consumable
    public static string coin10000 = "10000coin"; //������������ - consumable
    public string kProductNameAppleSubscription = "app";
    public string kProductNameGooglePlaySubscription = "play";

    private string _gameId = "4256493"; //��� game id
    private string _banner = "Banner_Android";


    string _androidAdUnitId = "Banner_Android";
    string _iOsAdUnitId = "Banner_iOS";
    string _adUnitId;
    int coin;

    private void OnEnable()
    {
       
        SceneManager.sceneLoaded += this.OnLevelLoaded;
        SceneManager.sceneUnloaded += this.OnLevelUnload;
    }

    

    private void OnDisable()
    {
       
        SceneManager.sceneLoaded -= this.OnLevelLoaded;
        SceneManager.sceneUnloaded -= this.OnLevelUnload;
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll(); 

        if (m_StoreController == null) //���� ��� �� ���������������� ������� Unity Purchasing, ����� ��������������
        {
            InitializePurchasing();
        }

    }
    public void Clear()
    {
        PlayerPrefs.DeleteAll();

    }



    public void InitializePurchasing()
    {
        if (IsInitialized()) //���� ��� ���������� � ������� - ������� �� �������
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(noads, ProductType.NonConsumable);
        //   builder.AddProduct(vip, ProductType.NonConsumable); //��� ProductType.Subscription
        builder.AddProduct(coin3000, ProductType.Consumable);
        builder.AddProduct(coin5000, ProductType.Consumable);
        builder.AddProduct(coin10000, ProductType.Consumable);

        //builder.AddProduct(noads, ProductType.NonConsumable, new IDs(){
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });

        //builder.AddProduct(coin100, ProductType.Consumable, new IDs(){
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });

        //builder.AddProduct(coin300, ProductType.Consumable, new IDs(){
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });

        //builder.AddProduct(coin1000, ProductType.Consumable, new IDs(){
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });
        UnityPurchasing.Initialize(this, builder);
    }

    //public  void Buy_noads()
    //  {
    //      PlayerPrefs.SetInt("ads", 0);
    //      initADS.banner.HideBannerAd();
    //      BuyProductID(noads);
    //  }
    public void Buy_product(string product)
    {

        BuyProductID(product.ToString());
        //    BuyProductID(vip);
    }

    //public void Buy_vip()
    //{

    ////    BuyProductID(vip);
    //}

    //public void Buy_coins100()
    //{
    //    BuyProductID(coin100);
    //}
    //public void Buy_coins300()
    //{
    //    BuyProductID(coin300);
    //}
    //public void Buy_coins1000()
    //{
    //    BuyProductID(coin1000);
    //}
    void BuyProductID(string productId)
    {
        if (IsInitialized()) //���� ������� ���������������� 
        {
            Product product = m_StoreController.products.WithID(productId); //������� ������� ������� 

            if (product != null && product.availableToPurchase) //���� ������� ������ � ����� ��� �������
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product); //��������
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    IEnumerator textMoneyScaler(TextMeshProUGUI text)
    {
        if (text)
        {
            text.gameObject.SetActive(true);
            float TimeScale = 0;
            float scaleer = 1;
            int revers = 1;
            while (true)
            {
                scaleer += Time.deltaTime / 3 * revers;
                TimeScale += Time.deltaTime;
                text.transform.localScale = scaleer * Vector3.one;
                if (scaleer > 1.1f)
                {
                    revers = -1;
                }
                else if (scaleer < 1)
                {
                    revers = 1;

                }
                if (TimeScale > 2)
                {
                    text.transform.localScale = Vector3.one;
                    if (SceneManager.GetActiveScene().name == "Ruletka")
                    {


                    }
                    else
                    {
                        text.gameObject.SetActive(false);

                    }
                    yield break;
                }

                yield return null;
            }
        }
        else
        {
            RaitLogic.Info();
            yield break;
        }
    }
    public void addcoin(int coins)
    {
        TotalMoney.TotalMoneys.Money = coins + PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", TotalMoney.TotalMoneys.Money);

        TotalMoney.TotalMoneys.Info();
        StartCoroutine(textMoneyScaler(TotalMoney.TotalMoneys.ReturnMoneyText()));
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        ProductPurchased(purchaseEvent.purchasedProduct.definition.id);

        return PurchaseProcessingResult.Complete;
    }
    public void ProductPurchased(string purchaseEvent)
    {

        if (String.Equals(purchaseEvent, noads, StringComparison.Ordinal)) //��� �������� ��� ID
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent));

            //�������� ��� �������
            if (PlayerPrefs.HasKey("ads") == false)
            {
                PlayerPrefs.SetInt("ads", 0);
                AudioEvent.AudioEvents.AudioPlay(AudioAction.Win);

                //   AdsCore..HideBanner();
                //    AdsCore.S.StopAllCoroutines();
            }
            //else if (PlayerPrefs.GetInt("ads") == 0)
            //{
            //    panelAds.SetActive(false);
            //    panelAds_Done.SetActive(true);
            //    if (Advertisement.isInitialized)
            //        Advertisement.Banner.Hide();
            //}

        }
        //else if (String.Equals(args.purchasedProduct.definition.id, vip, StringComparison.Ordinal))
        //{
        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        //    //�������� ��� �������
        //    if (PlayerPrefs.HasKey("vip") == false)
        //    {
        //        PlayerPrefs.SetInt("vip", 0);
        //        panelVIP.SetActive(false);
        //        panelVIP_Done.SetActive(true);

        //    }
        //    else if (PlayerPrefs.GetInt("vip") == 0)
        //    {
        //        panelVIP.SetActive(false);
        //        panelVIP_Done.SetActive(true);

        //    }
        //}
        else if (String.Equals(purchaseEvent, coin3000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent));
            addcoin(3000);
            AudioEvent.AudioEvents.AudioPlay(AudioAction.Purshes);

            //�������� ��� �������
            //  GameLogic.S.IncrementPoint2AfterAds(151);
        }
        else if (String.Equals(purchaseEvent, coin5000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent));

            //�������� ��� �������
            //  GameLogic.S.IncrementPoint2AfterAds(151);
            addcoin(5000);
            AudioEvent.AudioEvents.AudioPlay(AudioAction.Purshes);

        }
        else if (String.Equals(purchaseEvent, coin10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent));

            //�������� ��� �������
            //  GameLogic.S.IncrementPoint2AfterAds(151);
            addcoin(10000);
            AudioEvent.AudioEvents.AudioPlay(AudioAction.Purshes);

        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", purchaseEvent));
        }


    }

    public void RestorePurchases() //�������������� ������� (������ ��� Apple). � ���� ��� �������������� �������.
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer) //���� ��������� �� ��� ����������
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }
    private void Awake()
    {
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //else
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(gameObject);
        //    SceneManager.sceneLoaded += this.OnLevelLoaded;
        //}

    }
    private void OnLevelUnload(Scene scene)
    {

    }

    static bool first = true;
    private void OnLevelLoaded(Scene scene, LoadSceneMode sceneMode)
    {

        //if (first)
        //{
        //    first = false;
        //}
        //else
        //{

        //    if (!PlayerPrefs.HasKey("ads"))
        //    {

        //        initADS.integration.LoadAd();

        //        initADS.banner.LoadBanner();

        //    }




        //if (PlayerPrefs.HasKey("ads") == true)
        //{

        //if (initADS.NoAdsInit)
        //{
        //    if (SceneManager.GetActiveScene().name == "Menu")
        //    {
        //        initADS.banner.LoadBanner();
        //        initADS.integration.LoadAd();
        //    }
        //    else
        //    {
        //        initADS.banner.HideBannerAd();
        //    }

        //}
        //}
        //else
        //{
        //    if (SceneManager.GetActiveScene().name == "Menu")
        //    {
        //        banner.LoadBanner();
        //        integration.LoadAd();
        //    }
        //    else
        //    {
        //        banner.HideBannerAd();
        //    }
        //}
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

}
