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
			if (byPlayer?.WorldData.CurrentGameMode == EnumGameMode.Creative) return;
			handling = EnumHandling.PreventDefault;
			
			var code  = this.block.Code.ToString().Replace("leafybranch", "branch");
			var block = world.GetBlock(new AssetLocation(code));
			world.BlockAccessor.SetBlock(block.BlockId, pos);
		}
	}
}
