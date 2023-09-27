using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool _isFeedbackStart;
    private void OnCollisionEnter(Collision collision)
    {
        if (_isFeedbackStart) return;
        if (collision.gameObject.tag == "Hit")
        {
            _isFeedbackStart = true;
            StartCoroutine(StartFeedback());
        }

    }

    private IEnumerator StartFeedback()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }
}
