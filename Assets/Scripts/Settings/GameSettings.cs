using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public float BubbleSize;
        public float BubbleLineSpace;
        public List<BubbleSettings> BubbleSettings;
    }
}