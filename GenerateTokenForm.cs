using KeePass.Util;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeePassFlatexPlugin
{
	/// <summary>
	/// Form for generating a token from a Flatex string.
	/// </summary>
	public partial class GenerateTokenForm : Form
	{
		private Dictionary<String, String> tokens;

		/// <summary>
		/// Selected token.
		/// </summary>
		public String Token
		{
			get;
			private set;
		}

		private GenerateTokenForm()
		{
			InitializeComponent();
		}

		public GenerateTokenForm(Dictionary<String, String> tokens)
			: this()
		{
			this.tokens = tokens;
			//get token from clipboard if set
			if (Clipboard.ContainsText())
			{
				String token = Clipboard.GetText();
				//check it
				if (KeePassFlatexPluginExt.rToken.IsMatch(token))
				{
					txtIndex.Text = token;
					GenerateToken();
				}
			}
		}

		private void GenerateToken()
		{
			String s = txtIndex.Text.ToUpper();
			StringBuilder sb = new StringBuilder();
			foreach (Match m in KeePassFlatexPluginExt.rToken.Matches(s))
			{
				String token;
				if (tokens.TryGetValue(m.Value, out token))
				{
					sb.Append(token);
				}
			}
			this.Token = sb.ToString();
			txtToken.Text = this.Token;
		}

		private void txtIndex_TextChanged(object sender, EventArgs e)
		{
			GenerateToken();
		}

		private void btnAutotype_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

	}
}
