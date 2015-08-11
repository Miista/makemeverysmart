using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using Thesaurus;
using VerySmart_Core;
using WordComplexity = VerySmart_Core.WordComplexity;

namespace VerySmart_UI
{
    public partial class Main : Form
    {
        private readonly VerySmartGenerator _generator;

        private readonly IDictionary<string, IUsage> _history = new Dictionary<string, IUsage>();
        private readonly VerySmartOptions _options;
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

        public Main()
        {
            InitializeComponent();
            _generator = new VerySmartGenerator
            {
                UsageResolver = AskUserForCorrectUsage
            };
            _generator.WordMadeSmart += word => progressBar.Value += 1;
            _options = new VerySmartOptions();
            _synth.SelectVoice("Microsoft Zira Desktop");
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

            if ( synonymSelection != null )
            {
                _options.SynonymSelectionMode = synonymSelection.Text == SynonymSelectionMode.Longest.ToString()
                    ? SynonymSelectionMode.Longest
                    : SynonymSelectionMode.Random;
                _options.Complexity = GetComplexity();
            }
            _generator.Options = _options;

            var verysmartText = _generator.MakeMeVerySmart( text );

            outputTextBox.Text = verysmartText;
            progressBar.Value = 0;
        }

        private WordComplexity GetComplexity()
        {
            switch (complexityTrackBar.Value)
            {
                default:
                // ReSharper disable once RedundantCaseLabel
                case 0:
                    return WordComplexity.All;
                case 1:
                    return WordComplexity.LowComplexity;
                case 2:
                    return WordComplexity.MediumComplexity;
                case 3:
                    return WordComplexity.HighComplexity;
            }
        }

        private IUsage AskUserForCorrectUsage(string term, List<IUsage> usages)
        {
            IUsage usageFromHistory;
            if ( _history.TryGetValue( term, out usageFromHistory ) )
            {
                return usageFromHistory;
            }

            var usageSelectionDialog = new SelectUsageDialog( term, usages );
            var dialogResult = usageSelectionDialog.ShowDialog( this );
            if ( dialogResult == DialogResult.Cancel )
            {
                _history[term] = null;
                return null;
            }
            _history[term] = usageSelectionDialog.SelectedUsage;

            return usageSelectionDialog.SelectedUsage;
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
            _synth.SpeakAsync( outputTextBox.Text );
        }

        private void outputTextBox_TextChanged(object sender, EventArgs e) => speakItBtn.Enabled = !string.IsNullOrEmpty( outputTextBox.Text );
    }
}
