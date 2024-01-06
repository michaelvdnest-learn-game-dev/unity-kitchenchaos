using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    private const string CUT = "Cut";

    [SerializeReference] CuttingCounter counter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        counter.OnCut += Counter_OnCut;
    }

    private void Counter_OnCut(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
