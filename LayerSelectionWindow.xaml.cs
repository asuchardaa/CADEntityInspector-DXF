using System.Collections.Generic;
using System.Windows;

namespace DxfLoad_Layers_Dialog
{
    /// <summary>
    /// Interakční logika pro LayerSelectionWindow.xaml
    /// Okno pro zvolení všech načtených vrstev z DXf souboru
    /// </summary>
    public partial class LayerSelectionWindow : Window
    {
        public string SelectedLayer { get; private set; }

        public LayerSelectionWindow(List<string> layerNames)
        {
            InitializeComponent();
            LayerListBox.ItemsSource = layerNames;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (LayerListBox.SelectedItem != null)
            {
                SelectedLayer = LayerListBox.SelectedItem.ToString();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Prosím, vyberte vrstvu.");
            }
        }
    }
}
