using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator anim;
    private readonly string IS_FLASHING = "IsFlashing";
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        anim.SetBool(IS_FLASHING, show);

       
    }

  
}
