﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trafic_lights
{
    public partial class TraficLights : Form
    {
        private Timer timerSwitch = null;
        private Timer timerBlink = null;
        private int redInterval = 10;
        private int yellowInterval = 3;
        private int greenInterval = 6;
        private int timer = 0;
        private bool reverseLights = false;
        private bool greenBlink = true;

        
        public TraficLights()
        {
            InitializeComponent();
            InitializeProgram();
            InitializeTimerSwitch();
            InitializeTimerBlink();


        }
        private void InitializeProgram()
        {
            RedLight.BackColor = Color.Gray;
            YellowLight.BackColor = Color.Gray;
            GreenLight.BackColor = Color.Gray;

            RedLight.Region = Round(RedLight);
            YellowLight.Region = Round(YellowLight);
            GreenLight.Region = Round(GreenLight);

            RedNumeric.Value = redInterval;
            YellowNumeric.Value = yellowInterval;
            GreenNumeric.Value = greenInterval;

            greenBlinkCheck.Checked = true;
        }
        private void InitializeTimerSwitch()
        {
            timerSwitch = new Timer();
            timerSwitch.Interval = 1000;
            timerSwitch.Tick += new EventHandler(TimerSwitch_Tick);
            timerSwitch.Start();
        }
        private void InitializeTimerBlink()
        {
            timerBlink = new Timer();
            timerBlink.Interval = 500;
            timerBlink.Tick += new EventHandler(TimerBlink_Tick);
            
        }
        private void TimerBlink_Tick(object sender, EventArgs e)
        {
            GreenLightBlink();
        }
        private void TimerSwitch_Tick(object sender, EventArgs e)
        {
            timer++;
            if (reverseLights == false)
            {
                if (timer < redInterval)
                {
                    RedLight.BackColor = Color.Red;
                }
                if (timer == redInterval)
                {
                    YellowLight.BackColor = Color.Yellow;
                }
                if (timer == redInterval + yellowInterval)
                {
                    RedLight.BackColor = Color.Gray;
                    YellowLight.BackColor = Color.Gray;
                    GreenLight.BackColor = Color.Green;
                    if (greenBlink == true)
                    {
                        timerBlink.Start();
                    }
                    reverseLights = true;
                }
            }
            else
            {
                if (timer == redInterval + yellowInterval + greenInterval)
                {
                    timerBlink.Stop();
                    GreenLight.BackColor = Color.Gray;
                    YellowLight.BackColor = Color.Yellow;
                }
                if (timer == redInterval + yellowInterval + greenInterval + yellowInterval)
                {
                    YellowLight.BackColor = Color.Gray;
                    RedLight.BackColor = Color.Red;
                    reverseLights = false;
                    timer = 0;
                }
            }
        }
        private void GreenLightBlink()
        {
            if (GreenLight.BackColor == Color.Gray)
            {
                GreenLight.BackColor = Color.Green;
            }
            else
            {
                GreenLight.BackColor = Color.Gray;
            }
        }
        private void Buttons_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Tag == "R")
            {
                redInterval = Decimal.ToInt32(RedNumeric.Value);
            }
            else if (button.Tag == "Y")
            {
                yellowInterval = Decimal.ToInt32(YellowNumeric.Value);
            }
            else
            {
                greenInterval = Decimal.ToInt32(GreenNumeric.Value);
            }
        }
        private Region Round(PictureBox picB)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picB.Width, picB.Height);
            return new Region(gp);
        }
        private void greenBlinkCheck_Click(object sender, EventArgs e)
        {
            if (greenBlinkCheck.Checked == true)
            {
                greenBlink = true;
            }
            else
            {
                greenBlink = false;
            }
        }














        private void SwitchLights()//code that replaces if else statements 
        {
            int seconds = 0;
            switch (seconds)
            {
                case 0:
                    RedLight.BackColor = Color.Red;
                    break;
                case 3:
                    YellowLight.BackColor = Color.Yellow;
                    RedLight.BackColor = Color.Gray;
                    break;
                case 5:
                    YellowLight.BackColor = Color.Gray;
                    GreenLight.BackColor = Color.Green;
                    break;
                case 8:
                    YellowLight.BackColor = Color.Yellow;
                    GreenLight.BackColor = Color.Gray;
                    break;
                case 10:
                    YellowLight.BackColor = Color.Gray;
                    RedLight.BackColor = Color.Red;
                    seconds = -1;
                    break;
            }
            seconds++;
        }

    }
}
