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

    public bool loop = false, playOnAwake = false, isMusic = false;

    [Range(1, 10)]
    public int players = 1;

    private AudioSource[] _sources;

    public void SetSource(int _index, AudioSource _source)
    {
        _sources[_index] = _source;
        _sources[_index].clip = clip;
        _sources[_index].loop = loop;
        if (playOnAwake)
        {
            if ((this.isMusic && (PlayerPrefs.GetInt("Music") == 1)) || (!this.isMusic && (PlayerPrefs.GetInt("Sound") == 1)))
            {
                this.Play();
            }
        }
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
    Sound music;

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
            if (sounds[i].isMusic) music = sounds[i];
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
                if ((sounds[i].isMusic && (PlayerPrefs.GetInt("Music") == 1)) || (!sounds[i].isMusic && (PlayerPrefs.GetInt("Sound") == 1)))
                {
                    sounds[i].Play();
                    return;
                }
            }
        }
    }

    public void ToogleMusic()
    {
        if (music.sources[0].isPlaying)
        {
            music.sources[0].Pause();
        }
        else
        {
            music.sources[0].Play();
        }
    }

}
