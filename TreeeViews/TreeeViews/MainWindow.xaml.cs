﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeeViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine 
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for it 
                var item = new TreeViewItem()
                {

                    // Set the header 
                    Header = drive,
                    // And the full path
                    Tag = drive

                };

                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded 
                item.Expanded += Item_Expanded;

                // Add it to the main tree view 
                FolderView.Items.Add(item);
            }
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial checks
            var item = (TreeViewItem)sender;

            // If the item only contains the dummy data 
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear the dummy data 
            item.Items.Clear();

            // Get the full path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // Create a blank list for the directories 
            var directories = new List<string>();

            // Try and get the directories from the folder 
            // Ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // For each directory...
            directories.ForEach(directoryPath =>
            {
                // Create directory item
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    // And tag as full path 
                    Tag = directoryPath
                };
                // Add dummy item so we can expand folder 
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += Item_Expanded;

                // Add this item to the parent 
                item.Items.Add(subItem);

            });
            #endregion

            #region Get Files

            // Create a blank list for the files 
            var files = new List<string>();

            // Try and get the directories from the folder 
            // Ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    directories.AddRange(fs);
            }
            catch { }

            // For each file...
            files.ForEach(filePath =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    // Set header as file name
                    Header = GetFileFolderName(filePath),
                    // And tag as full path 
                    Tag = filePath
                };

                // Add this item to the parent 
                item.Items.Add(subItem);

            });



            #endregion


        }

        public static string GetFileFolderName(string path)
        {
            // If we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            // Make all slashes back slashes 
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path 
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself 
            if (lastIndex <= 0)
                return path;

            // Return the name after the last back slash 
            return path.Substring(lastIndex + 1);

        }
    }
}
