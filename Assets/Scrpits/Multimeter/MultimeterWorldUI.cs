using UnityEngine;
using TMPro;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterWorldUI : MonoBehaviour
    {
        [SerializeField] private MultimeterController multimeterController;
        [SerializeField] private GameObject arrow;
        [SerializeField] private TMP_Text measurementValue;
        [SerializeField] private Color arrowHighlightEmissionColor = Color.yellow;
        [SerializeField] private float emissionIntensity = 0.2f;
        [SerializeField] private float arrowRotationSpeed = 500f;

        private Material _arrowMaterial;
        private Color _baseEmissionColor;
        private Transform _arrowTransform;
        private float _rotationAngle = 0f;
        
        private const string EmissionColorPropertyName = "_EmissionColor";

        private void Awake()
        {
            InitializeArrowMaterial();
            _arrowTransform = arrow.transform;
            measurementValue.text = "0";

            multimeterController.MeasurementModeChanged += MeasurementModeChanged;
        }

        private void InitializeArrowMaterial()
        {
            if (!arrow.TryGetComponent(out MeshRenderer renderer))
            {
                return;
            }

            _arrowMaterial = renderer.material;
            if (_arrowMaterial == null)
            {
                return;
            }
            
            if (_arrowMaterial.HasProperty(EmissionColorPropertyName))
            {
                _baseEmissionColor = _arrowMaterial.GetColor(EmissionColorPropertyName);
                _arrowMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                _baseEmissionColor = Color.black;
            }
        }

        private void Update()
        {
            UpdateArrowHiglight();
            RotateArrow();
        }

        private void UpdateArrowHiglight()
        {
            if (_arrowMaterial == null)
            {
                return;
            }

            if (multimeterController.IsPointerOnArrow)
            {
                HiglightArrow();
            }
            else
            {
                SetArrowMaterialDefaults();
            }
        }

        private void HiglightArrow()
        {
            if (_arrowMaterial.HasProperty(EmissionColorPropertyName))
            {
                Color emissionColor = arrowHighlightEmissionColor * emissionIntensity;
                _arrowMaterial.SetColor(EmissionColorPropertyName, emissionColor);
            }
        }

        private void SetArrowMaterialDefaults()
        {
            if (_arrowMaterial.HasProperty(EmissionColorPropertyName))
            {
                _arrowMaterial.SetColor(EmissionColorPropertyName, _baseEmissionColor);
            }
        }

        private void RotateArrow()
        {
            Vector3 currentRotation = _arrowTransform.eulerAngles;
            float newZ = Mathf.MoveTowardsAngle(currentRotation.z, _rotationAngle, arrowRotationSpeed * Time.deltaTime);
            _arrowTransform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZ);
        }

        private void MeasurementModeChanged(MeasurementMode measurementMode, float currentMeasurement)
        {
            measurementValue.text = FormatForDisplay(currentMeasurement);
            _rotationAngle = MultimeterUIData.GetRotationAngle(measurementMode);
        }

        private static string FormatForDisplay(float value)
        {
            const int maxNumberLength = 5;
            string[] formats = { "F2", "F1", "F0" };

            foreach (string fmt in formats)
            {
                string str = value.ToString(fmt);
                if (str.Length <= maxNumberLength)
                    return str;
            }

            return "9999";
        }

        private void OnDestroy()
        {
            multimeterController.MeasurementModeChanged -= MeasurementModeChanged;
        }
    }
}