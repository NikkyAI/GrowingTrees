using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace GrowingTrees
{
	public class LeavesBreakingBehavior : BlockBehavior
	{
		public static string NAME { get; }
			= $"{ GrowingTreesSystem.MOD_ID }:LeavesBreaking";
		
		
		public LeavesBreakingBehavior(Block block)
			: base(block) {  }
		
		public override void OnBlockBroken(
			IWorldAccessor world, BlockPos pos,
			IPlayer byPlayer, ref EnumHandling handling)
		{
			// Execute default behavior if broken in creative mode.
			if (byPlayer?.WorldData.CurrentGameMode == EnumGameMode.Creative) return;
			
			// Replace branch block with non-leafy version.
			var newCode  = block.Code.ToString().Replace("leafybranch", "branch");
			var newBlock = world.GetBlock(new AssetLocation(newCode));
			// If there is no associated non-leafy block, execute default behavior.
			if (newBlock == null) return;
			// Otherwise continue and replace with non-leafy version.
			world.BlockAccessor.SetBlock(newBlock.BlockId, pos);
			
			// On server, drop items from the destroyed leaves. Like saplings!
			if (world.Side == EnumAppSide.Server) {
				var center = new Vec3d(pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5);
				var drops  = block.GetDrops(world, pos, byPlayer, 1.0F) ?? new ItemStack[0];
				foreach (var stack in drops) {
					var maxStackSize = (block.SplitDropStacks ? 1 : stack.StackSize);
					for (int i = 0; i < stack.StackSize; i += maxStackSize) {
						var dropStack = stack.Clone();
						dropStack.StackSize = Math.Min(maxStackSize, stack.StackSize - i);
						world.SpawnItemEntity(dropStack, center, null);
					}
				}
				world.PlaySoundAt(this.block.Sounds?.GetBreakSound(byPlayer),
				                  center.X, center.Y, center.Z, byPlayer);
			}
			
			// Override default behavior.
			handling = EnumHandling.PreventDefault;
		}
	}
}
