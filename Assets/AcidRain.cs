using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : MonoBehaviour
{
    [SerializeField]
    GameObject acidPrefab;

    [SerializeField]
    float radius;

    [SerializeField]
    float delay;

    [SerializeField]
    float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnerPos = this.gameObject.transform.position;
                float randX = Random.Range(spawnerPos.x - radius, spawnerPos.x + radius);
                float randZ = Random.Range(spawnerPos.z - radius, spawnerPos.z + radius);

                Vector3 acidPos = new Vector3(randX, spawnerPos.y, randZ);
                GameObject acid = Instantiate(acidPrefab, acidPos, Quaternion.identity);
                Destroy(acid, delay);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
