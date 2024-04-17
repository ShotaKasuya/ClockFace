using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Source.InGameScene.Cristal
{
    public class CrystalController : MonoBehaviour
    {
        [SerializeField] private GameObject crystalPrefab;

        [SerializeField] private Transform centerTransform;
        [SerializeField] private float offset = 50f;
        
        private Transform _transform;
        private CrystalView[] _crystals;
        public int CrystalAmount => _crystals.Length;

        private void Start()
        {
            _transform = transform;
            switch (DifficultySaver.Difficulty)
            {
                case Difficulty.Easy:
                    BuildCrystals(Random.Range(5, 7));
                    break;
                case Difficulty.Normal:
                    BuildCrystals(Random.Range(7, 9));
                    break;
                case Difficulty.Hard:
                    BuildCrystals(Random.Range(9, 12));
                    break;
            }
        }

        public bool CanDisable(int index)
        {
            if (index < 0 || index >= _crystals.Length)
            {
                Debug.Log("Index Error\n" +
                               $"len : value => {_crystals.Length}: {index}");
                return false;
            }

            var crystal = _crystals[index];
            return crystal.CanDisable();
        }

        public bool IsCleared()
        {
            return _crystals.All(x => !x.CanDisable() );
        }

        public void DisableCrystal(int index)
        {
            var crystal = _crystals[index];
            if ( !crystal.CanDisable()) return;
            
            crystal.Disable();
        }

        private void BuildCrystals(int cristalNum)
        {
            // todo : make array
            var crystalNumbers = CrystalBuilder.BuildCrystals();
            
            _crystals = new CrystalView[crystalNumbers.Length];
            var center = centerTransform.position;
            for (int i = 0; i < crystalNumbers.Length; i++)
            {
                var crystal = MakeCrystal(i, crystalNumbers[i]);
                _crystals[i] = crystal;
                crystal.transform.position =
                    new Vector3(center.x + offset * MathF.Cos(MathF.PI * i / crystalNumbers.Length * 2 + MathF.PI / 2),
                        center.y + offset * MathF.Sin(MathF.PI * i / crystalNumbers.Length * 2 + MathF.PI / 2));
            }

            // GameManager.Instance.CrystalAmount = crystalNumbers.Length;
        }

        private CrystalView MakeCrystal(int index, int number)
        {
            var tmp = Instantiate(crystalPrefab, _transform);
            var cristalView = tmp.GetComponent<CrystalView>();
            cristalView.Init(index, number);
            return cristalView;
        }
    }
}