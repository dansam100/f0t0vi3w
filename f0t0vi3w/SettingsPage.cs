using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using f0t0vi3w3r.Properties;
using System.Configuration;
using f0t0vi3w3r.Tools;
using Forms = System.Windows.Forms;

namespace f0t0vi3w3r.ProgramSettings
{
    public partial class SettingsPage : Form
    {
        private static Dictionary<string, string> settings;
        internal const string SHOWTINYIMAGE = "ShowTinyImage";
        internal const string REPEATSLIDESHOW = "RepeatSlideShow";
        internal const string STARTMAXIMIZED = "StartMaximized";
        internal const string AUTONAVIGATION = "AutoNavigation";
        internal const string AUTOREPEATDELAY = "AutoRepeatDelay";
        internal const string TABCHARACTERS = "TabCharacters";
        internal const string HASHSIZE = "HashSize";
        internal const string CLONEBOXNAVIGATION = "CloneBoxNavigation";
        internal const string DEFAULTORIENTATION = "DefaultOrientation";
        internal const string SHOWBORDER = "ShowBorder";
        internal const string SHOWMENUBAR = "ShowMenuBar";
        internal const string SLIDESHOWDELAY = "SlideShowDelay";
        internal const string RANDOMIZESLIDESHOW = "RandomizeSlideShow";
        
        /// <summary>
        /// Ctor
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Event: Occurs when the setting page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SettingsPage_Load(object sender, System.EventArgs e)
        {
            settings = new Dictionary<string, string>(f0t0Box.SettingValues);
            this.LoadSettings();
        }

        /// <summary>
        /// Loads the current settings into the settings box for viewing and changing
        /// Updates controls with setting values
        /// </summary>
        private void LoadSettings()
        {
            foreach (KeyValuePair<string, string> kvp in settings)
            {
                string s = kvp.Key;
                switch (s)
                {
                    case SHOWTINYIMAGE:
                        this.ShowTinyImage.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case REPEATSLIDESHOW:
                        this.repeatCheckBox.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case STARTMAXIMIZED:
                        this.StartMaximized.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case AUTONAVIGATION:
                        this.AutoNavigation.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case AUTOREPEATDELAY:
                        this.AutoRepeatDelay.Text = settings[s];
                        break;
                    case TABCHARACTERS:
                        this.TabCharacters.Text = settings[s];
                        break;
                    case HASHSIZE:
                        this.HashSize.Text = settings[s];
                        break;
                    case CLONEBOXNAVIGATION:
                        this.CloneBoxNavigation.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case DEFAULTORIENTATION:
                        this.HorizontalOrientation.Checked = (settings[s] == Orientation.Horizontal.ToString());
                        this.VerticalOrientation.Checked = (settings[s] == Orientation.Vertical.ToString());
                        break;
                    case SHOWBORDER:
                        this.ShowBorder.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case SHOWMENUBAR:
                        this.ShowMenuBar.Checked = Convert.ToBoolean(settings[s]);
                        break;
                    case SLIDESHOWDELAY:
                        this.SlideShowDelay.Text = settings[s];
                        break;
                    case RANDOMIZESLIDESHOW:
                        this.checkBoxRandomize.Checked = Convert.ToBoolean(settings[s]);
                        break;
                }
            }
            this.Extensions.ClearSelected();
            this.Extensions.Items.AddRange(ExtensionHandler.GetSearchOptionFormat());
        }

        /// <summary>
        /// Event: Occurs when the slideshow trackbar is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void slideShowtrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            if (int.Parse(this.SlideShowDelay.Text) != this.slideShowtrackBar.Value)
            {
                this.SlideShowDelay.Text = System.Convert.ToString(this.slideShowtrackBar.Value);
            }
        }

