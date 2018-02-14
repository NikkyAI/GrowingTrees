using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace GrowingTrees
{
	public class BlockBehaviorPlacement : BlockBehavior
	{
		public static string NAME { get; }
			= $"{ GrowingTreesMod.MOD_INFO.Name.ToLower() }:Placement";
		
		
		public BlockBehaviorPlacement(Block block)
			: base(block) {  }
		
		public override bool TryPlaceBlock(
			IWorldAccessor world, IPlayer byPlayer,
			IItemStack itemstack, BlockSelection blockSel,
			ref EnumHandling handling)
		{
			handling = EnumHandling.PreventDefault;
			var blockAtPosition = world.BlockAccessor.GetBlock(blockSel.Position);
			if (!blockAtPosition.IsReplacableBy(this.block))
				return false;
			
			var wood = this.block.FirstCodePart(1);
			var side = blockSel.Face.GetOpposite().Code.ToLower();
			var size = this.block.FirstCodePart(3);
			var placedAsset = this.block.CodeWithParts(wood, side, size);
			
			var placedBlock = world.BlockAccessor.GetBlock(placedAsset);
			placedBlock.DoPlaceBlock(world, blockSel.Position, blockSel.Face);
			return true;
		}
	}
}
