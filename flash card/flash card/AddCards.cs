using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace flash_card
{
    public partial class AddCards : Form
    {
        public AddCards()
        {
            InitializeComponent();
            label4.Text = (Data.myCards.Count-1).ToString();
            comboBox1.Items.Add("חיות");
            comboBox1.Items.Add("צבעים");
            comboBox1.Items.Add("אוכל");
            comboBox1.Items.Add("משפחה");
            comboBox1.Items.Add("בגדים");
        }

        private void AddCardButton(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("נא למלא את השדה הריק");
                return;
            }
            StreamWriter sw = new StreamWriter("flashCards.txt", true);
            flashCardWImg card = new flashCardWImg( Data.myCards.Count, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text);
            if (card.Image1 == "" && (Data.myCards.Count - Data.cardsWImg + 1) > Data.cardsWImg)
            {
                MessageBox.Show("חובה להוסיף תמונה (מחצית צריכות להיות עם תמונה)");
                sw.Close();
                return;
            }
            foreach (var item in Data.myCards)
            {
                if (card.CompareTo(item) == 1)
                {
                    MessageBox.Show("המילה כבר קיימת במאגר");
                    sw.Close();
                    return;
                }
            }
            if (textBox3.Text == "") 
                sw.WriteLine(Data.myCards.Count.ToString() + ';' + textBox1.Text + ';' + textBox2.Text + ';' + comboBox1.Text);

            else {
                sw.WriteLine(Data.myCards.Count.ToString() + ';' + textBox1.Text + ';' + textBox2.Text + ';' + comboBox1.Text + ';' + textBox3.Text + ';');
                Data.cardsWImg++;
            }
            Data.myCards.Add(card);
            sw.Close();

            MessageBox.Show("הכרטיס נוסף בהצלחה");
            if (Data.myCards.Count < 30)
            {
                MessageBox.Show("אין מספיק קלפים, נא להוסיף");
                AddCards ad = new AddCards();
                ad.Show();
            }
            else
            {
                List<string> myCategories = new List<string>();
                myCategories.Add("חיות");
                myCategories.Add("צבעים");
                myCategories.Add("אוכל");
                myCategories.Add("משפחה");
                myCategories.Add("בגדים");

                int i = 0;
                foreach (var item in Data.myCards)
                {
                    i = myCategories.IndexOf(item.Category1);
                    if (i >= 0)
                        myCategories.RemoveAt(i);
                }
                if (myCategories.Count>0)
                {
                    string msg = "יש קטגוריות שעדיין לא בחרת: ";
                    foreach (var item in myCategories)
                    {
                        msg += item + " ,";
                    }
                    MessageBox.Show(msg);
                }
            }
            

            this.Close();
        }
    }
}