        /// <summary>
        /// Event: Occurs when the autorepeat trackbar is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void autoNavDelaytrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            if (int.Parse(this.AutoRepeatDelay.Text) != this.autoNavDelaytrackBar.Value)
                this.AutoRepeatDelay.Text = System.Convert.ToString((this.autoNavDelaytrackBar.Value * 100));
        }

        /// <summary>
        /// Event: Occurs when the slideshow delay textbox value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SlideShowDelay_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (int.Parse(this.SlideShowDelay.Text) > this.slideShowtrackBar.Maximum)
                    this.SlideShowDelay.Text = this.slideShowtrackBar.Maximum.ToString();
                else if (int.Parse(this.SlideShowDelay.Text) < this.slideShowtrackBar.Minimum)
                    this.SlideShowDelay.Text = this.slideShowtrackBar.Minimum.ToString();
                else this.slideShowtrackBar.Value = int.Parse(this.SlideShowDelay.Text);
            }
            catch
            {
                this.SlideShowDelay.Text = System.Convert.ToString(this.slideShowtrackBar.Value);
            }
        }

        /// <summary>
        /// Event: Occurs when the autorepeat delay textbox value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AutoRepeatDelay_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (int.Parse(this.AutoRepeatDelay.Text) / 100 > this.autoNavDelaytrackBar.Maximum)
                    this.AutoRepeatDelay.Text = this.autoNavDelaytrackBar.Maximum.ToString();
                else if (int.Parse(this.AutoRepeatDelay.Text) / 100 < this.autoNavDelaytrackBar.Minimum)
                    this.AutoRepeatDelay.Text = this.autoNavDelaytrackBar.Minimum.ToString();
                else this.autoNavDelaytrackBar.Value = int.Parse(this.AutoRepeatDelay.Text) / 100;
            }
            catch
            {
                this.AutoRepeatDelay.Text = System.Convert.ToString((this.autoNavDelaytrackBar.Value * 100));
            }
        }

        /// <summary>
        /// Event: Occurs when an extension it to be added to the extension list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void addExtlinkLabel_Click(object sender, System.EventArgs e)
        {
            if (this.extTextBox.Text.Trim().Length > 0)
            {
                string[] exts = extTextBox.Text.Trim().Split(';');
                string duplicates = string.Empty;
                foreach (string s in exts)
                {
                    List<string> extensions = new List<string>(ExtensionHandler.GetSearchOptionFormat());
                    string ext = null;
                    if (s.IndexOf('.') > 0)
                    {
                        string[] extA = s.Split('.');
                        string extension = string.Format("*.{0}", extA[1].ToLower());
                        if (!extensions.Contains(extension))
                        {
                            ext = extension;
                        }
                        else
                        {
                            duplicates += string.Format("{0} ", extension);
                        }
                    }
                    else
                    {
                        string extension = string.Format("*.{0}", s.ToLower());
                        if (!extensions.Contains(extension))
                        {
                            ext = extension;
                        }
                        else
                        {
                            duplicates += string.Format("{0} ", extension);
                        }
                    }
                    if (ext != null)
                    {
                        Settings.Default.Extensions += string.Format("/{0}", ext);
                        this.Extensions.Items.Clear();
                        this.Extensions.Items.AddRange(ExtensionHandler.GetSearchOptionFormat());
                    }
                }
                if(duplicates.Length > 0)
                    Forms.MessageBox.Show("Extensions (" + duplicates + ") already exist!", "Duplicates",
                     System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Event: Occurs when the "OK" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void okButton_Click(object sender, System.EventArgs e)
        {
            this.UpdateSettings();
            Settings.Default.Save();
            this.Close();
        }


        /// <summary>
        /// Update all settings that have been changed
        /// </summary>
        private void UpdateSettings()
        {
            foreach (KeyValuePair<string, string> kvp in settings)
            {
                string s = kvp.Key;
                switch (s)
                {
                    case SHOWTINYIMAGE:
                        if (Convert.ToBoolean(settings[s]) != this.ShowTinyImage.Checked)
                            Settings.Default.ShowTinyImage = this.ShowTinyImage.Checked;
                        break;
                    case REPEATSLIDESHOW:
                        if (repeatCheckBox.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.RepeatSlideShow = this.repeatCheckBox.Checked;
                        break;
                    case STARTMAXIMIZED:
                        if (this.StartMaximized.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.StartMaximized = this.StartMaximized.Checked;
                        break;
                    case AUTONAVIGATION:
                        if (this.AutoNavigation.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.AutoNavigation = this.AutoNavigation.Checked;
                        break;
                    case AUTOREPEATDELAY:
                        if (this.AutoRepeatDelay.Text.CompareTo(settings[s]) != 0)
                            Settings.Default.AutoRepeatDelay = Convert.ToUInt32(this.AutoRepeatDelay.Text);
                        break;
                    case TABCHARACTERS:
                        if (this.TabCharacters.Text.CompareTo(settings[s]) != 0)
                            Settings.Default.TabCharacters = Convert.ToUInt32(this.TabCharacters.Text);
                        break;
                    case HASHSIZE:
                        if (this.HashSize.Text.CompareTo(settings[s]) != 0)
                            Settings.Default.HashSize = Convert.ToUInt32(this.HashSize.Text);
                        break;
                    case CLONEBOXNAVIGATION:
                        if (this.CloneBoxNavigation.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.CloneBoxNavigation = this.CloneBoxNavigation.Checked;
                        break;
                    case DEFAULTORIENTATION:
                        if (this.HorizontalOrientation.Checked && settings[s] != Orientation.Horizontal.ToString())
                            Settings.Default.DefaultOrientation = Orientation.Horizontal;
                        if (this.VerticalOrientation.Checked && settings[s] != Orientation.Vertical.ToString())
                            Settings.Default.DefaultOrientation = Orientation.Vertical;
                        break;
                    case SHOWBORDER:
                        if (this.ShowBorder.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.ShowBorder = this.ShowBorder.Checked;
                        break;
                    case SHOWMENUBAR:
                        if (this.ShowMenuBar.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.ShowMenuBar = this.ShowMenuBar.Checked;
                        break;
                    case SLIDESHOWDELAY:
                        if (this.SlideShowDelay.Text.CompareTo(settings[s]) != 0)
                            Settings.Default.SlideShowDelay = Convert.ToUInt32(this.SlideShowDelay.Text);
                        break;
                    case RANDOMIZESLIDESHOW:
                        if (this.checkBoxRandomize.Checked != Convert.ToBoolean(settings[s]))
                            Settings.Default.RandomizeSlideShow = this.checkBoxRandomize.Checked;
                        break;
                }
            }
        }
    }
}