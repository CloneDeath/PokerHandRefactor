using System.Linq;

namespace PokerHands
{
	public class FullHouseDetector{
		private Hand _player1Hand;
		private Hand _player2Hand;

		public FullHouseDetector(HandRanker handRanker){
			_player1Hand = handRanker.Player1Hand;
			_player2Hand = handRanker.Player2Hand;
		}

		public bool DetermineIfPlayerHasFullHouse(out int winner)
		{
			winner = _player1Hand.HasFullHouse ? 1 : 2;
			return _player1Hand.HasFullHouse ^ _player2Hand.HasFullHouse;
		}
	}
}