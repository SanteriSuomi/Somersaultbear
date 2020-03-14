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
        private RuntimePlatform[] pcInputSchemePlatformNames = default;
        [SerializeField]
        private RuntimePlatform[] mobileInputSchemePlatformNames = default;

        protected override void Awake()
        {
            base.Awake();
            SetInputScheme();
        }

        private void SetInputScheme()
        {
            InputScheme = SelectInputScheme(Application.platform);

            #if UNITY_EDITOR
            if (InputScheme is null)
            {
                Debug.LogError("Did not find an input scheme that exists!");
            }
            #endif

            InputScheme.gameObject.SetActive(true); // Activate the gameobject that contains the input scheme
        }

        private InputSchemeBase SelectInputScheme(RuntimePlatform platform)
        {
            foreach (RuntimePlatform item in pcInputSchemePlatformNames)
            {
                if (item == platform)
                {
                    return inputSchemePC;
                }
            }

            foreach (RuntimePlatform item in mobileInputSchemePlatformNames)
            {
                if (item == platform)
                {
                    mobileMenus.SetActive(true); // Activate mobile menu button
                    mobileMenuButton.SetActive(true);
                    return inputSchemeMobile;
                }
            }

            return inputSchemePC; // Default to PC input scheme.
        }
    }
}