namespace KeePassFlatexPlugin
{
	partial class GenerateTokenForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateTokenForm));
			this.lblIndex = new System.Windows.Forms.Label();
			this.txtIndex = new System.Windows.Forms.TextBox();
			this.lblToken = new System.Windows.Forms.Label();
			this.txtToken = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblIndex
			// 
			this.lblIndex.AutoSize = true;
			this.lblIndex.Location = new System.Drawing.Point(12, 9);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(36, 13);
			this.lblIndex.TabIndex = 0;
			this.lblIndex.Text = "Index:";
			// 
			// txtIndex
			// 
			this.txtIndex.Location = new System.Drawing.Point(54, 6);
			this.txtIndex.Name = "txtIndex";
			this.txtIndex.Size = new System.Drawing.Size(263, 20);
			this.txtIndex.TabIndex = 1;
			this.txtIndex.TextChanged += new System.EventHandler(this.txtIndex_TextChanged);
			// 
			// lblToken
			// 
			this.lblToken.AutoSize = true;
			this.lblToken.Location = new System.Drawing.Point(12, 35);
			this.lblToken.Name = "lblToken";
			this.lblToken.Size = new System.Drawing.Size(41, 13);
			this.lblToken.TabIndex = 2;
			this.lblToken.Text = "Token:";
			// 
			// txtToken
			// 
			this.txtToken.Location = new System.Drawing.Point(54, 32);
			this.txtToken.Name = "txtToken";
			this.txtToken.ReadOnly = true;
			this.txtToken.Size = new System.Drawing.Size(263, 20);
			this.txtToken.TabIndex = 3;
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(323, 4);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnAutotype_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(323, 30);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// GenerateTokenForm
			// 
			this.AcceptButton = this.btnCopy;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(409, 64);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtToken);
			this.Controls.Add(this.lblToken);
			this.Controls.Add(this.txtIndex);
			this.Controls.Add(this.lblIndex);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GenerateTokenForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generate Flatex Token";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.TextBox txtIndex;
		private System.Windows.Forms.Label lblToken;
		private System.Windows.Forms.TextBox txtToken;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnClose;
	}
}