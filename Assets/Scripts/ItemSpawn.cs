using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public Item[] Items;
    private int Index;
    private Item PickUp;
    private float hp;
    private int Chance;
    // Start is called before the first frame update
    void Start()
    {
        Chance = Random.Range(0, 3);
        StartCoroutine("Wait");
        Index = Random.Range(0,Items.Length);
        PickUp = Items[Index];
        hp = this.GetComponent<Goblin>().currhealth;
    }

    // Update is called once per frame
    void Update()
    {
        hp = this.GetComponent<Goblin>().currhealth;
        if (hp <= 0)
        {
            if(Chance == Index)
            {
                Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                Instantiate(PickUp, pos, Quaternion.identity);
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
