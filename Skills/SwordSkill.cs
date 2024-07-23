using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Skills
{
    public enum SwordType
    {
        Regular,
        // Bounce,
        Pierce,
        Spin
    }
    public class SwordSkill : Skill
    {
        [SerializeField] private SwordType swordType;
        
        [Header("Pierce Info")]
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity;

        [Header("Spin Info")] 
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 2;
        [SerializeField] private float spinHitCooldown = .5f;
        
        [Header("Skill Info")]
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        [FormerlySerializedAs("returnDurantion")] [SerializeField] private float returnSpeed = 1;
        

        private Vector2 finalDir;

        [Header("Aim dots")] 
        [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBetweenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        private GameObject[] dots;

        protected override void Start()
        {
            base.Start();
            GenerateDots();
            SetupGravity();
        }

        private void SetupGravity()
        {
            switch (swordType)
            {
                case SwordType.Pierce:
                    swordGravity = pierceGravity;
                    break;
                case SwordType.Spin:
                    swordGravity = spinGravity;
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (player.inputActions.Aim.WasReleasedThisFrame())
            {
                Vector2 aimDir = AimDirection();
                finalDir = new Vector2(aimDir.normalized.x * launchForce.x, aimDir.normalized.y * launchForce.y);
            }

            if (player.inputActions.Aim.IsInProgress())
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
                }
            }
        }

        public void CreateSword()
        {
            GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

            switch (swordType)
            {
                case SwordType.Pierce:
                    newSwordScript.SetupPierceSword(pierceAmount);
                    break;
                case SwordType.Spin:
                    newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration, spinHitCooldown);
                    break;
            }
            
            newSwordScript.SetupSword(finalDir, swordGravity, player, returnSpeed);
            player.AssignNewSword(newSword);
            DotsActive(false);
        }

        public Vector2 AimDirection()
        {
            // Debug.Log(player.inputActions.AimDir.ReadValue<Vector2>());
            // return player.inputActions.Look.ReadValue<Vector2>();
            Vector2 playerPosition = player.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - playerPosition;
            return direction;
        }

        public void DotsActive(bool _isActive)
        {
            foreach (var dot in dots)
            {
                dot.SetActive(_isActive);
            }
        }
        
        private void GenerateDots()
        {
            dots = new GameObject[numberOfDots];
            for (int i = 0; i < numberOfDots; i++)
            {
                dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            var aimDir = AimDirection().normalized;
            // 传说中的抛物线公式？
            Vector2 position = (Vector2)player.transform.position +
                               new Vector2(aimDir.x * launchForce.x, aimDir.y * launchForce.y) * t +
                               (Physics2D.gravity * swordGravity) * (t * t) * 0.5f;
            return position;
        }
    }
}