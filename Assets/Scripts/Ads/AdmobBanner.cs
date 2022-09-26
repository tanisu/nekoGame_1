using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{

    BannerView _bannerView;

    void Start()
    {
        _requestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Top);

        _bannerView.OnAdLoaded += HandleAdLoaded;
        _bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        AdRequest adRequest = new AdRequest.Builder().Build();

        _bannerView.LoadAd(adRequest);

    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("success");
    }


    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("failed "+ args.LoadAdError);
    }
    #endregion

}
