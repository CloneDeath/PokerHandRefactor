namespace PokerHands{
	public class FlushDetector{
		private HandRanker _handRanker;
		public FlushDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasFlush(out int winner1)
		{
			if (_handRanker.Player1Hand.HasFlush || _handRanker.Player2Hand.HasFlush){
				if (_handRanker.Player1Hand.HasFlush && !_handRanker.Player2Hand.HasFlush){
					winner1 = 1;
					return true;
				}

				if (!_handRanker.Player1Hand.HasFlush){
					winner1 = 2;
					return true;
				}
			}
			winner1 = 0;
			return false;
		}
	}
}