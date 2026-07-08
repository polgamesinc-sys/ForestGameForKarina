using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private InputAction clickButton;
    private static bool newScene = true;

    // Статическая ссылка на единственный экземпляр класса
    private static LoadManager instance;

    private void Awake()
    {
        // Проверяем, существует ли уже менеджер в памяти
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Уничтожаем дубликат
            return;
        }

        // Если это первый запуск — сохраняем ссылку
        instance = this;
        DontDestroyOnLoad(gameObject);

        clickButton.Enable();
        clickButton.performed += SceneSwitch;
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся от событий при уничтожении объекта
        if (instance == this)
        {
            clickButton.performed -= SceneSwitch;
        }
    }

    private void SceneSwitch(InputAction.CallbackContext context)
    {
        // Используем if (context.started) или context.performed в зависимости от настроек Action
        // Но Singleton-защита выше уже решит проблему дублирования вызовов
        SceneManager.LoadScene((newScene = !newScene) ? "BrightDay" : "demoScene_free");
    }
}
