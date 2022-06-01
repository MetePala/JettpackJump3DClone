using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeComponent : MonoBehaviour
{
    [Header("Total-Gold")]
    [SerializeField] Text _totalGoldText;
    public static int _totalGold=200;

    [SerializeField] GameObject _startPanel;
    [SerializeField] Button _jumpBoostButton, _jettpackBoostButton;
    [SerializeField] Text _jumpBoostPriceText, _jettpackBoostPriceText;
    [SerializeField]int _jumpBoostPrice=150, _jettpackBoostPrice=150;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("jumpPrice");
        //PlayerPrefs.DeleteKey("jettpackPrice");
        //PlayerPrefs.DeleteKey("jumpSpeed");
        //PlayerPrefs.DeleteKey("jettpackSpeed");


        if (PlayerPrefs.GetInt("jumpPrice") == 0)
        {
            PlayerPrefs.SetInt("jumpPrice", _jumpBoostPrice);
            PlayerPrefs.SetInt("jettpackPrice", _jettpackBoostPrice);
            PlayerPrefs.SetFloat("jumpSpeed", 450);
            PlayerPrefs.SetFloat("jettpackSpeed", 7f);
        }

        PlayerController._jumpSpeed = PlayerPrefs.GetFloat("jumpSpeed");
        PlayerController._jettPackSpeed = PlayerPrefs.GetFloat("jettpackSpeed");

        _jumpBoostPrice = PlayerPrefs.GetInt("jumpPrice");
        _jettpackBoostPrice = PlayerPrefs.GetInt("jettpackPrice");
        _jumpBoostPriceText.text = _jumpBoostPrice.ToString();
        _jettpackBoostPriceText.text = _jettpackBoostPrice.ToString();


        _startPanel.SetActive(true);

        _totalGold = PlayerPrefs.GetInt("totalgold");

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _startPanel.SetActive(false);
        }
        Buttonn();
    }

    public void JumpBoost()
    {
        PlayerPrefs.SetFloat("jumpSpeed", (PlayerController._jumpSpeed+25));
        PlayerController._jumpSpeed += 25;

        _totalGold -= _jumpBoostPrice;
        _jumpBoostPrice += 125;
        PlayerPrefs.SetInt("jumpPrice", _jumpBoostPrice);
        _jumpBoostPriceText.text = _jumpBoostPrice.ToString();
        _totalGoldText.text = "Gold: " + _totalGold.ToString();
        Buttonn();
    }
    public void JettpackBoost()
    {
        PlayerPrefs.SetFloat("jettpackSpeed", (PlayerController._jettPackSpeed + 0.5f));
        PlayerController._jettPackSpeed += 0.5f;

        _totalGold -= _jettpackBoostPrice;
        _jettpackBoostPrice += 150;
        PlayerPrefs.SetInt("jettpackPrice", _jettpackBoostPrice);
        _jettpackBoostPriceText.text = _jettpackBoostPrice.ToString();
        _totalGoldText.text = "Gold: " + _totalGold.ToString();
        Buttonn();
    }
    void Buttonn()
    {
        PlayerPrefs.SetInt("totalgold", _totalGold);
        _totalGoldText.text = "Gold: "+_totalGold.ToString();

        if (_totalGold >= _jettpackBoostPrice)
        {
            _jettpackBoostButton.interactable = true;
        }
        else
            _jettpackBoostButton.interactable = false;

        if (_totalGold >= _jumpBoostPrice)
        {
            _jumpBoostButton.interactable = true;
        }
        else
            _jumpBoostButton.interactable = false;
    }
}
