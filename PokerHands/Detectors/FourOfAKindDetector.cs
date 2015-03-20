using System.Linq;

namespace PokerHands
{
	public class FourOfAKindDetector{
		private HandRanker _handRanker;
		public FourOfAKindDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasFourOfAKind(out int winner1)
		{
			if (_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 4) || _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 4)){
				if (_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 4) && _handRanker.Player2Hand.ValueCountList.All(i => i.Count != 4)){
					winner1 = 1;
					return true;
				}

				if (_handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 4) && _handRanker.Player1Hand.ValueCountList.All(i => i.Count != 4)){
					winner1 = 2;
					return true;
				}

				var hand1FourOfAKindValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 4).Select(i => (int) i.Value).FirstOrDefault();
				var hand2FourOfAKindValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 4).Select(i => (int) i.Value).FirstOrDefault();

				if (hand1FourOfAKindValue > hand2FourOfAKindValue){
					winner1 = 1;
					return true;
				}

				if (hand2FourOfAKindValue > hand1FourOfAKindValue){
					winner1 = 2;
					return true;
				}
			}
			winner1 = 0;
			return false;
		}
	}
}