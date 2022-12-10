using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBehaviour : MonoBehaviour
{
    [SerializeReference] private AudioSource _audioSourceIntro;
    [SerializeReference] private AudioSource _audioSourceLoop;
    

    private void Awake()
    {
        if (_audioSourceIntro == null)
            Debug.LogError($"Missing {nameof(_audioSourceIntro)} on {gameObject.name}");
        if (_audioSourceIntro == null)
            Debug.LogError($"Missing {nameof(_audioSourceIntro)} on {gameObject.name}");
        
        _audioSourceIntro.Play();
        _audioSourceLoop.loop = true;
        _audioSourceLoop.PlayDelayed(_audioSourceIntro.clip.length);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
