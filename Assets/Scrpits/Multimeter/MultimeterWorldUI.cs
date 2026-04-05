using UnityEngine;
using TMPro;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterWorldUI : MonoBehaviour
    {
        [SerializeField] private MultimeterController multimeterController;
        [SerializeField] private MultimeterModeSwitcher modeSwitcher;
        [SerializeField] private TMP_Text measurementValue;
        [SerializeField] private Color modeSwitcherHighlightEmissionColor = Color.yellow;
        [SerializeField] private float emissionIntensity = 0.2f;
        [SerializeField] private float modeSwitcherRotationSpeed = 500f;

        private Material _modeSwitcherMaterial;
        private Color _initiaEmissionColor;
        private Transform _modeSwitcherTransform;
        private float _rotationAngle = 0f;
        
        private const int MaxNumberLength = 5;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            InitializeModeSwitcherMaterial();
            _modeSwitcherTransform = modeSwitcher.transform;
            measurementValue.text = "0";

            multimeterController.MeasurementModeChanged += OnMeasurementModeChanged;
        }

        private void InitializeModeSwitcherMaterial()
        {
            if (!modeSwitcher.gameObject.TryGetComponent(out MeshRenderer renderer))
            {
                return;
            }

            _modeSwitcherMaterial = renderer.material;
            if (_modeSwitcherMaterial == null)
            {
                return;
            }
            
            if (_modeSwitcherMaterial.HasProperty(EmissionColor))
            {
                _initiaEmissionColor = _modeSwitcherMaterial.GetColor(EmissionColor);
                _modeSwitcherMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                _initiaEmissionColor = Color.black;
            }
        }

        private void Update()
        {
            UpdateModeSwitcherHighlight();
            UpdateModeSwitcherRotation();
        }

        private void UpdateModeSwitcherHighlight()
        {
            if (_modeSwitcherMaterial == null)
            {
                return;
            }

            if (modeSwitcher.IsPointerOnModeSwitcher)
            {
                HighlightModeSwitcher();
            }
            else
            {
                ResetModeSwitcherMaterial();
            }
        }

        private void HighlightModeSwitcher()
        {
            if (_modeSwitcherMaterial.HasProperty(EmissionColor))
            {
                Color emissionColor = modeSwitcherHighlightEmissionColor * emissionIntensity;
                _modeSwitcherMaterial.SetColor(EmissionColor, emissionColor);
            }
        }

        private void ResetModeSwitcherMaterial()
        {
            if (_modeSwitcherMaterial.HasProperty(EmissionColor))
            {
                _modeSwitcherMaterial.SetColor(EmissionColor, _initiaEmissionColor);
            }
        }

        private void UpdateModeSwitcherRotation()
        {
            Vector3 currentRotation = _modeSwitcherTransform.eulerAngles;
            float newZ = Mathf.MoveTowardsAngle(currentRotation.z, _rotationAngle, modeSwitcherRotationSpeed * Time.deltaTime);
            _modeSwitcherTransform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZ);
        }

        private void OnMeasurementModeChanged(MeasurementMode measurementMode, float currentMeasurement)
        {
            measurementValue.text = FormatForAllowedSymbolsCountDisplay(MaxNumberLength, currentMeasurement);
            _rotationAngle = MultimeterUIData.GetRotationAngle(measurementMode);
        }

        private static string FormatForAllowedSymbolsCountDisplay(int symbolsCount, float value)
        {
            string[] formats = { "F2", "F1", "F0" };

            foreach (string fmt in formats)
            {
                string str = value.ToString(fmt);
                if (str.Length <= symbolsCount)
                    return str;
            }

            return "9999";
        }

        private void OnDestroy()
        {
            multimeterController.MeasurementModeChanged -= OnMeasurementModeChanged;
        }
    }
}