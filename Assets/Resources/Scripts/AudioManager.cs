using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false, playOnAwake = false;

    [Range(1, 10)]
    public int players = 1;

    private AudioSource[] _sources;

    public void SetSource(int _index, AudioSource _source)
    {
        _sources[_index] = _source;
        _sources[_index].clip = clip;
        _sources[_index].loop = loop;
        if (playOnAwake) this.Play();
    }

    public void Play()
    {
        for (int i = 0; i < _sources.Length; i++)
        {
            if (!_sources[i].isPlaying)
            {
                _sources[i].volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
                _sources[i].pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
                _sources[i].Play();
                return;
            }
        }

    }



    public AudioSource[] sources
    {
        get { return _sources; }
        set { _sources = value; }
    }


}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].sources = new AudioSource[sounds[i].players];
            for (int j = 0; j < sounds[i].sources.Length; j++)
            {
                //Debug.Log("_sources.Length = " + sounds[i].sources.Length + ", _index = " + j);
                GameObject _go = new GameObject("Sound_" + i + ", " + j + "_" + sounds[i].name);
                _go.transform.SetParent(this.transform);
                sounds[i].SetSource(j, _go.AddComponent<AudioSource>());
            }
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

}
