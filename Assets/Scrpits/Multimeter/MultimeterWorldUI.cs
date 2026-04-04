using UnityEngine;
using TMPro;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterWorldUI : MonoBehaviour
    {
        [SerializeField] private MultimeterController multimeterController;
        [SerializeField] private GameObject arrow;
        [SerializeField] private Color arrowHiglightColor = Color.yellow;
        [SerializeField] private TMP_Text measurementValue;
        [SerializeField] private float rotationSpeed = 500f;

        private Material _arrowMaterial;
        private Color _baseArrowColor;
        private Transform _arrowTransform;
        private float _rotationAngle = 0f;

        private void Awake() 
        {
            if (arrow.TryGetComponent(out MeshRenderer component))
            {
                _arrowMaterial = component.material;
                if (_arrowMaterial != null)
                {
                    _baseArrowColor = _arrowMaterial.color;
                }
            }

            _arrowTransform = arrow.transform;
            measurementValue.text = "0";

            multimeterController.MeasurementModeChanged += MeasurementModeChanged;
        }

        private void Update() 
        {
            if (_arrowMaterial == null)
            {
                return;
            }

            if (multimeterController.IsPointerOnArrow)
            {
                _arrowMaterial.color = arrowHiglightColor;
            }
            else
            {
                _arrowMaterial.color = _baseArrowColor;
            }

            Vector3 currentRotation = _arrowTransform.eulerAngles;
            float newZ = Mathf.MoveTowardsAngle(currentRotation.z, _rotationAngle, rotationSpeed * Time.deltaTime);
            _arrowTransform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZ);
        }

        private void MeasurementModeChanged(MeasurementMode measurementMode, float currentMeasurement)
        {
            measurementValue.text = FormatForDisplay(currentMeasurement);
            _rotationAngle = MultimeterUIData.GetRotationAngle(measurementMode);
        }

        private string FormatForDisplay(float value)
        {
            const int maxNumberLength = 5;

            string[] formats = { "F2", "F1", "F0" };

            foreach (string fmt in formats)
            {
                string str = value.ToString(fmt);
                if (str.Length <= maxNumberLength)
                {
                    return str;
                }
            }

            return value >= 10000 ? "9999" : value <= -10000 ? "-999" : "0";
        }

        private void OnDestroy() 
        {
            multimeterController.MeasurementModeChanged -= MeasurementModeChanged;
        }
    }
}