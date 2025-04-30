using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Glass glass = other.GetComponent<Glass>();

        if (glass != null && glass.LiquidNaCl_filled)
        {
            Debug.Log("Success! Liquid NaCl delivered. Ending game...");
            SceneManager.LoadScene(2);
        }
    }
}
