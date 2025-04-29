using UnityEngine;

public class gameEndArea : MonoBehaviour{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Erlenmeyer_flask (1)")){
            Debug.Log("Player entered the end zone!");
            // Add your game end logic here
        }
    }
}
