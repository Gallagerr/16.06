using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _16._06
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Timer timer = new Timer(Save, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));
    }
    static void Save(object state)
    {
      var now = DateTime.Now;
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "*.txt";
      string filepath = @"Этот компьютер\Документы";
      FileStream file = new FileStream(filepath, FileMode.OpenOrCreate);
      new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Save(file, DataFormats.Text);
      string file2 = $@"C:\Users\{Environment.UserName}\tmp";
      using (var stream = File.Open(file2, FileMode.Create))
      {
        byte[] dataBytes = Encoding.UTF8.GetBytes(new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text);
        stream.Write(dataBytes, 0, dataBytes.Length);
      }
    }
    private void Create_Click(object sender, RoutedEventArgs e)
    {
      rtb.SelectAll();
      rtb.Selection.Text = "";
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == true)
        new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text = File.ReadAllText(openFileDialog.FileName);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Text File(*.txt)|*.txt";
      if (saveFileDialog.ShowDialog() == true)
        File.WriteAllText(saveFileDialog.FileName, new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text);
  
    }
  }
}
