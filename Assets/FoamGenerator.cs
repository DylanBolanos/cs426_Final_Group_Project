using UnityEngine;

public class FoamGenerator : MonoBehaviour
{
    public GameObject foamPrefab;
    public bool stopFoam = false;
    public float spawnTime;
    public float spawnDelay;

    void Start(){
        //so when implimenting in game change spawntime to when its on the burner
        //and when its been set on the burner for some time then it will start bubbeling
        InvokeRepeating("SpawnFoam",spawnTime,spawnDelay);
    }

    public void SpawnFoam(){
        GameObject foam = Instantiate(foamPrefab,transform.position,Quaternion.identity);
        Destroy(foam,3f);
        if(stopFoam){
            CancelInvoke("SpawnFoam");
        }
    }

    
}
