using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Create Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    public float BubbleSize;
    public float BubbleLineSpace;
    public float BubblePrepareLaunchSpeed;
    public float BubbleFlySpeed;
    public float BubbleMergeSpeed;
    public List<BubbleSettings> BubbleSettings;
    public List<AudioClip> TransitionAudioClips;
    public List<AudioClip> BubbleAudioClips;
    public AudioClip Explode2K;
    public AudioClip Perfect;


    private static readonly int[] BubbleNumbers = new int[] {2, 4, 8, 16, 32, 64, 128, 256, 512, 1024};

    public List<int> Progression;

    public static int GetRandomBubbleNumber(int minIndex, int maxIndex)
    {
        var index = Random.Range(minIndex, maxIndex + 1);
        return BubbleNumbers[index];
    }
}
