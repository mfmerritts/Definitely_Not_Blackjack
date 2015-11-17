using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    public class Person
    {
        public string Name;
        public int ID;
        public Boolean players_Turn;
        public List<Card> hand;

        public Person(string name, int id)
        {
            Name = name;
            ID = id;
            players_Turn = false;
            hand = new List<Card>();
        }

        //returns string name
        public string get_Name()
        {
            return Name;
        }

        //removes cards from hand
        public void take_Cards()
        {
            //code to be done once hand class is made.
            hand.Clear();
        }

        //returns int ID
        public int get_Id()
        {
            return ID;
        }

        //returns Boolean players_Turn
        public Boolean get_Turn()
        {
            return players_Turn;
        }

        //Starts player's turn
        public void start_Turn()
        {
            players_Turn = true;
        }

        //Ends player's turn
        public void end_Turn()
        {
            players_Turn = false;
        }
        public int sum_Hand()
        {
            int sum = 0;
            for (int i = 0; i < hand.Count(); i++)
            {
                sum = sum + hand[i].get_Score();
            }

            return sum;
        }

        public void print_Hand()
        {
            if (this.hand.Count() > 0)
            {
                for (int i = 0; i < this.hand.Count(); i++)
                {
                    Console.WriteLine(hand[i].get_Rank() + " : " + hand[i].get_Suit());
                }
            }
            else
            {
                Console.WriteLine(this.get_Name() + " has no cards");
            }
        }
    }
}
