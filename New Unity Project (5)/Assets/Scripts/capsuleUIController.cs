using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class capsuleUIController : MonoBehaviour
{
  [SerializeField] private TMP_Text capsuleText;

  private void OnEnable()
  {
    PlayerObserverManager.OncapsuleChanged += UpdatecapsuleText;
  }

  private void OnDisable()
  {
    PlayerObserverManager.OncapsuleChanged -= UpdatecapsuleText;
  }

  private void UpdatecapsuleText(int newcapsuleValue)
  {
    capsuleText.text = newcapsuleValue.ToString();
  }
}
