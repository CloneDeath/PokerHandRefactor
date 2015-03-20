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

		public List<ValueCount> ValueCountList{
			get{
				return _cards.GroupBy(i => i.CardValue).Select(g => new ValueCount{
					Value = g.Key,
					Count = g.Select(v => (int) v.CardValue).Count()
				}).ToList();
			}
		}

		public void RemoveCardsWithCardValue(CardValue pairValue)
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

		public bool IsAStraight{
			get{
				int lowestHand2Value = ValueCountList.OrderBy(i => (int)i.Value).Select(i => (int)i.Value).FirstOrDefault();

				bool isHandAStraight = ValueCountList.Count == 5;

				if (isHandAStraight)
				{
					if (ValueCountList.Any(i => i.Value == CardValue.Ace))
					{
						for (var i = 0; i < 4; i++)
						{
							if (ValueCountList.Any(v => (int)v.Value == (i))) ;
							else
							{
								isHandAStraight = false;

								break;
							}
						}
					}
					else
					{
						foreach (var value in ValueCountList)
						{
							if (ValueCountList.Any(v => (int)v.Value == (lowestHand2Value)))
								lowestHand2Value = lowestHand2Value + 1;
							else
							{
								isHandAStraight = false;

								break;
							}
						}
					}
				}

				return isHandAStraight;
			}
		}

		public bool HasPair{
			get { return ValueCountList.Any(i => i.Count == 2); }
		}

		public bool HasFourOfAKind{
			get { return ValueCountList.Any(i => i.Count == 4); }
		}

		public bool HasThreeOfAKind{
			get { return ValueCountList.Any(i => i.Count == 3); }
		}
	}
}
