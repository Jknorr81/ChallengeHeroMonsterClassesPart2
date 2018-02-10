using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();
            hero.Name = "Batman";
            hero.Health = 50;
            hero.DamageMaximum = 25;
            hero.AttackBonus = true;


            Character monster = new Character();
            monster.Name = "Joker";
            monster.Health = 50;
            monster.DamageMaximum = 25;
            monster.AttackBonus = false;

            Dice dice = new Dice();

            if (hero.AttackBonus)
            {
                int damage = hero.Attack(dice);
                monster.Defend(damage);
                resultLabel.Text += "Bonus Attack:<br />";
                displayStats(hero);
                displayStats(monster);
                resultLabel.Text += "<br />";
            }

            if (monster.AttackBonus)
            {
                int damage = monster.Attack(dice);
                hero.Defend(damage);
                resultLabel.Text += "Bonus Attack:<br />";
                displayStats(hero);
                displayStats(monster);
                resultLabel.Text += "<br />";
            }


            while (hero.Health >= 0 && monster.Health >= 0)
            {
                int damage = monster.Attack(dice);
                hero.Defend(damage);

                damage = hero.Attack(dice);
                monster.Defend(damage);

                resultLabel.Text += "New Round<br />";
                displayStats(hero);
                displayStats(monster);
                resultLabel.Text += "<br />";
            }

            displayResult(hero, monster);
        }

        private void displayStats(Character character)
        {
            resultLabel.Text += string.Format("Name: {0} - Health: {1} - DamageMax: {2} - AttackBonus: {3} <br />",
                character.Name,
                character.Health,
                character.DamageMaximum,
                character.AttackBonus);
        }

        private void displayResult(Character opponent1, Character opponent2)
        {
            if (opponent1.Health < 0 && opponent2.Health < 0)
                resultLabel.Text += "<br /> Both parties lost there lives in this battle";
            else if (opponent1.Health > 0)
                resultLabel.Text += string.Format("<br />{0} wins!!!",opponent1.Name);
            else
                resultLabel.Text += string.Format("<br />{0} wins...", opponent2.Name);
        }
    }
    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        Random random = new Random();

        public int Attack(Dice dice)
        {
            dice.Sides = random.Next(0, DamageMaximum);
            int damage = dice.Roll(dice.Sides);          
            return damage;
        }

        public int Defend(int damage)
        {
            this.Health -= damage;
            return this.Health;
        }
    }

    class Dice
    {
        public int Sides { get; set; }

        Random random = new Random();
        

        public int Roll(int sides)
        {
            int roll = random.Next(0,sides);
            return roll;
        }
    }
}