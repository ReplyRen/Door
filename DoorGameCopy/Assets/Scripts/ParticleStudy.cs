using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleStudy : MonoBehaviour
{
    private ParticleSystem m_System;
    ParticleSystem.Particle[] m_Paritcles;
    public float m_Drift = 0.01f;

    private void Awake()
    {
        if (m_System == null) m_System = GetComponent<ParticleSystem>();
        if (m_Paritcles == null || m_Paritcles.Length < m_System.main.maxParticles)
            m_Paritcles = new ParticleSystem.Particle[m_System.main.maxParticles];
        var m_Main = m_System.main;
        m_Main.simulationSpace = ParticleSystemSimulationSpace.World;
    }

    private void LateUpdate()
    {
        int numParticlesAlive = m_System.GetParticles(m_Paritcles);
        ParticleSystem.Particle single = new ParticleSystem.Particle();
        for (int i = 0; i < numParticlesAlive; i++)
        {
            Debug.Log(m_Paritcles[i].position);
            //m_Paritcles[i].velocity += Vector3.up * m_Drift;
        }
        m_System.SetParticles(m_Paritcles, numParticlesAlive);
        //Debug.Log(numParticlesAlive);
    }
}
