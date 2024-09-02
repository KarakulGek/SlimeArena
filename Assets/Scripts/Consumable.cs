using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт для съедобных предметов
public class Consumable : MonoBehaviour
{
    [SerializeField] public float HealAmount;
    [SerializeField] public GameObject PickUpVFX;
    public GameObject CurrentPickUpVFX;
    public float grow;
    private void Awake() 
    {
        transform.localScale = Vector3.zero;
        grow = 0;
    }
    private void Update()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0, 1, grow / 1), Mathf.Lerp(0, 1, grow / 1),Mathf.Lerp(0, 1, grow / 1));
            grow += Time.deltaTime * 1.5f;
        }
    }
    //Лечение при подборе игроком
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Heal(HealAmount);
            CurrentPickUpVFX = Instantiate(PickUpVFX, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(CurrentPickUpVFX, 2f);
            Destroy(gameObject);
        }
    }
}
