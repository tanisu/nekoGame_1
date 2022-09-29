using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{

    BannerView _bannerView;
    InterstitialAd _interstitial;

    public static AdmobBanner I;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
    }


    void Start()
    {
        _requestBanner();
        _requestInterstitial();
    }

    public void _interstitialAd()
    {
        if(_interstitial.IsLoaded() == true)
        {
            _interstitial.Show();
        }
        else
        {
            Debug.Log("not yet");
        }
        
    }

    void _requestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1205963622209231/7666006214";
#else
        String adUnitId = "unexpected_platform";
#endif
        _interstitial = new InterstitialAd(adUnitId);
        _interstitial.OnAdLoaded += HandleOnAdLoaded;
        _interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        _interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender,EventArgs args)
    {
        //Debug.Log("success Interstitial");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed Interstitial " + args.LoadAdError);
    }


    public void HandleOnAdClosed(object sender,EventArgs args)
    {
       // Debug.Log("end interstitial");
        _interstitial.Destroy();
        _requestInterstitial();
        //Debug.Log("re interstitial");
    }

    //banner

    void _requestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1205963622209231/6081461809";

#else
        string adUnitId = "unexpected_platform";
#endif



        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        _bannerView.OnAdLoaded += HandleAdLoaded;
        _bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        AdRequest adRequest = new AdRequest.Builder().Build();

        _bannerView.LoadAd(adRequest);

    }

#region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
     //   Debug.Log("success");
    }


    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("failed "+ args.LoadAdError);
    }
#endregion

}
