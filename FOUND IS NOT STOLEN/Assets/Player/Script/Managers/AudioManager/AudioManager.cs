using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;

    [Range(0, 1)]
    public float volume = 0.7f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float volumeRandomFactor = 0.1f;
    [Range(0f, 0.5f)]
    public float pitchRandomFactor = 0.1f;
    

    public void SetAudioSource (AudioSource _audioSource)
    {
        source = _audioSource;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 - Random.Range(-volumeRandomFactor/2, volumeRandomFactor/2));
        source.pitch = pitch * (1 - Random.Range(-pitchRandomFactor/2, pitchRandomFactor / 2));
        source.Play();
    }

}


public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    [SerializeField]
    Sound[] sounds;

    

    private void Awake()
    {    
        if(instance != null)        
            Debug.LogError("More than one audio manager in the scene");
        else
            instance = this;
    }


    private void Start()
    {        
        for (int i = 0; i < sounds.Length; i++)
        {            
            GameObject _audio = new GameObject("Audio_" + i + "_" + sounds[i].name);
            sounds[i].SetAudioSource(_audio.AddComponent<AudioSource>());
            _audio.transform.SetParent(this.transform);
            
        }

       
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i<sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].Play();
                return;
            }

        }

        Debug.LogWarning("Sound " + name + " not found in the list");
    }

}
