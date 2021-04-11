using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public int seed;
    public int maxAllowedDistance;
    public int maxLevelSize;
    [SerializeField] Block initialBlock;
    public List<Block> blocks;
    
    [SerializeField] List<Block> registeredBlocks; 

    public int SizeAvailable {get; private set;}

    private void Start()
    {
        if (seed == 0)
            RandomService.SetSeed(seed);
        else
            seed = RandomService.seed;

        SizeAvailable = maxLevelSize;
        InitiateFirstBlock();


    }

    void InitiateFirstBlock()
    {
        Instantiate(initialBlock, transform.position, Quaternion.identity, transform).Initialize(this);
    }

    public void RegisterNewBlock(Block newBlock)
    {
        registeredBlocks.Add(newBlock);
        SizeAvailable--;
    }
}
