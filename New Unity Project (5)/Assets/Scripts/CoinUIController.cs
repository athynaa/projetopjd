using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIController : MonoBehaviour
{
   [SerializeField] private TMP_Text coinText;

   private void OnEnable()
   {
      PlayerObserverManager.OnCoinsChanged += UpddateCoinText;
   }

   private void OnDisable()
   {
      PlayerObserverManager.OnCoinsChanged -= UpddateCoinText;
   }

   private void UpddateCoinText(int newCoinsValue)
   {
      coinText.text = newCoinsValue.ToString();
   }
}
