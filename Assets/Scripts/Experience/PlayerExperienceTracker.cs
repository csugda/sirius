using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Experience
{
    class PlayerExperienceTracker : MonoBehaviour
    {
        public int currentXP;

        public void Start()
        {
            
        }
        public void OnEnable()
        {
            LoadXP();
        }

        private void LoadXP()
        {
            try
            {
                string line;
                StreamReader reader = new StreamReader("Assets/Resources/SaveData/Experience.srs");
                using (reader)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        Debug.LogError("No Experience info saved at 'Assets/Resources/Experience.srs'");
                    currentXP = int.Parse(line);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }
        }

        public void OnDisable()
        {
            SaveXP();
        }

        private void SaveXP()
        {
            string path = "Assets/Resources/Experience.srs";

            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine(currentXP);
            writer.Close();
        }
    }
}
