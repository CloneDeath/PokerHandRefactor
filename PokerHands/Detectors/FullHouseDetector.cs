using System.Linq;

namespace PokerHands
{
	public class FullHouseDetector{
		private HandRanker _handRanker;
		public FullHouseDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasFullHouse(out int winner1)
		{
			if ((_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 3) && _handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 2)) ||
			    (_handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 3) && _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 2))){
				// Full house
				if ((_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 3) && _handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 2)) &&
				    !(_handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 3) && _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 2))){
					winner1 = 1;
					return true;
				}

				if (!(_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 3) && _handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 2))){
					winner1 = 2;
					return true;
				}
			}
			winner1 = 0;
			return false;
		}
	}
}