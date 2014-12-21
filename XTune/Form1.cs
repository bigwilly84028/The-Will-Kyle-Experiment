using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

using FTD2XX_NET;

namespace XTune
{
    public partial class mainForm : Form
    {
        
        public mainForm()
        {
            InitializeComponent();
            label1.Text = "Searching for Devices...";
            

            EasyTimer.SetInterval(() =>
            {
                // start FTDI communication
                UInt32 ftdiDeviceCount = 0;
                FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

                // Create new instance of the FTDI device class
                FTDI myFtdiDevice = new FTDI();

                // Determine the number of FTDI devices connected to the machine
                ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

                if (ftStatus == FTDI.FT_STATUS.FT_OK) 
                {
                    label1.Text = "Number of FTDI devices: " + ftdiDeviceCount.ToString();
                }
                else
                {
                    // Wait for a key press
                    label1.Text = "Failed to get number of devices (error: " + ftStatus.ToString() + ")";
                    return;
                }

                // If no devices available, return
                if (ftdiDeviceCount == 0)
                {
                    label1.Text = "No Devices Found (error " + ftStatus.ToString() + ")";
                    return;
                }
                else {
                    label1.Text = "Got Devices! (Mssg: " + ftStatus.ToString() + ")";
                }

                // Allocate storage for device info list
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];
              
                // --- You code here ---
                // This piece of code will once after 1000 ms delay
                
            }, 1000);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Psyche! This doesn't do anything yet.");
            
        }


    }
    public static class EasyTimer
    {
        public static IDisposable SetInterval(Action method, int delayInMilliseconds)
        {
            System.Timers.Timer timer = new System.Timers.Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) =>
            {
                method();
            };

            timer.Enabled = true;
            timer.Start();

            // Returns a stop handle which can be used for stopping
            // the timer, if required
            return timer as IDisposable;
        }

        public static IDisposable SetTimeout(Action method, int delayInMilliseconds)
        {
            System.Timers.Timer timer = new System.Timers.Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) =>
            {
                method();
            };

            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();

            // Returns a stop handle which can be used for stopping
            // the timer, if required
            return timer as IDisposable;
        }
    }
}



