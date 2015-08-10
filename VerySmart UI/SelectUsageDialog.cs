using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Thesaurus;

namespace VerySmart_UI
{
    public partial class SelectUsageDialog : Form
    {
        private const string InfoText = "Multiple usages have been found for \"{0}\".";

        public IUsage SelectedUsage { get; private set; }

        private readonly List<IUsage> _usages;

        public SelectUsageDialog(string term, List<IUsage> usages)
        {
            _usages = usages;
            InitializeComponent();
            infoLabel.Text = string.Format( InfoText, term );

            for (var i = 0; i < _usages.Count; i++)
            {
                var usage = _usages[i];
                var data = new[]
                {
                    usage.Text,
                    usage.Type.ToString(),
                    i.ToString()
                };
                var item = new ListViewItem( data );
                usagesListView.Items.Add( item );
            }
            usagesListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );
        }

        private void usagesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MarkSelectedUsage();
        }

        private void usagesListView_ItemActivate(object sender, System.EventArgs e)
        {
            MarkSelectedUsage();
        }

        private void MarkSelectedUsage()
        {
            var item = usagesListView.SelectedItems[0];
            var index = usagesListView.Items.IndexOf( item );
            SelectedUsage = _usages[index];
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void dontUseASynonymBtn_Click(object sender, System.EventArgs e)
        {
            Dispose();
        }

        private void SelectUsageDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Escape )
            {
                dontUseASynonymBtn_Click( sender, e );
            }
        }
    }
}
