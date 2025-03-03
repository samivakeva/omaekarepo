using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public AudioSource itemSound;
    public AudioClip itemClip;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TYNNYRI"))
        {
            itemSound.PlayOneShot(itemClip);
            Debug.Log("Collected an item");
            //collision.gameObject.SetActive(false);
            collision.transform.parent.gameObject.SetActive(false);


        }
    }


}
