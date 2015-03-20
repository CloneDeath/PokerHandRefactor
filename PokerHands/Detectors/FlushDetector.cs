namespace PokerHands{
	public class FlushDetector{
		private HandRanker _handRanker;
		public FlushDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasFlush(out int winner)
		{
			if (_handRanker.Player1Hand.HasFlush ^ _handRanker.Player2Hand.HasFlush){
				if (_handRanker.Player1Hand.HasFlush){
					winner = 1;
				}
				else{
					winner = 2;
				}
				return true;
			}
			winner = 0;
			return false;
		}
	}
}