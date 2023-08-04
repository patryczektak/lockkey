using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class ChestManager : MonoBehaviour
{
    private ChangeValueAnimator changeStar;

    public int small;
    public int mid;
    public int big;
    public int price;


    public GameObject sBox;
    public GameObject mBox;
    public GameObject bBox;
    public GameObject sChest;
    public GameObject mChest;
    public GameObject bChest;
    public GameObject Open;
    public GameObject tooExpensive;

    public TextMeshProUGUI valueText;

    public PlayableDirector timelineBack;
    public PlayableDirector timelineShow;

    // Start is called before the first frame update
    void Start()
    {
        smallBox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void smallBox()
    {
        price = small;

        sBox.SetActive(true);
        mBox.SetActive(false);
        bBox.SetActive(false);

        sChest.SetActive(true);
        mChest.SetActive(false);
        bChest.SetActive(false);
        Open.SetActive(false);

        valueText.text = small.ToString();

        tooExpensive.SetActive(false);
        if (small > PlayerPrefs.GetInt("star"))
        {
            tooExpensive.SetActive(true);
        }
    }

    public void midBox()
    {
        price = mid;

        sBox.SetActive(false);
        mBox.SetActive(true);
        bBox.SetActive(false);

        sChest.SetActive(false);
        mChest.SetActive(true);
        bChest.SetActive(false);
        Open.SetActive(false);

        valueText.text = mid.ToString();

        tooExpensive.SetActive(false);
        if (mid > PlayerPrefs.GetInt("star"))
        {
            tooExpensive.SetActive(true);
        }
    }

    public void bigBox()
    {
        price = big;

        sBox.SetActive(false);
        mBox.SetActive(false);
        bBox.SetActive(true);

        sChest.SetActive(false);
        mChest.SetActive(false);
        bChest.SetActive(true);
        Open.SetActive(false);

        valueText.text = big.ToString();

        tooExpensive.SetActive(false);
        if (big > PlayerPrefs.GetInt("star"))
        {
            tooExpensive.SetActive(true);
        }
    }

    public void BuyTresure()
    {
        if (price <= PlayerPrefs.GetInt("star"))
        {
            changeStar = GameObject.FindObjectOfType<ChangeValueAnimator>();
            changeStar.ChangeValueDown(price);

            sChest.SetActive(false);
            mChest.SetActive(false);
            bChest.SetActive(false);
            Open.SetActive(true);

            if (price > PlayerPrefs.GetInt("star"))
            {
                tooExpensive.SetActive(true);
            }
        }        
    }

    public void Show()
    {
        timelineShow.Stop();
        timelineShow.time = 0f;
        timelineShow.Play();
    }

    public void Hide()
    {
        timelineBack.Stop();
        timelineBack.time = 0f;
        timelineBack.Play();
    }
}
