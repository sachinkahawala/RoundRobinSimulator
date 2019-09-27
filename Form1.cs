using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoundRobinSimulator
{
    public partial class Form1 : Form
        

    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            timer1.Enabled = false;
            simu.Enabled = false;
            button2.Enabled = false;
            Application.EnableVisualStyles();
            


        }

        Process[] processlist = new Process[15];
        Process[] waitqueue = new Process[100];
        int progCount1 = 0;
        int progCount2 = 0;
        int temptym = 0;
        int timeCount;
        int processnum = 0;
        Process currentp;
        int tottymreq;
        Boolean npro = false;
        int noprotym = 0;
        int extratym=0;
        public Label lebal { get { return label9; } }

        public void setprocesses(Process[] n)
        {
            for (int i = 0; i< 15; ++i)
            {
                n[i] = new Process();
            }

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int timeDiv = 100 / tottymreq;
            
            try {
                progressBar1.Increment(timeDiv);
                timeCount++;
                temptym++;
                eltym.Text = timeCount.ToString();
                currentp.setrtym(1);
                label13.Text = currentp.getrtym().ToString();
                label9.Text = currentp.getpid().ToString();
                if (progCount1 < dataGridView1.Rows.Count)
                {   foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells[1].Value) == timeCount)

                        {
                            processlist[progCount1].setdata(dataGridView1.Rows[progCount1].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[2].Value));
                            waitqueue[progCount2] = processlist[progCount1];
                            waitlist.Items.Add(waitqueue[progCount2].getpid());
                            progCount1++;
                            progCount2++;
                        }
                    }
                }
                if (temptym == Convert.ToInt32(Qnum.Text) || currentp.getrtym() == 0)
                {
                    if (currentp.getrtym() == 0)
                    {
                        donelist.Items.Add(currentp.getpid());
                        processnum++;
                        currentp = waitqueue[processnum];
                        if (waitlist.Items.Count>0) {
                            waitlist.Items[0].Remove();
                        }
                    }
                    else
                    {
                        if (waitlist.Items.Count > 0)
                        {
                            waitlist.Items[0].Remove();
                        }
                        waitqueue[progCount2] = waitqueue[processnum];
                        waitlist.Items.Add(waitqueue[progCount2].getpid());
                        progCount2++;
                        processnum++;
                        currentp = waitqueue[processnum];

                    }
                    temptym = 0;
                }
                if (timeCount == tottymreq+noprotym)
                {
                    timer1.Stop();
                    label9.Text = "Done!";
                }
                if (!npro)
                {
                    if (progCount1 < dataGridView1.Rows.Count)
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value) == timeCount)

                        {
                            processlist[progCount1].setdata(dataGridView1.Rows[progCount1].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[2].Value));
                            waitqueue[progCount2] = processlist[progCount1];



                            currentp = waitqueue[progCount2];
                            progCount2++;
                            progCount1++;
                            npro = true;
                            //processnum++;
                            //currentp = waitqueue[processnum];
                        }
                    }

                }

                // foreach (Process p in processlist)
                // {

                //     int j = 0;
                //    if (p.getrtym() == 0)
                //     {
                //         j++;
                //     }
                //     if (j == 15)
                //     {
                //         timer1.Stop();
                //      }
                //  }

            }
            catch (NullReferenceException)
            {
                
                noprotym++;
                //timeCount++;
                //temptym++;
                label13.Text = "unknown";
                label9.Text = "Null";
                npro = false;
                if (progCount1 < dataGridView1.Rows.Count)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value) == timeCount)

                    {
                        processlist[progCount1].setdata(dataGridView1.Rows[progCount1].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[2].Value));
                        waitqueue[progCount2] = processlist[progCount1];
                        
                        
                        
                        currentp = waitqueue[progCount2];
                        progCount2++;
                        progCount1++;
                        npro = true;
                        //processnum++;
                        //currentp = waitqueue[processnum];
                    }
                }

            }
            }
        

    


   
         private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                simu.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(id.Text, Convert.ToInt32(at.Text), Convert.ToInt32(pt.Text));
            this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
            id.Clear();
            at.Clear();
            pt.Clear();

        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            if (id.Text.Length > 0 && at.Text.Length > 0 && pt.Text.Length > 0)
            {
                button1.Enabled = true;
            }
        }

        private void at_TextChanged(object sender, EventArgs e)
        {
            if (id.Text.Length > 0 && at.Text.Length > 0 && pt.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(at.Text, "[^0-9]") && this.at.Text.Length < 9)
            {
                MessageBox.Show("Please enter a Valid time in Seconds.");
                at.Text = at.Text.Remove(at.Text.Length - 1);
            }
        }

        private void pt_TextChanged(object sender, EventArgs e)
        {
            if (id.Text.Length > 0 && at.Text.Length > 0 && pt.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(pt.Text, "[^0-9]") && this.pt.Text.Length < 9)
            {
                MessageBox.Show("Please enter a Valid time in Seconds.");
                pt.Text = pt.Text.Remove(pt.Text.Length - 1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = tottymreq;
            timer1.Enabled = true;
            button2.Enabled = true;
            button2.Text = "Pause";
            button2.BackColor = Color.Orange;
            setprocesses(processlist);
            npro = true;
            timeCount = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            eltym.Text = timeCount.ToString();
            processlist[progCount1].setdata(dataGridView1.Rows[progCount1].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[progCount1].Cells[2].Value));
            waitqueue[progCount2] = processlist[progCount1];
            label9.Text = waitqueue[processnum].getpid();
            currentp = waitqueue[processnum];
            waitlist.Items.Add(waitqueue[processnum].getpid());
            progCount1++;
            progCount2++;
            simu.Enabled = false;
            tottymreq = tottymreq + Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                    tottymreq=tottymreq+Convert.ToInt32(row.Cells[2].Value);
                   
              
            }
            for (int i=0;i< dataGridView1.Rows.Count-1;++i)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value)+ Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) < Convert.ToInt32(dataGridView1.Rows[i+1].Cells[1].Value))

                {
                    extratym = extratym - (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) - Convert.ToInt32(dataGridView1.Rows[i + 1].Cells[1].Value));
                }
            }
        }   



        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void Qnum_TextChanged(object sender, EventArgs e)
        {
            if (Qnum.Text.Length > 0 && dataGridView1.RowCount>0)
            {
                simu.Enabled = true;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(Qnum.Text, "[^0-9]") && this.Qnum.Text.Length < 9)
            {
                MessageBox.Show("Please enter a Valid Quantum time in seconds.");
                Qnum.Text = Qnum.Text.Remove(Qnum.Text.Length - 1);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            if (timer1.Enabled)
            {
                timer1.Stop();
                button2.BackColor = Color.LightBlue;
                button2.Text = "Start";
                button2.Image = RoundRobinSimulator.Properties.Resources.Play;
            }
            else
            {
                timer1.Start();
                button2.Text = "Pause";
                button2.BackColor = Color.Orange;
                button2.Image = RoundRobinSimulator.Properties.Resources.Pause;
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}



    public class Process{
        String processid;
        int timeArrive;
        int runTime=0;
        private bool running = false;
        int processedtym = 0;
       // void runonthread()
       // {
        //    Thread newThread = new Thread(new ThreadStart(Run));
        //    newThread.Start();
       // }

        //public void Run()
       // {
        //    Thread.Sleep(3000);
       //     Console.WriteLine("Running in a different thread.");
        // }
        public void setrtym(int n)
        {
            if (runTime != 0)
            {
                runTime = runTime - n;
            }
        }
        public int getptym()
        {
            return processedtym;
        }
        public void setptym(int n)
        {
            processedtym = processedtym + n;
        }
        public int getrtym()
        {
            return runTime;
        }
        public string getpid()
        {
            return processid;
        }
        public void setdata(String id,int at,int rt)
        {
            processid = id;
            timeArrive = at;
            runTime = rt;
        }
        public int getartym()
        {
            return timeArrive;
        }
        public void Start()
        {
        
            this.running = true;
        }


        public void Stop()
        {
            this.running = false;
        }
    }
