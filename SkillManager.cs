using System;
using Skills;
using UnityEngine;


    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;

        public SwordSkill sword { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(instance.gameObject);
        }

        private void Start()
        {
            sword = GetComponent<SwordSkill>();
        }
    }
