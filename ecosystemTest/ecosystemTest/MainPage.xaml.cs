using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ecosystemTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GraphRootObject myGraphRoot;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void updateScreenData()
        {
            tblData.Text = tblData.Text + Environment.NewLine + "Data Retrieved...";

            for(int i = 0; i < myGraphRoot.graph.cells.Count(); i++ )
            {
                switch (myGraphRoot.graph.cells[i].celltype )
                {
                    case "actor":
                        {
                            tblData.Text = tblData.Text + Environment.NewLine +
                                            "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
                                            "size: " + myGraphRoot.graph.cells[i].size.width + ", " + myGraphRoot.graph.cells[i].size.height + Environment.NewLine +
                                            "outer fill: " + myGraphRoot.graph.cells[i].attrs.outer.fill + Environment.NewLine +
                                            "attrs text text:" + myGraphRoot.graph.cells[i].attrs.text.text + Environment.NewLine +
                                            "attrs text fill: " + myGraphRoot.graph.cells[i].attrs.text.fill + Environment.NewLine;
                            break;
                        }
                    case "relationship":
                        {
                            tblData.Text = tblData.Text + Environment.NewLine +
                                            "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
                                            "source id: " + myGraphRoot.graph.cells[i].source.id + Environment.NewLine +
                                            "target id: " + myGraphRoot.graph.cells[i].target.id + Environment.NewLine +
                                            "attrs text text: " + myGraphRoot.graph.cells[i].labels[0].attrs.text.text + Environment.NewLine;
                            break;
                        }
                    default:
                        break;
                }
            }


        }

        private async void loadData(string filePath)
        {
            if (myGraphRoot == null) myGraphRoot = new GraphRootObject();
            StorageFolder storageFolder;
            StorageFile storageFile;

            string text;
            try
            {
                storageFolder = ApplicationData.Current.LocalFolder;
                storageFile = await storageFolder.GetFileAsync("Simple.Json");
                text = await FileIO.ReadTextAsync(storageFile);

                //var result = JsonConvert.DeserializeObject<RootObject>(json);
                dynamic json = JsonConvert.DeserializeObject<GraphRootObject>(text);

                myGraphRoot = json;
            }
            catch (Exception ex)
            {
                tblData.Text = "Error: " + ex.Message;
            }

        }

        private void cmdData_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\DamienC\\Documents\\Simple.Json";
            tblData.Text = filePath;

            loadData(filePath);
            updateScreenData();

        }
    }
}
