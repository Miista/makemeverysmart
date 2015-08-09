using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using Thesaurus;
using VerySmart_Core;

namespace VerySmart_UI
{
    public partial class Main : Form
    {
        private readonly VerySmartGenerator _generator;

        private readonly IDictionary<string, IUsage> _history = new Dictionary<string, IUsage>();

        public Main()
        {
            InitializeComponent();
            _generator = new VerySmartGenerator();
            _generator.UsageResolver = AskUserForCorrectUsage;
            _generator.WordMadeSmart += word => progressBar.Value += 1;
        }

        private void makeMeVerySmartBtn_Click(object sender, EventArgs e)
        {
            _history.Clear();
            repeatLastBtn.Enabled = true;
            MakeMeVerySmart();
        }

        private void MakeMeVerySmart()
        {
            var text = inputTextBox.Text;
            var terms = text.Split( ' ' );
            progressBar.Maximum = terms.Length;
            progressBar.Value = 0;

            var synonymSelection = synonymSelectionBox.Controls.OfType<RadioButton>()
                                                      .FirstOrDefault( r => r.Checked );

            var options = new VerySmartOptions();
            if ( synonymSelection != null )
            {
                options.SynonymSelectionMode = synonymSelection.Text == SynonymSelectionMode.Longest.ToString()
                    ? SynonymSelectionMode.Longest
                    : SynonymSelectionMode.Random;
            }
            _generator.Options = options;

            var verysmartText = _generator.MakeMeVerySmart( text );

            outputTextBox.Text = verysmartText;
            progressBar.Value = 0;
        }

        private IUsage AskUserForCorrectUsage(string term, List<IUsage> usages)
        {
            IUsage usage = null;
            if ( _history.TryGetValue( term, out usage ) )
            {
                return usage;
            }

            var dialog = new SelectUsageDialog( term, usages );
            var dialogResult = dialog.ShowDialog( this );
            if ( dialogResult == DialogResult.Cancel )
            {
                _history[term] = null;
                return null;
            }
            _history[term] = dialog.SelectedUsage;

            return dialog.SelectedUsage;
        }

        private void repeatLastBtn_Click(object sender, EventArgs e)
        {
            MakeMeVerySmart();
        }

        private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // User entered something in the input field. Disable the "Again!!!" button
            repeatLastBtn.Enabled = false;
            _history.Clear();
        }

        private void speakItBtn_Click(object sender, EventArgs e)
        {
            using (var synth = new SpeechSynthesizer())
            {
                synth.SelectVoice( "Microsoft Zira Desktop" );
                synth.Speak( outputTextBox.Text );
            }
        }

        private void outputTextBox_TextChanged(object sender, EventArgs e) => speakItBtn.Enabled = !string.IsNullOrEmpty( outputTextBox.Text );
    }
}
