using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RecipeSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeName;
    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeName.text = recipeSO.recipeName;
        
    }
}