using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Thesaurus;
using VerySmart_Core;

namespace VerySmart_UI
{
    public partial class Main : Form
    {
        private readonly VerySmartGenerator _generator;

        public Main()
        {
            InitializeComponent();
            _generator = new VerySmartGenerator();
            _generator.UsageResolver = AskUserForCorrectUsage;
            _generator.WordMadeSmart += word => progressBar.Value += 1;
        }

        private void makeMeVerySmartBtn_Click(object sender, EventArgs e)
        {
            var text = inputTextBox.Text;
            var terms = text.Split( ' ' );
            progressBar.Maximum = terms.Length;
            progressBar.Value = 0;

            var synonymSelection = synonymSelectionBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

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
            var dialog = new SelectUsageDialog( term, usages );
            var dialogResult = dialog.ShowDialog( this );
            if ( dialogResult == DialogResult.Cancel )
            {
                return new DummyUsage();
            }

            return dialog.SelectedUsage;
        }
    }

    internal class DummyUsage : IUsage
    {
        public string Text { get; } = "Dummy";
        public WordType Type { get; } = WordType.Unknown;
        public IReadOnlyList<string> Synonyms { get; } = new List<string>();
    }
}
