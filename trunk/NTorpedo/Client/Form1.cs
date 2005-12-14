/*
* ====================================================================
*
* NTORPEDO
* A Testbed of Object Relational Products for Enterprise Distributed Objects
* Copyright (c) 2005 torpedo-group
* http://www.torpedo-group.org
* @author Bruce Martin
* @version 8.25.04
* Translated to c# by Mats Helander
*
* This library is free software; you can redistribute it and/or modify it
* under the terms of the GNU Lesser General Public License 2.1 or later, as
* published by the Free Software Foundation. See the included license.txt
* or http://www.gnu.org/copyleft/lesser.html for details.
* ====================================================================
*/

using System;
using System.Windows.Forms;

namespace NTorpedo.Client
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button runTestsButton;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox listAuctionCheckBox;
		private System.Windows.Forms.Button clearAllButton;
		private System.Windows.Forms.Button checkAllButton;
		private System.Windows.Forms.TextBox resultsTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label listAuctionHitsLabel;
		private System.Windows.Forms.Label listAuctionTwiceWithoutTransactionHitsLabel;
		private System.Windows.Forms.CheckBox listAuctionTwiceWithoutTransactionCheckBox;
		private System.Windows.Forms.Label listAuctionTwiceWithTransactionHitsLabel;
		private System.Windows.Forms.CheckBox listAuctionTwiceWithTransactionCheckBox;
		private System.Windows.Forms.Label listPartialAuctionHitsLabel;
		private System.Windows.Forms.CheckBox listPartialAuctionCheckBox;
		private System.Windows.Forms.CheckBox findAllAuctionsCheckBox;
		private System.Windows.Forms.Label findAllAuctionsHitsLabel;
		private System.Windows.Forms.Label findHighBidsHitsLabel;
		private System.Windows.Forms.CheckBox findHighBidsCheckBox;
		private System.Windows.Forms.Label placeBidHitsLabel;
		private System.Windows.Forms.CheckBox placeBidCheckBox;
		private System.Windows.Forms.Label place2BidsHitsLabel;
		private System.Windows.Forms.CheckBox place2BidsCheckBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label totalHitslabel;
		private System.Windows.Forms.Button closeButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
			this.runTestsButton = new System.Windows.Forms.Button();
			this.urlTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listAuctionCheckBox = new System.Windows.Forms.CheckBox();
			this.clearAllButton = new System.Windows.Forms.Button();
			this.checkAllButton = new System.Windows.Forms.Button();
			this.resultsTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listAuctionHitsLabel = new System.Windows.Forms.Label();
			this.listAuctionTwiceWithoutTransactionHitsLabel = new System.Windows.Forms.Label();
			this.listAuctionTwiceWithoutTransactionCheckBox = new System.Windows.Forms.CheckBox();
			this.listAuctionTwiceWithTransactionHitsLabel = new System.Windows.Forms.Label();
			this.listAuctionTwiceWithTransactionCheckBox = new System.Windows.Forms.CheckBox();
			this.listPartialAuctionHitsLabel = new System.Windows.Forms.Label();
			this.listPartialAuctionCheckBox = new System.Windows.Forms.CheckBox();
			this.findAllAuctionsHitsLabel = new System.Windows.Forms.Label();
			this.findAllAuctionsCheckBox = new System.Windows.Forms.CheckBox();
			this.findHighBidsHitsLabel = new System.Windows.Forms.Label();
			this.findHighBidsCheckBox = new System.Windows.Forms.CheckBox();
			this.placeBidHitsLabel = new System.Windows.Forms.Label();
			this.placeBidCheckBox = new System.Windows.Forms.CheckBox();
			this.place2BidsHitsLabel = new System.Windows.Forms.Label();
			this.place2BidsCheckBox = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.totalHitslabel = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// runTestsButton
			// 
			this.runTestsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.runTestsButton.Location = new System.Drawing.Point(16, 328);
			this.runTestsButton.Name = "runTestsButton";
			this.runTestsButton.Size = new System.Drawing.Size(344, 32);
			this.runTestsButton.TabIndex = 0;
			this.runTestsButton.Text = "Run Selected Tests";
			this.runTestsButton.Click += new System.EventHandler(this.runTestsButton_Click);
			// 
			// urlTextBox
			// 
			this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.urlTextBox.Location = new System.Drawing.Point(16, 32);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(344, 20);
			this.urlTextBox.TabIndex = 1;
			this.urlTextBox.Text = "http://localhost/NTorpedo/AuctionService.asmx";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Url to NTorpedo Web Service:";
			// 
			// listAuctionCheckBox
			// 
			this.listAuctionCheckBox.Checked = true;
			this.listAuctionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.listAuctionCheckBox.Location = new System.Drawing.Point(16, 96);
			this.listAuctionCheckBox.Name = "listAuctionCheckBox";
			this.listAuctionCheckBox.Size = new System.Drawing.Size(192, 24);
			this.listAuctionCheckBox.TabIndex = 3;
			this.listAuctionCheckBox.Text = "ListAuction";
			// 
			// clearAllButton
			// 
			this.clearAllButton.Location = new System.Drawing.Point(16, 64);
			this.clearAllButton.Name = "clearAllButton";
			this.clearAllButton.TabIndex = 4;
			this.clearAllButton.Text = "Clear all";
			this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
			// 
			// checkAllButton
			// 
			this.checkAllButton.Location = new System.Drawing.Point(96, 64);
			this.checkAllButton.Name = "checkAllButton";
			this.checkAllButton.TabIndex = 5;
			this.checkAllButton.Text = "Check all";
			this.checkAllButton.Click += new System.EventHandler(this.checkAllButton_Click);
			// 
			// resultsTextBox
			// 
			this.resultsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.resultsTextBox.Location = new System.Drawing.Point(16, 392);
			this.resultsTextBox.Multiline = true;
			this.resultsTextBox.Name = "resultsTextBox";
			this.resultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.resultsTextBox.Size = new System.Drawing.Size(344, 184);
			this.resultsTextBox.TabIndex = 6;
			this.resultsTextBox.Text = "";
			this.resultsTextBox.WordWrap = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 376);
			this.label2.Name = "label2";
			this.label2.TabIndex = 7;
			this.label2.Text = "Results:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(264, 72);
			this.label3.Name = "label3";
			this.label3.TabIndex = 8;
			this.label3.Text = "Hits:";
			// 
			// listAuctionHitsLabel
			// 
			this.listAuctionHitsLabel.Location = new System.Drawing.Point(264, 104);
			this.listAuctionHitsLabel.Name = "listAuctionHitsLabel";
			this.listAuctionHitsLabel.TabIndex = 9;
			this.listAuctionHitsLabel.Text = "0";
			// 
			// listAuctionTwiceWithoutTransactionHitsLabel
			// 
			this.listAuctionTwiceWithoutTransactionHitsLabel.Location = new System.Drawing.Point(264, 152);
			this.listAuctionTwiceWithoutTransactionHitsLabel.Name = "listAuctionTwiceWithoutTransactionHitsLabel";
			this.listAuctionTwiceWithoutTransactionHitsLabel.TabIndex = 11;
			this.listAuctionTwiceWithoutTransactionHitsLabel.Text = "0";
			// 
			// listAuctionTwiceWithoutTransactionCheckBox
			// 
			this.listAuctionTwiceWithoutTransactionCheckBox.Checked = true;
			this.listAuctionTwiceWithoutTransactionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.listAuctionTwiceWithoutTransactionCheckBox.Location = new System.Drawing.Point(16, 144);
			this.listAuctionTwiceWithoutTransactionCheckBox.Name = "listAuctionTwiceWithoutTransactionCheckBox";
			this.listAuctionTwiceWithoutTransactionCheckBox.Size = new System.Drawing.Size(208, 24);
			this.listAuctionTwiceWithoutTransactionCheckBox.TabIndex = 10;
			this.listAuctionTwiceWithoutTransactionCheckBox.Text = "ListAuctionTwiceWithoutTransaction";
			// 
			// listAuctionTwiceWithTransactionHitsLabel
			// 
			this.listAuctionTwiceWithTransactionHitsLabel.Location = new System.Drawing.Point(264, 128);
			this.listAuctionTwiceWithTransactionHitsLabel.Name = "listAuctionTwiceWithTransactionHitsLabel";
			this.listAuctionTwiceWithTransactionHitsLabel.TabIndex = 13;
			this.listAuctionTwiceWithTransactionHitsLabel.Text = "0";
			// 
			// listAuctionTwiceWithTransactionCheckBox
			// 
			this.listAuctionTwiceWithTransactionCheckBox.Checked = true;
			this.listAuctionTwiceWithTransactionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.listAuctionTwiceWithTransactionCheckBox.Location = new System.Drawing.Point(16, 120);
			this.listAuctionTwiceWithTransactionCheckBox.Name = "listAuctionTwiceWithTransactionCheckBox";
			this.listAuctionTwiceWithTransactionCheckBox.Size = new System.Drawing.Size(208, 24);
			this.listAuctionTwiceWithTransactionCheckBox.TabIndex = 12;
			this.listAuctionTwiceWithTransactionCheckBox.Text = "ListAuctionTwiceWithTransaction";
			// 
			// listPartialAuctionHitsLabel
			// 
			this.listPartialAuctionHitsLabel.Location = new System.Drawing.Point(264, 176);
			this.listPartialAuctionHitsLabel.Name = "listPartialAuctionHitsLabel";
			this.listPartialAuctionHitsLabel.TabIndex = 15;
			this.listPartialAuctionHitsLabel.Text = "0";
			// 
			// listPartialAuctionCheckBox
			// 
			this.listPartialAuctionCheckBox.Checked = true;
			this.listPartialAuctionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.listPartialAuctionCheckBox.Location = new System.Drawing.Point(16, 168);
			this.listPartialAuctionCheckBox.Name = "listPartialAuctionCheckBox";
			this.listPartialAuctionCheckBox.Size = new System.Drawing.Size(208, 24);
			this.listPartialAuctionCheckBox.TabIndex = 14;
			this.listPartialAuctionCheckBox.Text = "ListPartialAuction";
			// 
			// findAllAuctionsHitsLabel
			// 
			this.findAllAuctionsHitsLabel.Location = new System.Drawing.Point(264, 200);
			this.findAllAuctionsHitsLabel.Name = "findAllAuctionsHitsLabel";
			this.findAllAuctionsHitsLabel.TabIndex = 17;
			this.findAllAuctionsHitsLabel.Text = "0";
			// 
			// findAllAuctionsCheckBox
			// 
			this.findAllAuctionsCheckBox.Checked = true;
			this.findAllAuctionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.findAllAuctionsCheckBox.Location = new System.Drawing.Point(16, 192);
			this.findAllAuctionsCheckBox.Name = "findAllAuctionsCheckBox";
			this.findAllAuctionsCheckBox.Size = new System.Drawing.Size(208, 24);
			this.findAllAuctionsCheckBox.TabIndex = 16;
			this.findAllAuctionsCheckBox.Text = "FindAllAuctions";
			// 
			// findHighBidsHitsLabel
			// 
			this.findHighBidsHitsLabel.Location = new System.Drawing.Point(264, 224);
			this.findHighBidsHitsLabel.Name = "findHighBidsHitsLabel";
			this.findHighBidsHitsLabel.TabIndex = 19;
			this.findHighBidsHitsLabel.Text = "0";
			// 
			// findHighBidsCheckBox
			// 
			this.findHighBidsCheckBox.Checked = true;
			this.findHighBidsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.findHighBidsCheckBox.Location = new System.Drawing.Point(16, 216);
			this.findHighBidsCheckBox.Name = "findHighBidsCheckBox";
			this.findHighBidsCheckBox.Size = new System.Drawing.Size(208, 24);
			this.findHighBidsCheckBox.TabIndex = 18;
			this.findHighBidsCheckBox.Text = "FindHighBids";
			// 
			// placeBidHitsLabel
			// 
			this.placeBidHitsLabel.Location = new System.Drawing.Point(264, 248);
			this.placeBidHitsLabel.Name = "placeBidHitsLabel";
			this.placeBidHitsLabel.TabIndex = 24;
			this.placeBidHitsLabel.Text = "0";
			// 
			// placeBidCheckBox
			// 
			this.placeBidCheckBox.Checked = true;
			this.placeBidCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.placeBidCheckBox.Location = new System.Drawing.Point(16, 240);
			this.placeBidCheckBox.Name = "placeBidCheckBox";
			this.placeBidCheckBox.Size = new System.Drawing.Size(208, 24);
			this.placeBidCheckBox.TabIndex = 23;
			this.placeBidCheckBox.Text = "PlaceBid";
			// 
			// place2BidsHitsLabel
			// 
			this.place2BidsHitsLabel.Location = new System.Drawing.Point(264, 272);
			this.place2BidsHitsLabel.Name = "place2BidsHitsLabel";
			this.place2BidsHitsLabel.TabIndex = 26;
			this.place2BidsHitsLabel.Text = "0";
			// 
			// place2BidsCheckBox
			// 
			this.place2BidsCheckBox.Checked = true;
			this.place2BidsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.place2BidsCheckBox.Location = new System.Drawing.Point(16, 264);
			this.place2BidsCheckBox.Name = "place2BidsCheckBox";
			this.place2BidsCheckBox.Size = new System.Drawing.Size(208, 24);
			this.place2BidsCheckBox.TabIndex = 25;
			this.place2BidsCheckBox.Text = "Place2Bids";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(184, 296);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 23);
			this.label5.TabIndex = 27;
			this.label5.Text = "Total:";
			// 
			// totalHitslabel
			// 
			this.totalHitslabel.Location = new System.Drawing.Point(264, 296);
			this.totalHitslabel.Name = "totalHitslabel";
			this.totalHitslabel.TabIndex = 28;
			this.totalHitslabel.Text = "0";
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.Location = new System.Drawing.Point(288, 592);
			this.closeButton.Name = "closeButton";
			this.closeButton.TabIndex = 29;
			this.closeButton.Text = "Close";
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 622);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.totalHitslabel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.place2BidsHitsLabel);
			this.Controls.Add(this.place2BidsCheckBox);
			this.Controls.Add(this.placeBidHitsLabel);
			this.Controls.Add(this.placeBidCheckBox);
			this.Controls.Add(this.findHighBidsHitsLabel);
			this.Controls.Add(this.findHighBidsCheckBox);
			this.Controls.Add(this.findAllAuctionsHitsLabel);
			this.Controls.Add(this.findAllAuctionsCheckBox);
			this.Controls.Add(this.listPartialAuctionHitsLabel);
			this.Controls.Add(this.listPartialAuctionCheckBox);
			this.Controls.Add(this.listAuctionTwiceWithTransactionHitsLabel);
			this.Controls.Add(this.listAuctionTwiceWithTransactionCheckBox);
			this.Controls.Add(this.listAuctionTwiceWithoutTransactionHitsLabel);
			this.Controls.Add(this.listAuctionTwiceWithoutTransactionCheckBox);
			this.Controls.Add(this.resultsTextBox);
			this.Controls.Add(this.listAuctionCheckBox);
			this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.listAuctionHitsLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkAllButton);
			this.Controls.Add(this.clearAllButton);
			this.Controls.Add(this.runTestsButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Name = "Form1";
			this.Text = "NTorpedo Test Client";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void RunSelectedTests()
		{
			resultsTextBox.Text = "";

			listAuctionHitsLabel.Text = "0";
			listAuctionTwiceWithTransactionHitsLabel.Text = "0";
			listAuctionTwiceWithoutTransactionHitsLabel.Text = "0";
			listPartialAuctionHitsLabel.Text = "0";
			findAllAuctionsHitsLabel.Text = "0";
			findHighBidsHitsLabel.Text = "0";
			placeBidHitsLabel.Text = "0";
			place2BidsHitsLabel.Text = "0";

			if (listAuctionCheckBox.Checked)
				RunListAuctionTest();
			if (listAuctionTwiceWithTransactionCheckBox.Checked)
				RunListAuctionTwiceWithTransactionTest();
			if (listAuctionTwiceWithoutTransactionCheckBox.Checked)
				RunListAuctionTwiceWithoutTransactionTest();
			if (listPartialAuctionCheckBox.Checked)
				RunListPartialAuctionTest();
			if (findAllAuctionsCheckBox.Checked)
				RunFindAllAuctionsTest();
			if (findHighBidsCheckBox.Checked)
				RunFindHighBidsTest();
			if (placeBidCheckBox.Checked)
				RunPlaceBidTest();
			if (place2BidsCheckBox.Checked)
				RunPlace2BidsTest();

			int total = 0;
			total += Int32.Parse(listAuctionHitsLabel.Text);
			total += Int32.Parse(listAuctionTwiceWithTransactionHitsLabel.Text);
			total += Int32.Parse(listAuctionTwiceWithoutTransactionHitsLabel.Text);
			total += Int32.Parse(listPartialAuctionHitsLabel.Text);
			total += Int32.Parse(findAllAuctionsHitsLabel.Text);
			total += Int32.Parse(findHighBidsHitsLabel.Text);
			total += Int32.Parse(placeBidHitsLabel.Text);
			total += Int32.Parse(place2BidsHitsLabel.Text);

			totalHitslabel.Text = total.ToString(); 

		}

		private int RunTest(ITest test)
		{
			test.RunTest()  ;
			resultsTextBox.Text += test.TestResult + Environment.NewLine + Environment.NewLine;
			return test.TestScore;
		}

		private void RunListAuctionTest()
		{
			ITest test = new ListAuction(urlTextBox.Text) ;
			listAuctionHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunListAuctionTwiceWithTransactionTest()
		{
			ITest test = new ListAuctionTwiceWithTransaction(urlTextBox.Text) ;
			listAuctionTwiceWithTransactionHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunListAuctionTwiceWithoutTransactionTest()
		{
			ITest test = new ListAuctionTwiceWithoutTransaction(urlTextBox.Text) ;
			listAuctionTwiceWithoutTransactionHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunListPartialAuctionTest()
		{
			ITest test = new ListPartialAuction(urlTextBox.Text) ;
			listPartialAuctionHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunFindAllAuctionsTest()
		{
			ITest test = new FindAllAuctions(urlTextBox.Text) ;
			findAllAuctionsHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunFindHighBidsTest()
		{
			ITest test = new FindHighBids(urlTextBox.Text) ;
			findHighBidsHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunPlaceBidTest()
		{
			ITest test = new PlaceBid(urlTextBox.Text) ;
			placeBidHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void RunPlace2BidsTest()
		{
			ITest test = new Place2Bids(urlTextBox.Text) ;
			place2BidsHitsLabel.Text = RunTest(test).ToString() ;
		}

		private void runTestsButton_Click(object sender, System.EventArgs e)
		{
			RunSelectedTests() ; 		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}


		private void clearAllButton_Click(object sender, System.EventArgs e)
		{
			listAuctionCheckBox.Checked = false;
			listAuctionTwiceWithTransactionCheckBox.Checked = false;
			listAuctionTwiceWithoutTransactionCheckBox.Checked = false;
			listPartialAuctionCheckBox.Checked = false;
			findAllAuctionsCheckBox.Checked = false;
			findHighBidsCheckBox.Checked = false;
			placeBidCheckBox.Checked = false;
			place2BidsCheckBox.Checked = false;
		}

		private void checkAllButton_Click(object sender, System.EventArgs e)
		{
			listAuctionCheckBox.Checked = true;
			listAuctionTwiceWithTransactionCheckBox.Checked = true;
			listAuctionTwiceWithoutTransactionCheckBox.Checked = true;
			listPartialAuctionCheckBox.Checked = true;
			findAllAuctionsCheckBox.Checked = true;
			findHighBidsCheckBox.Checked = true;		
			placeBidCheckBox.Checked = true;
			place2BidsCheckBox.Checked = true;
		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close() ;
		}


	}
}
