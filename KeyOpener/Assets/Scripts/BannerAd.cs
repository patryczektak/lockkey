using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

//Banner ad
public class BannerAd : MonoBehaviour
{
    private BannerView adBanner;

    private string idApp, idBanner;


    void Start()
    {
        //idApp = "ca-app-pub-3935654686224415~1440199166";
        //idBanner = "ca-app-pub-3935654686224415/4560308219";

        //MobileAds.Initialize(initStatus => { });

        //RequestBannerAd();
    }

    public void LoadAfter()
    {
        idApp = "ca-app-pub-3935654686224415~1440199166";
        idBanner = "ca-app-pub-3935654686224415/4560308219";

        MobileAds.Initialize(initStatus => { });

        RequestBannerAd();
    }

    #region Banner Methods --------------------------------------------------

    public void RequestBannerAd()
    {
        adBanner = new BannerView(idBanner, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        adBanner.LoadAd(request);
    }

    public void DestroyBannerAd()
    {
        //if (adBanner != null)
        //    adBanner.Destroy();
    }

    #endregion


    //------------------------------------------------------------------------

    void OnDestroy()
    {
        //DestroyBannerAd();
    }

}
