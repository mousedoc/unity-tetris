public class BlockGroupInfo
{
    public BlockShapeType GroupType { get; private set; }
    public BlockColorType ColorType { get; private set; }

    public BlockGroupInfo(BlockShapeType group, BlockColorType color)
    {
        GroupType = group;
        ColorType = color;
    }

    public static BlockGroupInfo GetRandomBlock()
    {
        return new BlockGroupInfo(EnumExtension.GetRandom<BlockShapeType>(), EnumExtension.GetRandom<BlockColorType>());
    }
}
