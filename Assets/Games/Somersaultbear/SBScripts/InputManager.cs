using UnityEngine;

namespace Somersaultbear
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField]
        private InputSchemePC inputSchemePC = default;
        [SerializeField]
        private InputSchemeMobile inputSchemeMobile = default;
        [SerializeField]
        private GameObject mobileMenus = default;
        [SerializeField]
        private GameObject mobileMenuButton = default;
        public InputSchemeBase InputScheme { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SetInputScheme();
        }

        private void SetInputScheme()
        {
            string platformName = Application.platform.ToString();
            if (platformName.Contains("Windows") || platformName.Contains("Linux") 
                || platformName.Contains("OSX") || platformName.Contains("WebGL"))
            {
                InputScheme = inputSchemePC;
            }
            else if (platformName.Contains("Android") || platformName.Contains("IPhone"))
            {
                InputScheme = inputSchemeMobile;
                mobileMenus.SetActive(true); // Activate mobile menu button
                mobileMenuButton.SetActive(true);
            }

            InputScheme.gameObject.SetActive(true); // Activate the gameobject that contains the input scheme
        }
    }
}