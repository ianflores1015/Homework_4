/* UserInterface.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ksu.Cis300.NameLookup
{
    /// <summary>
    /// A GUI for a program that looks up information on names.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// The information for each name.
        /// </summary>
        private Dictionary<string, FrequencyAndRank> _nameInformation = new Dictionary<string, FrequencyAndRank>();

        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reads the given input file into a dictionary.
        /// </summary>
        /// <param name="fn">The name of the file to read.</param>
        /// <returns>A dictionary whose keys are the names from the file and whose values give the frequency and 
        /// rank for each name.</returns>
        private static Dictionary<string, FrequencyAndRank> ReadFile(string fn)
        {
            Dictionary<string, FrequencyAndRank> d = new Dictionary<string, FrequencyAndRank>();
            using (StreamReader input = new StreamReader(fn))
            {
                while (!input.EndOfStream)
                {
                    string name = input.ReadLine().Trim();
                    float freq = Convert.ToSingle(input.ReadLine());
                    int rank = Convert.ToInt32(input.ReadLine());
                    d.Add(name, new FrequencyAndRank(freq, rank));
                }
            }
            return d;
        }

        /// <summary>
        /// Handles a Click event on the "Get Statistics" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLookup_Click(object sender, EventArgs e)
        {
            string name = uxName.Text.Trim().ToUpper();
            if (_nameInformation.TryGetValue(name, out FrequencyAndRank info))
            {
                uxFrequency.Text = info.Frequency.ToString();
                uxRank.Text = info.Rank.ToString();
            }
            else
            {
                MessageBox.Show("Name not found.");
                uxFrequency.Text = "";
                uxRank.Text = "";
            }
        }

        /// <summary>
        /// Handles a Click event on the "Remove" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxRemove_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles a Click event on the "Save" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSave_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler for the Open raw data file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openRawDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _nameInformation = ReadFile(uxOpenDialog.FileName);
                    MessageBox.Show("Number of elements: " + "/n" + "Secondary table length: "); 
                    //AddLater:  Add the numbers to the message box
                    uxSaveHashTableToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }



        /// <summary>
        /// The event handler for the Save Hash Table button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveHashTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxSaveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    using (FileStream output = new FileStream(uxSaveDialog.FileName, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        foreach (KeyValuePair<string, FrequencyAndRank> p in _nameInformation)
                        {
                            formatter.Serialize(output, p.Key);
                            formatter.Serialize(output, p.Value.Frequency);
                            formatter.Serialize(output, p.Value.Rank);
                        }
                    }
                    MessageBox.Show("File written.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// The event handler for the Open Hash Table Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openHashTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(uxOpenHashTableDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    using (FileStream input = new FileStream(uxOpenHashTableDialog.FileName, FileMode.Open))
                    {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FrequencyAndRank holder = (FrequencyAndRank)formatter.Deserialize(input); //Error?
                    //Holder should be the new data set?
                    }
                    MessageBox.Show("Hash table successfully read");
                    uxSaveHashTableToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //AddLater: Need Special Error statements
                }
            }
        }
    }
}
