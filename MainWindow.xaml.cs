using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Course
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Variables();

        }
        private void Variables()
        {
            StandartModel stnMod = new StandartModel();
            NoiseModel noiseMod = new NoiseModel();
            PercolationModel perMod = new PercolationModel();
            ProbabilityPerSide probMod = new ProbabilityPerSide();
            TheStrongestGroup strgroup = new TheStrongestGroup();

            if (CheckPercolation.IsChecked == true) TextTime.Text = perMod.PercolationModels(int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text), int.Parse(TextsSize.Text), new Stopwatch(),
                                                                   0, new ArrayList(), new ArrayList(), new int[int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text)],
                                                                   new int[4] { 1 * int.Parse(TextsSize.Text), 0, -1 * int.Parse(TextsSize.Text), 0 },
                                                                   new int[4] { 0, -1 * int.Parse(TextsSize.Text), 0, 1 * int.Parse(TextsSize.Text) }, float.Parse(probabilityBlocking.Text));
            if (CheckNoiseModel.IsChecked == true) TextTime.Text = noiseMod.NoiseModels(int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text), int.Parse(TextsSize.Text), new Stopwatch(),
                                                                    int.Parse(TextCountColour.Text), true, 0,
                                                                    new ArrayList(), new ArrayList(), new int[int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text)],
                                                                    new int[4] { 1 * int.Parse(TextsSize.Text), 0, -1 * int.Parse(TextsSize.Text), 0 },
                                                                    new int[4] { 0, -1 * int.Parse(TextsSize.Text), 0, 1 * int.Parse(TextsSize.Text) }, float.Parse(countNoise.Text));
            if (ProbabilityPerSideCheck.IsChecked == true) TextTime.Text = probMod.ProbabilityPerSides(int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text), int.Parse(TextsSize.Text), new Stopwatch(),
                                                                    int.Parse(TextCountColour.Text), true, 0,
                                                                    new ArrayList(), new ArrayList(), new int[int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text)],
                                                                    new int[4] { 1 * int.Parse(TextsSize.Text), 0, -1 * int.Parse(TextsSize.Text), 0 },
                                                                    new int[4] { 0, -1 * int.Parse(TextsSize.Text), 0, 1 * int.Parse(TextsSize.Text) },
                                                                    double.Parse(top.Text), double.Parse(right.Text), double.Parse(bottom.Text), double.Parse(left.Text));
            if (StandartModelCheck.IsChecked == true) TextTime.Text = stnMod.StandartModels(int.Parse(WidthWindow.Text),
                                                                    int.Parse(HeightWindow.Text), int.Parse(TextsSize.Text), new Stopwatch(),
                                                                    int.Parse(TextCountColour.Text), true, 0,
                                                                    new ArrayList(), new ArrayList(), new int[int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text)],
                                                                    new int[4] { 1 * int.Parse(TextsSize.Text), 0, -1 * int.Parse(TextsSize.Text), 0 },
                                                                    new int[4] { 0, -1 * int.Parse(TextsSize.Text), 0, 1 * int.Parse(TextsSize.Text) });
            if (TheStrongestGroupsCheck.IsChecked == true) TextTime.Text = strgroup.TheStrongestGroups(int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text), int.Parse(TextsSize.Text), new Stopwatch(),
                                                                    int.Parse(TextCountColour.Text), new ArrayList(), new ArrayList(),
                                                                    new int[int.Parse(WidthWindow.Text), int.Parse(HeightWindow.Text)],
                                                                    new int[4] { 1 * int.Parse(TextsSize.Text), 0, -1 * int.Parse(TextsSize.Text), 0 },
                                                                    new int[4] { 0, -1 * int.Parse(TextsSize.Text), 0, 1 * int.Parse(TextsSize.Text) });
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            Variables();
        }
    }
}
