using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands
{
	public class Hand{
		private List<Card> _cards;

		public Hand(IList<Card> cards){
			_cards = cards.ToList();
		}

		public List<ValueCount> GetValueCounts{
			get{
				return _cards.GroupBy(i => i.CardValue).Select(g => new ValueCount{
					Value = g.Key,
					Count = g.Select(v => (int) v.CardValue).Count()
				}).ToList();
			}
		}

		public void RemovePairValue3CardsLeft(CardValue pairValue)
		{
			_cards = _cards.Where(i => i.CardValue != pairValue).ToList();
		}

		public bool HasFlush{
			get { return _cards.All(i => i.Suit == _cards.Select(j => j.Suit).FirstOrDefault()); }
		}

		public int GetLargestCardValue()
		{
			return _cards.Max(i => (int)i.CardValue);
		}

		internal void RemoveLargestCardValue()
		{
			var hand1MaxCard = _cards.FirstOrDefault(j => (int)j.CardValue == GetLargestCardValue());
			_cards.Remove(hand1MaxCard);
		}

		public bool HasCards{
			get { return _cards.Count > 0; }
		}
	}
}
