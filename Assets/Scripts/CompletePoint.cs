using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletePoint : MonoBehaviour
{
    private bool m_IsComplete = false;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player") && !m_IsComplete)
            StageComplete();    
    }

    private void StageComplete()
    {
        m_IsComplete = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
