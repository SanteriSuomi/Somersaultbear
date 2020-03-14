using UnityEngine;

namespace Somersaultbear
{
    public class InputManager : Singleton<InputManager>
    {
        public InputSchemeBase InputScheme { get; private set; }
        [SerializeField]
        private InputSchemePC inputSchemePC = default;
        [SerializeField]
        private InputSchemeMobile inputSchemeMobile = default;
        [SerializeField]
        private GameObject mobileMenus = default;
        [SerializeField]
        private GameObject mobileMenuButton = default;
        [SerializeField]
        private string[] pcInputSchemePlatformNames = default;
        [SerializeField]
        private string[] mobileInputSchemePlatformNames = default;

        protected override void Awake()
        {
            base.Awake();
            SetInputScheme();
        }

        private void SetInputScheme()
        {
            string platformName = Application.platform.ToString();
            InputScheme = SelectInputScheme(platformName);

            #if UNITY_EDITOR
            if (InputScheme is null)
            {
                Debug.LogError("Did not find an input scheme that exists!");
            }
            #endif

            InputScheme.gameObject.SetActive(true); // Activate the gameobject that contains the input scheme
        }

        private InputSchemeBase SelectInputScheme(string platformName)
        {
            for (int i = 0; i < pcInputSchemePlatformNames.Length; i++)
            {
                if (pcInputSchemePlatformNames[i].Contains(platformName))
                {
                    return inputSchemePC;
                }
            }

            for (int i = 0; i < mobileInputSchemePlatformNames.Length; i++)
            {
                if (pcInputSchemePlatformNames[i].Contains(platformName))
                {
                    mobileMenus.SetActive(true); // Activate mobile menu button
                    mobileMenuButton.SetActive(true);
                    return inputSchemeMobile;
                }
            }

            return null;
        }
    }
}