using UnityEngine;
using System.Collections;

public class ExplosionSystem : MonoBehaviour {
	private ParticleSystem emitter;
	// Use this for initialization
	void Start () {
		emitter = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {/*
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[emitter.particleCount];
		emitter.GetParticles (particles);
		foreach(ParticleSystem.Particle p in particles) {
			p.lifetime = .
		}
		*/
		if (emitter.particleCount == 0) {
			Destroy (gameObject);
		}
	}
}
