using UnityEngine;
using TMPro;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterWorldUI : MonoBehaviour
    {
        [SerializeField] private MultimeterController multimeterController;
        [SerializeField] private GameObject arrow;
        [SerializeField] private Color arrowHiglightColor = Color.yellow;
        [SerializeField] private TMP_Text mainNumbers;
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

        private void MeasurementModeChanged(MeasurmentMode measurmentMode, float currentMeasurment)
        {
            mainNumbers.text = currentMeasurment.ToString("F2");

            switch (measurmentMode)
            {
                case MeasurmentMode.Neutral:
                    _rotationAngle = 0f;
                    break;
                case MeasurmentMode.DCVoltage:
                    _rotationAngle = 76f;
                    break;
                case MeasurmentMode.ACVoltage:
                    _rotationAngle = 133f;
                    break;
                case MeasurmentMode.CurrentStrength:
                    _rotationAngle = 150f;
                    break;
                case MeasurmentMode.Resistance:
                    _rotationAngle = 284f;
                    break;
            }
        }

        private void OnDestroy() 
        {
            multimeterController.MeasurementModeChanged -= MeasurementModeChanged;
        }
    }
}