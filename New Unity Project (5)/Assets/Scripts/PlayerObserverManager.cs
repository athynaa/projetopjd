using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerObserverManager
{
     public static Action<int> OnCoinsChanged;
     public static Action<int> OncapsuleChanged;

     public static void CoinsChanged(int value)
     {
          OnCoinsChanged?.Invoke(value);
     }

     public static void capsuleChanged(int value)
     {
          OncapsuleChanged?.Invoke(value);
     }
     
}

   
   
