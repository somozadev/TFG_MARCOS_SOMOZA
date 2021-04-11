using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Block : MonoBehaviour
{
    [SerializeField] Hole holes;
    private ProceduralGenerator level;
    public blockType type;

    public void Initialize(ProceduralGenerator _level)
    {
        level = _level;
        level.RegisterNewBlock(this);
        GenerateConnections();
    }

    private void GenerateConnections()
    {
        int i=0;
        List<Block> blocks = new List<Block>();
        foreach (Transform point in holes.holes)
        {
            if (level.SizeAvailable > 0)
            {
                Block aux = Instantiate(SelectBlock(type).PickOne(),point.position,point.rotation);
                blocks.Add(aux);
                Debug.Log($"<color=yellow> Hole {i}</color> + {point.rotation.eulerAngles} + {point.parent.parent.gameObject.name}");
                i++;
            }
        }
        foreach(Block b in blocks)
            b.Initialize(level);
    }

    private IEnumerable<Block> SelectBlock(blockType type)
    {
        IEnumerable<Block> blocks = Enumerable.Empty<Block>();
        switch (type)
        {
            case blockType.SPAWN:
            case blockType.HALL:
                blocks = from block in level.blocks where block.type == blockType.CORRIDOR select block;
                break;
            case blockType.CORRIDOR:
                blocks = from block in level.blocks where block.type == blockType.HALL select block;
                break;
        }
        return blocks;
    }

}

[SerializeField]
public enum blockType
{
    CORRIDOR,
    SPAWN,
    HALL,
    CHEST,
    SHOP,
    REST,
    BOSS
}