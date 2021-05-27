using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    Light[] lights;
    Dictionary<Light, float> effectTimeDict = new Dictionary<Light, float>();
    Dictionary<Light, float> elapsedTimeDict = new Dictionary<Light, float>();
    Dictionary<Light, float> distanceDict = new Dictionary<Light, float>();
    Dictionary<Light, Vector3> targetDict = new Dictionary<Light, Vector3>();
    public float MovementIntensity = 0.7f;
    public float MinSpeed = 0.1f, MaxSpeed = 0.7f;
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        foreach(var light in lights) {
                effectTimeDict.Add(light, Random.Range(MinSpeed, MaxSpeed));
                elapsedTimeDict.Add(light, 0f);
                Vector3 newPos = Random.insideUnitSphere * MovementIntensity;
                targetDict.Add(light, transform.position + newPos);
                distanceDict.Add(light, Vector3.Distance(targetDict[light], light.transform.position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var light in lights) {
            if (effectTimeDict[light] > elapsedTimeDict[light]) {
                light.transform.position = Vector3.MoveTowards(light.transform.position, targetDict[light], distanceDict[light] * Time.deltaTime);
                elapsedTimeDict[light] += Time.deltaTime;
            }
            else {
                effectTimeDict[light] = Random.Range(MinSpeed, MaxSpeed);
                elapsedTimeDict[light] = 0f;
                Vector3 newPos = Random.insideUnitSphere * MovementIntensity;
                targetDict[light] = transform.position + newPos;
                distanceDict[light] = Vector3.Distance(targetDict[light], light.transform.position);
            }
        }
    }
}
