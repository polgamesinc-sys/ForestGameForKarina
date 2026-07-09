using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppleSystem : MonoBehaviour
{
    private int Count;

    [SerializeField] private Text CountText;

    [SerializeField] private GameObject ApplePrefab;

    [SerializeField] private AudioSource source;

    [SerializeField] private InputAction actionMenu;

    private void Start()
    { 
        Vector3 RandomSpawnPosition = new Vector3(Random.Range(25, 48), 12, Random.Range(55, 30));

        for (int i = 0; i < 10; i++)
        {
            Instantiate(ApplePrefab, RandomSpawnPosition, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        actionMenu.Enable();
        actionMenu.performed += GoMenu;
    }

    private void OnDisable()
    {
        actionMenu.Disable();
        actionMenu.performed -= GoMenu;
    }

    private void Update()
    {
        CountText.text = Count.ToString() + "/10";
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject CurrentApple;

        if(other.tag == "Apple")
        {
            CurrentApple = other.gameObject;
            source.Play();
            Destroy(CurrentApple);
            Count++;
        }
    }

    private void GoMenu(InputAction.CallbackContext contex)
    {
        SceneManager.LoadScene(0);
    }
}
