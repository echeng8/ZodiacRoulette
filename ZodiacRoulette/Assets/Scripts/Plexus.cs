using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Plexus : MonoBehaviour
{
    public float maxDistance = 1f;
    public int maxConnections = 5;
    public int maxLineRenderers = 100;
    public LineRenderer lineRendererTemplate;

    List<LineRenderer> lineRenderers = new List<LineRenderer>();

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule particleSystemMainModule;

    Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int maxParticles = particleSystemMainModule.maxParticles;

        if(particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }

        int lrIndex = 0;
        int lineRendererCount = lineRenderers.Count;

        if(lineRendererCount > maxLineRenderers)
        {
            for(int i = maxLineRenderers; i < lineRendererCount; i++)
            {
                Destroy(lineRenderers[i].gameObject);
            }
            lineRenderers.RemoveRange(maxLineRenderers, lineRendererCount - maxLineRenderers);
            lineRendererCount -= lineRendererCount - maxLineRenderers;
        }

        if (maxConnections > 0 && maxLineRenderers > 0)
        {
            particleSystem.GetParticles(particles);
            int particleCount = particleSystem.particleCount;
            float maxDistanceSquared = maxDistance * maxDistance;


            switch (particleSystemMainModule.simulationSpace)
            {
                case ParticleSystemSimulationSpace.Local:
                    _transform = transform;
                    lineRendererTemplate.useWorldSpace = false;
                    break;
                case ParticleSystemSimulationSpace.Custom:
                    _transform = particleSystemMainModule.customSimulationSpace;
                    break;
                case ParticleSystemSimulationSpace.World:
                    _transform = transform;
                    lineRendererTemplate.useWorldSpace = true;
                    break;
                default:
                    throw new System.NotSupportedException("Unsupported simulation space");
            }


            for (int i = 0; i < particleCount; i++)
            {
                if (lrIndex == maxLineRenderers)
                {
                    break;
                }
                Vector3 p1Pos = particles[i].position;
                int connections = 0;

                for (int j = i + 1; j < particleCount; j++)
                {
                    Vector3 p2Pos = particles[j].position;
                    float distanceSquared = Vector3.SqrMagnitude(p1Pos - p2Pos); //more performant friendly than Vector3.Distance which has square root calculation (heavy)

                    if (distanceSquared <= maxDistanceSquared)
                    {
                        LineRenderer lr;
                        if (lrIndex == lineRendererCount)
                        {
                            lr = Instantiate(lineRendererTemplate, _transform, false);
                            lineRenderers.Add(lr);
                        }
                        lr = lineRenderers[lrIndex];
                        lr.enabled = true;
                        lr.SetPosition(0, p1Pos);
                        lr.SetPosition(1, p2Pos);
                        lrIndex++;
                        connections++;
                        if(connections == maxConnections || lrIndex == maxLineRenderers)
                        {
                            break;
                        }
                    }
                }
            }
        }


        for (int i = lrIndex; i < lineRendererCount; i++)
        {
            lineRenderers[i].enabled = false;
        }
    }
}
