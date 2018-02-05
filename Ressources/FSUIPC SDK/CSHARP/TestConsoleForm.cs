using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace FsuipcSdk
{

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TestConsoleForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnInitialize;
		private System.Windows.Forms.TextBox txtInitialize;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.Button btnGet;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtOpen;
		private System.Windows.Forms.TextBox txtProcess;
		private System.Windows.Forms.TextBox txtGet;
		private System.Windows.Forms.TextBox txtClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnRead;
		private System.Windows.Forms.TextBox txtRead;

		Fsuipc fsuipc = new Fsuipc();	// Our main fsuipc object!
		bool result = false;			// Return boolean for FSUIPC method calls
		bool stop = false;				// Flag to stop airspeed loop
		int dwFSReq = 0;				// Any version of FS is OK
		int dwResult = -1;				// Variable to hold returned results
		int dwOffset = 0x02BC;			// 02BC = indicated airspeed
		int dwSize = 4;					// 4 bytes for indicated airspeed
		int token = -1;

		private System.Windows.Forms.Button btnFullCycle;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.TextBox txtResults1;
		private System.Windows.Forms.TextBox txtResults2;
		private System.Windows.Forms.Button btnKosu;
		private System.Windows.Forms.TextBox txtKosu;
		private System.Windows.Forms.TextBox txtFullCycle;

		public void translateResultCode(int code) 
		{
			switch (code) 
			{

				case Fsuipc.FSUIPC_ERR_OK:
					txtResults1.Text = code + " = FSUIPC_ERR_OK";
					break;

				case Fsuipc.FSUIPC_ERR_OPEN:
					txtResults1.Text = code + " = FSUIPC_ERR_OPEN";
					break;

				case Fsuipc.FSUIPC_ERR_NOFS:
					txtResults1.Text = code + " = FSUIPC_ERR_NOFS";
					break;

				case Fsuipc.FSUIPC_ERR_REGMSG:
					txtResults1.Text = code + " = FSUIPC_ERR_REGMSG";
					break;

				case Fsuipc.FSUIPC_ERR_ATOM:
					txtResults1.Text = code + " = FSUIPC_ERR_ATOM";
					break;

				case Fsuipc.FSUIPC_ERR_MAP:
					txtResults1.Text = code + " = FSUIPC_ERR_MAP";
					break;

				case Fsuipc.FSUIPC_ERR_VIEW:
					txtResults1.Text = code + " = FSUIPC_ERR_VIEW";
					break;

				case Fsuipc.FSUIPC_ERR_VERSION:
					txtResults1.Text = code + " = FSUIPC_ERR_VERSION";
					break;

				case Fsuipc.FSUIPC_ERR_WRONGFS:
					txtResults1.Text = code + " = FSUIPC_ERR_WRONGFS";
					break;

				case Fsuipc.FSUIPC_ERR_NOTOPEN:
					txtResults1.Text = code + " = FSUIPC_ERR_NOTOPEN";
					break;

				case Fsuipc.FSUIPC_ERR_NODATA:
					txtResults1.Text = code + " = FSUIPC_ERR_NODATA";
					break;

				case Fsuipc.FSUIPC_ERR_TIMEOUT:
					txtResults1.Text = code + " = FSUIPC_ERR_TIMEOUT";
					break;

				case Fsuipc.FSUIPC_ERR_SENDMSG:
					txtResults1.Text = code + " = FSUIPC_ERR_SENDMSG";
					break;

				case Fsuipc.FSUIPC_ERR_DATA:
					txtResults1.Text = code + " = FSUIPC_ERR_DATA";
					break;

				case Fsuipc.FSUIPC_ERR_RUNNING:
					txtResults1.Text = code + " = FSUIPC_ERR_RUNNING";
					break;

				case Fsuipc.FSUIPC_ERR_SIZE:
					txtResults1.Text = code + " = FSUIPC_ERR_SIZE";
					break;

				case Fsuipc.FSUIPC_ERR_BUFOVERFLOW:
					txtResults1.Text = code + " = FSUIPC_ERR_BUFOVERFLOW";
					break;

				default:
					txtResults1.Text = code.ToString();
					break;

			}
		}

		public TestConsoleForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnInitialize = new System.Windows.Forms.Button();
            this.txtInitialize = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtOpen = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtGet = new System.Windows.Forms.TextBox();
            this.txtClose = new System.Windows.Forms.TextBox();
            this.txtResults1 = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.btnFullCycle = new System.Windows.Forms.Button();
            this.txtFullCycle = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnKosu = new System.Windows.Forms.Button();
            this.txtKosu = new System.Windows.Forms.TextBox();
            this.txtResults2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(19, 28);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(90, 26);
            this.btnInitialize.TabIndex = 0;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // txtInitialize
            // 
            this.txtInitialize.Location = new System.Drawing.Point(125, 28);
            this.txtInitialize.Name = "txtInitialize";
            this.txtInitialize.Size = new System.Drawing.Size(317, 22);
            this.txtInitialize.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(19, 65);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 26);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(19, 138);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(90, 27);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Process";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(19, 175);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(90, 27);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "Get";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(19, 212);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 27);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtOpen
            // 
            this.txtOpen.Location = new System.Drawing.Point(125, 65);
            this.txtOpen.Name = "txtOpen";
            this.txtOpen.Size = new System.Drawing.Size(317, 22);
            this.txtOpen.TabIndex = 6;
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(125, 138);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(317, 22);
            this.txtProcess.TabIndex = 7;
            // 
            // txtGet
            // 
            this.txtGet.Location = new System.Drawing.Point(125, 175);
            this.txtGet.Name = "txtGet";
            this.txtGet.Size = new System.Drawing.Size(317, 22);
            this.txtGet.TabIndex = 8;
            // 
            // txtClose
            // 
            this.txtClose.Location = new System.Drawing.Point(125, 212);
            this.txtClose.Name = "txtClose";
            this.txtClose.Size = new System.Drawing.Size(317, 22);
            this.txtClose.TabIndex = 9;
            // 
            // txtResults1
            // 
            this.txtResults1.Location = new System.Drawing.Point(19, 332);
            this.txtResults1.Name = "txtResults1";
            this.txtResults1.Size = new System.Drawing.Size(211, 22);
            this.txtResults1.TabIndex = 10;
            this.txtResults1.TextChanged += new System.EventHandler(this.txtResults_TextChanged);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(19, 102);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(90, 26);
            this.btnRead.TabIndex = 12;
            this.btnRead.Text = "Read";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(125, 102);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(317, 22);
            this.txtRead.TabIndex = 13;
            // 
            // btnFullCycle
            // 
            this.btnFullCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFullCycle.Location = new System.Drawing.Point(19, 249);
            this.btnFullCycle.Name = "btnFullCycle";
            this.btnFullCycle.Size = new System.Drawing.Size(144, 27);
            this.btnFullCycle.TabIndex = 14;
            this.btnFullCycle.Text = "Get Pos Repeatedly";
            this.btnFullCycle.Click += new System.EventHandler(this.btnFullCycle_Click);
            // 
            // txtFullCycle
            // 
            this.txtFullCycle.Location = new System.Drawing.Point(230, 249);
            this.txtFullCycle.Name = "txtFullCycle";
            this.txtFullCycle.Size = new System.Drawing.Size(212, 22);
            this.txtFullCycle.TabIndex = 15;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(173, 249);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 27);
            this.btnStop.TabIndex = 16;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnKosu
            // 
            this.btnKosu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKosu.Location = new System.Drawing.Point(19, 286);
            this.btnKosu.Name = "btnKosu";
            this.btnKosu.Size = new System.Drawing.Size(115, 27);
            this.btnKosu.TabIndex = 17;
            this.btnKosu.Text = "Go to KOSU";
            this.btnKosu.Click += new System.EventHandler(this.btnKosu_Click);
            // 
            // txtKosu
            // 
            this.txtKosu.Location = new System.Drawing.Point(144, 286);
            this.txtKosu.Name = "txtKosu";
            this.txtKosu.Size = new System.Drawing.Size(298, 22);
            this.txtKosu.TabIndex = 18;
            // 
            // txtResults2
            // 
            this.txtResults2.Location = new System.Drawing.Point(240, 332);
            this.txtResults2.Name = "txtResults2";
            this.txtResults2.Size = new System.Drawing.Size(202, 22);
            this.txtResults2.TabIndex = 19;
            // 
            // TestConsoleForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(446, 317);
            this.Controls.Add(this.txtResults2);
            this.Controls.Add(this.txtKosu);
            this.Controls.Add(this.btnKosu);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtFullCycle);
            this.Controls.Add(this.btnFullCycle);
            this.Controls.Add(this.txtRead);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtResults1);
            this.Controls.Add(this.txtClose);
            this.Controls.Add(this.txtGet);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.txtOpen);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtInitialize);
            this.Controls.Add(this.btnInitialize);
            this.Name = "TestConsoleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FSUIPC C# Test Console";
            this.Load += new System.EventHandler(this.TestConsoleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TestConsoleForm());
		}

		private void TestConsoleForm_Load(object sender, System.EventArgs e)
		{
		}

		private void btnInitialize_Click(object sender, System.EventArgs e)
		{
			fsuipc.FSUIPC_Initialization();
			txtInitialize.Text = "Initialized";
			txtOpen.Text = "";
			txtRead.Text = "";
			txtProcess.Text = "";
			txtGet.Text = "";
			txtClose.Text = "";
			txtKosu.Text = "";
			txtResults1.Text = dwResult.ToString();
			txtResults2.Text = dwResult.ToString();
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			result = fsuipc.FSUIPC_Open(dwFSReq, ref dwResult);
			if (result) 
			{
				txtOpen.Text = "Result = true (Successful)";
			}
			else
			{
				txtOpen.Text = "Result = false (Unsuccessful)";
			}
			translateResultCode(dwResult);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			fsuipc.FSUIPC_Close();

			txtInitialize.Text = "";
			txtOpen.Text = "";
			txtRead.Text = "";
			txtProcess.Text = "";
			txtGet.Text = "";
			txtClose.Text = "Closed";
			txtKosu.Text = "";
			dwResult = -1;
			txtResults1.Text = dwResult.ToString();
			txtResults2.Text = dwResult.ToString();
		}

		private void btnRead_Click(object sender, System.EventArgs e)
		{
			result = fsuipc.FSUIPC_Read(dwOffset, dwSize, ref token, ref dwResult);
			if (result) 
			{
				txtRead.Text = "Result = true (Successful)";
			}
			else
			{
				txtRead.Text = "Result = false (Unsuccessful)";
			}
			translateResultCode(dwResult);
		}

		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			result = fsuipc.FSUIPC_Process(ref dwResult);
			if (result) 
			{
				txtProcess.Text = "Result = true (Successful)";
			}
			else
			{
				txtProcess.Text = "Result = false (Unsuccessful)";
			}
			translateResultCode(dwResult);
		}

		private void btnGet_Click(object sender, System.EventArgs e)
		{
			result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
			if (result) 
			{
				txtGet.Text = "Result = true (Successful)";
			}
			else
			{
				txtGet.Text = "Result = false (Unsuccessful)";
			}
			translateResultCode(dwResult);
		}

		private void txtResults_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnFullCycle_Click(object sender, System.EventArgs e)
		{
			btnInitialize_Click(sender, e);
			btnOpen_Click(sender, e);

			double latHi = 0;
			double latLo = 0;
			double latitude = 0;

			double longHi = 0;
			double longLo = 0;
			double longitude = 0;

			while(!stop) 
			{

				// Get latitude
				result = fsuipc.FSUIPC_Read(0x0564, 4, ref token, ref dwResult);
				result = fsuipc.FSUIPC_Process(ref dwResult);
				result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
				latHi = dwResult;

				result = fsuipc.FSUIPC_Read(0x0560, 4, ref token, ref dwResult);
				result = fsuipc.FSUIPC_Process(ref dwResult);
				result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
				latLo = dwResult;

				if (latLo != 0) 
				{
					latLo = latLo / (65536.0 * 65536.0);
				}
				if (latHi > 0) 
				{
					latitude = latHi + latLo;
				}
				else 
				{
					latitude = latHi - latLo;
				}

				if (result) 
				{
					txtFullCycle.Text = "Result = true (Successful)";
					txtResults1.Text = dwResult + " lat (FS units)";
				}
				else
				{
					txtFullCycle.Text = "Result = false (Unsuccessful)";
				}

				// Get longitude
				result = fsuipc.FSUIPC_Read(0x056C, 4, ref token, ref dwResult);
				result = fsuipc.FSUIPC_Process(ref dwResult);
				result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
				longHi = dwResult;

				result = fsuipc.FSUIPC_Read(0x0568, 4, ref token, ref dwResult);
				result = fsuipc.FSUIPC_Process(ref dwResult);
				result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
				longLo = dwResult;

				if (longLo != 0) 
				{
					longLo = longLo / (65536.0 * 65536.0);
				}
				if (longHi > 0) 
				{
					longitude = longHi + longLo;
				}
				else 
				{
					longitude = longHi - longLo;
				}

				if (result) 
				{
					txtFullCycle.Text = "Result = true (Successful)";
					txtResults1.Text = latitude + " lat (FS units)";
					txtResults2.Text = longitude + " long (FS units)";
				}
				else
				{
					txtFullCycle.Text = "Result = false (Unsuccessful)";
				}

                Application.DoEvents();
			}
			
			fsuipc.FSUIPC_Close();

		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			stop = true;
		}

		private void btnKosu_Click(object sender, System.EventArgs e)
		{

			btnInitialize_Click(sender, e);
			btnOpen_Click(sender, e);

			// Set slew mode on
			result = fsuipc.FSUIPC_Write(0x05DC, 1, ref token, ref dwResult);

			// Set latitude
			result = fsuipc.FSUIPC_Write(0x0564, 4454206, ref token, ref dwResult);
			result = fsuipc.FSUIPC_Write(0x0560, 901120000, ref token, ref dwResult);

			// Set longitude
			result = fsuipc.FSUIPC_Write(0x056C, -991553537, ref token, ref dwResult);
			result = fsuipc.FSUIPC_Write(0x0568, 219480064, ref token, ref dwResult);

			// Write the data to FS
			result = fsuipc.FSUIPC_Process(ref dwResult);
			
			if (result) 
			{
				txtKosu.Text = "Result = true (Successful)";
				dwResult = dwResult / 128;
				txtResults1.Text = dwResult.ToString();
			}
			else
			{
				txtKosu.Text = "Result = false (Unsuccessful)";
			}

			System.Windows.Forms.Application.DoEvents();
			
			fsuipc.FSUIPC_Close();
		
		}
	}
}
