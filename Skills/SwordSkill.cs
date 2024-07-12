using UnityEngine;
using UnityEngine.Serialization;


namespace Skills
{
    public class SwordSkill : Skill
    {
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
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Vector2 aimDir = AimDirection();
                finalDir = new Vector2(aimDir.normalized.x * launchForce.x, aimDir.normalized.y * launchForce.y);
            }

            if (Input.GetKey(KeyCode.Mouse1))
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

            newSwordScript.SetupSword(finalDir, swordGravity, player, returnSpeed);
            player.AssignNewSword(newSword);
            DotsActive(false);
        }

        public Vector2 AimDirection()
        {
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
                               .5f * (Physics2D.gravity * swordGravity) * (t * t);
            return position;
        }
    }
}