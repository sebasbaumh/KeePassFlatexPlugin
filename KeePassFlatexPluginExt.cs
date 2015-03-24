using KeePass.Plugins;
using KeePass.Util;
using KeePassFlatexPlugin.Properties;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeePassFlatexPlugin
{
	public sealed class KeePassFlatexPluginExt : Plugin
	{
		private const String TITLE = "KeePassFlatexPlugin";
		private const String ITANTITLE = "<ITAN>";
		private const String ROWS = "ABCDEFGHKLM";
		//9x11
		private const int FLATEX_COLUMNS = 9;
		private const int FLATEX_ROWS = 11;
		public static Regex rToken = new Regex(@"([A-HK-M])([1-9])");
		private IPluginHost phHost = null;
		private ToolStripMenuItem tsmStart;

		public IPluginHost PluginHost
		{
			get
			{
				return phHost;
			}
		}

		public override System.Drawing.Image SmallIcon
		{
			get
			{
				return Resources.Flatex;
			}
		}

		public override bool Initialize(IPluginHost host)
		{
			phHost = host;
			//add a menu item
			MenuStrip msMain = phHost.MainWindow.MainMenu;
			tsmStart = new ToolStripMenuItem("Flatex");
			tsmStart.Click += tsmStart_Click;
			tsmStart.Visible = false;
			msMain.Items.Add(tsmStart);
			//wait for file opening
			phHost.MainWindow.FileClosed += MainWindow_FileClosed;
			phHost.MainWindow.FileOpened += MainWindow_FileOpened;
			phHost.MainWindow.DocumentManager.ActiveDocumentSelected += DocumentManager_ActiveDocumentSelected;
			return true;
		}

		/// <summary>
		/// Collects all tokens in the given group.
		/// </summary>
		/// <param name="group">group</param>
		/// <param name="d">map of tokens</param>
		private static void CollectTokens(PwGroup group, Dictionary<String, String> d)
		{
			if (group != null)
			{
				//check entries
				foreach (PwEntry e in group.Entries)
				{
					String title = e.Strings.Get(PwDefs.TitleField).ReadString();
					if (ITANTITLE.Equals(title))
					{
						//get token index
						String username = e.Strings.Get(PwDefs.UserNameField).ReadString();
						if (!String.IsNullOrEmpty(username))
						{
							//try to parse it
							int i;
							if (int.TryParse(username, out i))
							{
								int row = (i - 1) / FLATEX_COLUMNS;
								int col = i - 1 - (row * FLATEX_COLUMNS);
								if ((row >= 0) && (row < FLATEX_ROWS) && (col >= 0) && (col < FLATEX_COLUMNS))
								{
									String token = ROWS[row] + (col + 1).ToString();
									String value = e.Strings.Get(PwDefs.PasswordField).ReadString();
									d[token] = value;
								}
							}
						}
					}
				}
				//walk down
				foreach (PwGroup g in group.Groups)
				{
					CollectTokens(g, d);
				}
			}
		}

		/// <summary>
		/// Collects all tokens in the current database.
		/// </summary>
		/// <returns>map of tokens</returns>
		private Dictionary<String, String> CollectTokens()
		{
			Dictionary<String, String> d = new Dictionary<String, String>();
			PwDatabase db = phHost.MainWindow.ActiveDatabase;
			if (db != null)
			{
				CollectTokens(db.RootGroup, d);
			}
			return d;
		}

		/// <summary>
		/// Checks if the active database has iTanEntries.
		/// </summary>
		/// <param name="group">group (can be null)</param>
		/// <returns>true on success, else false</returns>
		private static bool DatabaseHasITanEntries(PwGroup group)
		{
			if (group != null)
			{
				//check entries
				foreach (PwEntry e in group.Entries)
				{
					String title = e.Strings.Get(PwDefs.TitleField).ReadString();
					if (ITANTITLE.Equals(title))
					{
						return true;
					}
				}
				//walk down
				foreach (PwGroup g in group.Groups)
				{
					if (DatabaseHasITanEntries(g))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if the active database has iTanEntries.
		/// </summary>
		/// <returns>true on success, else false</returns>
		public bool DatabaseHasITanEntries()
		{
			PwDatabase db = phHost.MainWindow.ActiveDatabase;
			if (db != null)
			{
				return DatabaseHasITanEntries(db.RootGroup);
			}
			return false;
		}

		public void RefreshState()
		{
			//search for iTan entries
			tsmStart.Visible = DatabaseHasITanEntries();
		}

		private void DocumentManager_ActiveDocumentSelected(object sender, EventArgs e)
		{
			try
			{
				RefreshState();
			}
			catch (Exception ex)
			{
				ShowError(null, ex);
			}
		}

		private void MainWindow_FileClosed(object sender, KeePass.Forms.FileClosedEventArgs e)
		{
			try
			{
				RefreshState();
			}
			catch (Exception ex)
			{
				ShowError(null, ex);
			}
		}

		private void MainWindow_FileOpened(object sender, KeePass.Forms.FileOpenedEventArgs e)
		{
			try
			{
				RefreshState();
			}
			catch (Exception ex)
			{
				ShowError(null, ex);
			}
		}

		private void tsmStart_Click(object sender, EventArgs e)
		{
			try
			{
				Dictionary<String, String> tokens = CollectTokens();
				GenerateTokenForm frm = new GenerateTokenForm(tokens);
				if (frm.ShowDialog() == DialogResult.OK)
				{
					//get token and construct temporary entry
					String token = frm.Token;
					PwEntry entry = new PwEntry(false, false);
					//create an empty title field to prevent bug in iTanMaster plugin
					entry.Strings.Set(PwDefs.TitleField, new KeePassLib.Security.ProtectedString(false, ""));
					entry.Strings.Set(PwDefs.PasswordField, new KeePassLib.Security.ProtectedString(true, token));
					AutoType.PerformIntoPreviousWindow(phHost.MainWindow, entry, phHost.Database, "{PASSWORD}{ENTER}");
				}
			}
			catch (Exception ex)
			{
				ShowError(null, ex);
			}
		}

		public static void ShowError(String msg, Exception ex)
		{
			if (msg != null)
			{
				msg = msg + "\n" + ex.Message + "@" + ex.StackTrace;
			}
			else if (ex != null)
			{
				msg = ex.Message + "@" + ex.StackTrace;
			}
			MessageBox.Show(msg, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

	}
}
