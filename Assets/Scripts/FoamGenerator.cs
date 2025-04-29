using UnityEngine;

public class FoamGenerator : MonoBehaviour
{
    public GameObject foamPrefab;
    public bool stopFoam = false;
    public bool createFoam;
    public float spawnDelay;

    private AudioSource BubblingAudio;

    void Start(){
        //so when implimenting in game change spawntime to when its on the burner
        //and when its been set on the burner for some time then it will start bubbeling


        AudioSource[] audios = GetComponents<AudioSource>();
        if (audios.Length >= 1)
        {
            BubblingAudio = audios[0];
            BubblingAudio.loop = true;
        }
    }

    public void startSpawningFoam(){
        InvokeRepeating("SpawnFoam",0.5f,spawnDelay);
    }

    public void SpawnFoam(){
        GameObject foam = Instantiate(foamPrefab,transform.position,Quaternion.identity);
        // Only play if not already playing
        PlayBubbleSound();
        Destroy(foam,2f);
        if(stopFoam){
            CancelInvoke("SpawnFoam");
            if (BubblingAudio != null) BubblingAudio.Stop();
        }
    }

    
        void PlayBubbleSound(){
        if (BubblingAudio != null && !BubblingAudio.isPlaying)
            BubblingAudio.Play();
        }

    
}
