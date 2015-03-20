using System.Linq;

namespace PokerHands
{
	public class StraightFlushDetector{
		private HandRanker _handRanker;
		public StraightFlushDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasStraightFlush(out int winner)
		{
			if ((_handRanker.Player1Hand.HasFlush && _handRanker.Player1Hand.IsAStraight) || (_handRanker.Player2Hand.HasFlush && _handRanker.Player2Hand.IsAStraight)){
				if (!(_handRanker.Player1Hand.HasFlush && _handRanker.Player1Hand.IsAStraight)){
					winner = 2;
					return true;
				}

				if (!(_handRanker.Player2Hand.HasFlush && _handRanker.Player2Hand.IsAStraight)){
					winner = 1;
					return true;
				}

				var hand1MaxValue = _handRanker.Player1Hand.GetLargestCardValue();
				var hand2MaxValue = _handRanker.Player2Hand.GetLargestCardValue();

				if (hand1MaxValue == 12){
					var lowCard = _handRanker.Player1Hand.ValueCountList.OrderBy(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();

					if (lowCard == 0) hand1MaxValue = 3;
				}

				if (hand2MaxValue == 12){
					var lowCard = _handRanker.Player2Hand.ValueCountList.OrderBy(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();

					if (lowCard == 0) hand2MaxValue = 3;
				}

				if (hand1MaxValue > hand2MaxValue){
					winner = 1;
					return true;
				}

				if (hand1MaxValue < hand2MaxValue){
					winner = 2;
					return true;
				}
			}
			winner = 0;
			return false;
		}
	}
}