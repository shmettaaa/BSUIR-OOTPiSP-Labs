using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Figures;

namespace FiguresApp
{
    public partial class TransformersSettingsWindow : Window
    {
        public IDataTransformer SelectedTransformer { get; private set; }

        public TransformersSettingsWindow(IDataTransformer current)
        {
            InitializeComponent();
            var list = TransformerRegistry.Instance.GetAll().ToList();
            lbTransformers.ItemsSource = list;
            // Pre-select current active transformer
            if (current != null)
            {
                var found = list.FirstOrDefault(t => t.GetType() == current.GetType() || t.Name == current.Name);
                if (found != null)
                    lbTransformers.SelectedItem = found;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            SelectedTransformer = lbTransformers.SelectedItem as IDataTransformer;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}