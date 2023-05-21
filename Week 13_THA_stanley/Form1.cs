using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Week_13_THA_stanley
{
    public partial class Form1 : Form
    {
        MySqlConnection connect = new MySqlConnection();
        MySqlCommand command = new MySqlCommand();
        MySqlDataAdapter data = new MySqlDataAdapter();
        string connection = "server=localhost;uid=root;pwd=;database=premier_league; port=8111";
        string sql;
        DataTable one = new DataTable();
        DataTable two = new DataTable();
        DataTable three = new DataTable();
        DataTable four = new DataTable();
        DataTable five = new DataTable();
        DataTable six = new DataTable();
        string a = "1";
        string b = "0";
        string simpen;
        string work;
        string unwork;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sql = "select team_name, team_id from team;";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql,connect);
            data = new MySqlDataAdapter(command);
            data.Fill(one);
            comboBox2.DataSource = one;
            comboBox2.DisplayMember = "team_name";
            comboBox2.ValueMember = "team_id";
            comboBox3.DataSource = one;
            comboBox3.DisplayMember = "team_name";
            comboBox3.ValueMember = "team_id";
            comboBox4.DataSource = one;
            comboBox4.DisplayMember = "team_name";
            comboBox4.ValueMember = "team_id";
            
            sql = "select nation, nationality_id from nationality;";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(two);
            comboBox1.DataSource = two;
            comboBox1.DisplayMember = "nation";
            comboBox1.ValueMember = "nationality_id";

            sql = "select * from player;";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(three);
            dataGridView1.DataSource = three;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = $"insert into player values ('{textBox2.Text}', {textBox3.Text}, '{textBox1.Text}', '{comboBox1.SelectedValue}', '{textBox4.Text}', {textBox5.Text}, {textBox6.Text}, '{dateTimePicker1.Text}', '{comboBox2.SelectedValue}', {a}, {b})";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            three = new DataTable();
            sql = "select * from player;";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(three);
            dataGridView1.DataSource = three;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(four.Rows.Count <= 11)
            {
                MessageBox.Show("error!!!!!! < dri 11");
            }
            else
            {
                sql = $"update player set status = '0' where player_id = '{simpen}';";
                connect = new MySqlConnection(connection);
                command = new MySqlCommand(sql, connect);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                four = new DataTable();
                sql = $"select p.player_id,p.player_name,n.nation,p.playing_pos,p.team_number, p.height, p.weight, p.birthdate, p.status from player p,nationality n where p.team_id = '{comboBox3.SelectedValue}' and n.nationality_id = p.nationality_id and p.status = '1';"; ;
                connect = new MySqlConnection(connection);
                command = new MySqlCommand(sql, connect);
                data = new MySqlDataAdapter(command);
                data.Fill(four);
                dataGridView2.DataSource = four;
            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sql = $"select p.player_id,p.player_name,n.nation,p.playing_pos,p.team_number, p.height, p.weight, p.birthdate, p.status from player p,nationality n where p.team_id = '{comboBox3.SelectedValue}' and n.nationality_id = p.nationality_id and p.status = '1';"; ;
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(four);
            dataGridView2.DataSource = four;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            simpen = dataGridView2.SelectedCells[0].Value.ToString();
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sql = $"select m.manager_id,m.manager_name,t.team_name,m.birthdate, m.nationality_id, m.working from manager m\r\nleft join team t on m.manager_id = t.manager_id where t.team_id = '{comboBox4.SelectedValue}';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(five);
            dataGridView3.DataSource = five;

            sql = $"select m.manager_id,m.manager_name,m.birthdate, m.nationality_id, m.working from manager m  where m.working = '0';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(six);
            dataGridView4.DataSource = six;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            work = dataGridView3.SelectedCells[0].Value.ToString();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            unwork = dataGridView4.SelectedCells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sql = $"update team set manager_id = '{unwork}' where team_id = '{comboBox4.SelectedValue}';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            sql = $"update manager set working = '0' where manager_id = '{work}';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            sql = $"update manager set working = '1' where manager_id = '{unwork}';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            five = new DataTable();
            six = new DataTable();  

            sql = $"select m.manager_id,m.manager_name,t.team_name,m.birthdate, m.nationality_id, m.working from manager m\r\nleft join team t on m.manager_id = t.manager_id where t.team_id = '{comboBox4.SelectedValue}';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(five);
            dataGridView3.DataSource = five;

            sql = $"select m.manager_id,m.manager_name,m.birthdate, m.nationality_id, m.working from manager m  where m.working = '0';";
            connect = new MySqlConnection(connection);
            command = new MySqlCommand(sql, connect);
            data = new MySqlDataAdapter(command);
            data.Fill(six);
            dataGridView4.DataSource = six;
        }
    }
}
