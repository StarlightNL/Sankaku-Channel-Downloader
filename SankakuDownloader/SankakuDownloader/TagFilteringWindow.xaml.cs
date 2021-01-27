using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SankakuDownloader
{
    /// <summary>
    /// Interaction logic for TagFilteringWindow.xaml
    /// </summary>
    public partial class TagFilteringWindow : Window
    {
        private MainViewModel viewModel;
        public TagFilteringWindow()
        {
            InitializeComponent();
            viewModel = (MainViewModel) this.DataContext;
            btnRemove.IsEnabled = false;
            btnEdit.IsEnabled = false;
        }

        new void GotFocus(object sender, RoutedEventArgs e) => ((TextBox)sender).SelectAll();

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            List<String> tags = new List<string>();
            String txtTagsStr = txtTags.Text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            if (String.IsNullOrWhiteSpace(txtTagsStr)) tags.Add(txtFoldername.Text);
            else tags = txtTagsStr.Split(' ').ToList();

            viewModel.TagFilters.Add(new TagFilter(){FolderName = txtFoldername.Text, Method = ((ComboBoxItem)cobMethod.SelectedItem).Content.ToString().ToLower(), Tags = tags});

            txtFoldername.Text = "";
            txtTags.Text = "";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TagFilters.Remove(viewModel.TagFilters.First(tf => tf.FolderName == txtFoldername.Text));

            List<String> tags = new List<string>();
            if (String.IsNullOrWhiteSpace(txtTags.Text)) tags.Add(txtFoldername.Text);
            else tags = txtTags.Text.Split(' ').ToList();

            viewModel.TagFilters.Add(new TagFilter() { FolderName = txtFoldername.Text, Method = ((ComboBoxItem)cobMethod.SelectedItem).Content.ToString().ToLower(), Tags = tags });
            btnRemove.IsEnabled = false;
            btnEdit.IsEnabled = false;
            txtFoldername.IsEnabled = true;
            txtFoldername.Text = "";
            txtTags.Text = "";
        }

        private void libTagFilters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tagFilter = (TagFilter)libTagFilters.SelectedItem;
            try
            {
                txtFoldername.Text = tagFilter.FolderName;
                txtTags.Text = String.Join(" ", tagFilter.Tags);
                foreach (ComboBoxItem item in cobMethod.Items)
                {
                    if (item.Content.ToString() == tagFilter.Method.ToUpper())
                    {
                        cobMethod.SelectedValue = item;
                        break;
                    }
                }

                btnRemove.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnCreate.IsEnabled = false;
                txtFoldername.IsEnabled = false;
            } catch(Exception){}
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TagFilters.Remove(viewModel.TagFilters.First(tf => tf.FolderName == txtFoldername.Text));
            btnRemove.IsEnabled = false;
            btnEdit.IsEnabled = false;
            txtFoldername.IsEnabled = true;
            txtFoldername.Text = "";
            txtTags.Text = "";
        }
    }
}
