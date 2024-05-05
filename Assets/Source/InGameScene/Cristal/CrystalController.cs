using System;
using System.Linq;
using UnityEngine;


namespace Source.InGameScene.Cristal
{
    public class CrystalController : MonoBehaviour
    {
        //Advise: MonoBehaviourを継承したコンポーネントをアタッチしたオブジェクトであれば、こんな風にかいてもアタッチできるよ～
        [SerializeField] private CrystalView crystalPrefab;

        [SerializeField] private Transform centerTransform;
        [SerializeField] private float offset = 50f;
        
        private Transform _transform;
        private CrystalView[] _crystals;
        public int CrystalAmount => _crystals.Length;

        private void Awake()
        {
            _transform = transform;
            BuildCrystals();
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

        private void BuildCrystals()
        {
            // todo : make array
            var crystalNumbers = CrystalBuilder.BuildCrystals();
            
            _crystals = new CrystalView[crystalNumbers.Length];
            var center = centerTransform.position;
            for (int i = 0; i < crystalNumbers.Length; i++)
            {
                var crystal = MakeCrystal(i, crystalNumbers[i]);
                _crystals[i] = crystal;
                float theta = MathF.PI * i / crystalNumbers.Length * 2 + MathF.PI / 2;
                crystal.transform.position =
                    new Vector3(center.x + offset * MathF.Cos(theta), center.y + offset * MathF.Sin(theta));
            }

            // GameManager.Instance.CrystalAmount = crystalNumbers.Length;
        }

        private CrystalView MakeCrystal(int index, int number)
        {
            //Advise: crystalPrefabがCrystalView型なので、GetComponentはなくてOK
            var cristalView = Instantiate(crystalPrefab, _transform);
            //var cristalView = tmp.GetComponent<CrystalView>();
            cristalView.Init(index, number);
            return cristalView;
        }
    }
}