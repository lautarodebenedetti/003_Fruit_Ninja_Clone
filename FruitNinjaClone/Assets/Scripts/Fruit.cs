using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;

    public void CreateSliceFruit()
    {
        GameObject inst = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);
        //Play slice sound
        FindObjectOfType<GameManager>().PlayRandomSlicedSound();
        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody r in rbsOnSliced)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(100, 200), transform.position, 5f);
        }

        FindObjectOfType<GameManager>().IncreaseScore(5);

        Destroy(inst.gameObject, 5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade blade = collision.GetComponent<Blade>();
        if (!blade)
        {
            return;
        }
        CreateSliceFruit();
    }
}
