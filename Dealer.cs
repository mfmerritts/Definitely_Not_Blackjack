using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    public class Dealer : Person
    {
        public Dealer(string name, int num) : base(name, num)
        {
            Name = name;
            ID = num;
            players_Turn = false;
            hand = new List<Card>();
        }

        public void auto_Deal(BlackjackTable bjt) //Possible infinite loop, check logic with group
        {
            if(this.get_Turn())
            {
                bjt.deal_Card_To_Person(5);
                bjt.deal_Card_To_Person(5);

                while(sum_Hand() < 17)
                {
                    bjt.deal_Card_To_Person(5);

                    if (sum_Hand() > 21)
                    {
                        for (int i = 0; i < hand.Count(); i++)
                        {
                            if (hand[i].get_Rank() == 'a' && sum_Hand() > 21)
                            {
                                hand[i].change_Ace();
                            }
                        }
                    }
                }                
            }
        }
    }
}
