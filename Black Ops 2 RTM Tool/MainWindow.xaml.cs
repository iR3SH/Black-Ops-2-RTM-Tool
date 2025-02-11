﻿using Black_Ops_2_RTM_Tool.Classes;
using PS3Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Black_Ops_2_RTM_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<Images>? PrestigeIcons { get { return _PrestigeIcons; } set { _PrestigeIcons = value; NotifyPropertyChanged(); } }
        private List<Images>? _PrestigeIcons { get; set; }
        public List<API>? APIS { get{ return _APIS; } set {_APIS = value; NotifyPropertyChanged(); } }
        private List<API>? _APIS { get; set; }

        private readonly PS3API API = new();
        DispatcherTimer TempTimer = new();
        Functions Function;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            APIS = [
                new() { DisplayName = "DEX", SelectAPI = SelectAPI.TargetManager },
                new() { DisplayName = "CEX", SelectAPI = SelectAPI.ControlConsole },
                new() { DisplayName = "HEN", SelectAPI = SelectAPI.Hen }
            ];
            UnlockTool(false);
            TempTimer.Tick += new EventHandler(TempTimerTickEvent);
            TempTimer.Interval = new TimeSpan(0, 1, 0);
            ConnexionButton.IsEnabled = false;
            AttachButton.IsEnabled = false;
        }
        public void UnlockTool(bool cond)
        {
            NoHostHeader.IsEnabled = cond;
            StatsHeader.IsEnabled = cond;
            CIDButton.IsEnabled = cond;
            RefreshTempCheck.IsEnabled = cond;
        }
        private void ChangeColorTemp(Label label, int temp)
        {

            if (temp < 70)
            {
                label.Foreground = Brushes.DarkGreen;
            }
            else if (temp > 70 && temp < 85)
            {
                label.Foreground = Brushes.Orange;
            }
            else
            {
                label.Foreground = Brushes.Red;
            }
        }
        private void GetTemperature()
        {
            int CellTemp = API.GetTemperatureCell();
            int RsxTemp = API.GetTemperatureRSX();
            TempCellLabel.Content = CellTemp + "°C";
            TempRSXLabel.Content = RsxTemp + " °C";

            ChangeColorTemp(TempCellLabel, CellTemp);
            ChangeColorTemp(TempRSXLabel, RsxTemp);
        }

        private void TempTimerTickEvent(object sender, EventArgs args)
        {
            GetTemperature();
        }

        private void ConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            if (IPTextBox.Text.Length != 0 && !IPTextBox.Text.Equals("192.168.1.1"))
            {
                if (API.ConnectTarget(IPTextBox.Text))
                {
                    ConnexionLabel.Content = "Connecté";
                    ConnexionLabel.Foreground = Brushes.DarkGreen;
                    API.Notify(NotifyIcon.INFO, "Connecte au RTM Tool Bo2");
                    API.RingBuzzer(BuzzerMode.Single);
                    GetTemperature();
                    AttachButton.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Impossible de se connecter.\r\nVérifier l'adresse IP de votre PS3.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (API.ConnectTarget())
                {
                    ConnexionLabel.Content = "Connecté";
                    ConnexionLabel.Foreground = Brushes.DarkGreen;
                    API.Notify(NotifyIcon.INFO, "Connecte au RTM Tool Bo2");
                    API.RingBuzzer(BuzzerMode.Single);
                    GetTemperature();
                    AttachButton.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Impossible de se connecter."+ Environment.NewLine +"Vérifier l'adresse IP de votre PS3.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            if (API.AttachProcess())
            {
                AttachLabel.Content = "Attaché";
                AttachLabel.Foreground = Brushes.DarkGreen;
                API.Notify(NotifyIcon.INFO, "Attache au RTM Tool Bo 2");
                API.RingBuzzer(BuzzerMode.Double);
                UnlockTool(true);
            }
            else
            {
                MessageBox.Show("Impossible de s'attaché au jeu.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CIDButton_Click(object sender, RoutedEventArgs e)
        {
            if(CIDTextbox.Text.Length == 32)
            {
                API.SetConsoleId(CIDTextbox.Text);
                API.Notify(NotifyIcon.INFO, "Console ID change avec succes !");
            }
            else
            {
                MessageBox.Show("Un Console ID est de 32 caractères.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshTempCheck_Checked(object sender, RoutedEventArgs e)
        {
            if(RefreshTempCheck.IsChecked == true)
            {
                TempTimer.Start();
            }
            else
            {
                TempTimer.Stop();
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoUAV(ToggleUAV.IsChecked == true);
        }

        private void ToggleVSAT_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoVSAT(ToggleVSAT.IsChecked == true);
        }

        private void ChangePseudoButton_Click(object sender, RoutedEventArgs e)
        {
            Function.ChangeUsername(PseudoTextBox.Text);
        }

        private void ToggleRedBoxes_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoRedBoxes(ToggleRedBoxes.IsChecked == true);
        }

        private void ToggleLazer_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoLazer(ToggleLazer.IsChecked == true);
        }

        private void ToggleBigNames_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoBigNames(ToggleBigNames.IsChecked == true);
        }

        private void ToggleNoRecoil_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoNoRecoil(ToggleNoRecoil.IsChecked == true);
        }

        private void ToggleWallhack_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoWallHack(ToggleWallhack.IsChecked == true);
        }

        private void ToggleSteadyAim_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoSteadyAim(ToggleSteadyAim.IsChecked == true);
        }

        private void ToggleTargetFinder_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoTargetFinder(ToggleTargetFinder.IsChecked == true);
        }

        private void ToggleFloatingBodies_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoFloatingBodies(ToggleFloatingBodies.IsChecked == true);
        }

        private void ToggleForceHost_Checked(object sender, RoutedEventArgs e)
        {
            Function.DoForceHost(ToggleForceHost.IsChecked == true);
        }

        private void ChangeClanTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClanTagTextBox.Text.Length > 0 && ClanTagTextBox.Text.Length < 5)
            {
                Function.ChangeClanTag(ClanTagTextBox.Text);
            }
            else
            {
                MessageBox.Show("Le Nom de Clan ne peut être supérieur à 4 caractères.");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrestigeComboBox.SelectedIndex > -1)
            {
                Function.ChangePrestige(PrestigeComboBox.SelectedIndex);
            }
        }

        private void LegitStatButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoLegitStats();
        }

        private void LowStatButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoLowStats();
        }

        private void MediumStatButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoMediumStats();
        }

        private void HighStatButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoHighStats();
        }

        private void LevelButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoLevel55();
        }

        private void PreOrderBonusButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoPreOrderBonuses();
        }

        private void UnlockAllButton_Click(object sender, RoutedEventArgs e)
        {
            Function.DoUnlockAll();
        }

        private void TokenButton_Click(object sender, RoutedEventArgs e)
        {
            Function.Do255Token();
        }

        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            Function.EditScore(Convert.ToInt32(ScoreNumericUpDown.Value));
        }

        private void WinsButton_Click(object sender, RoutedEventArgs e)
        {
            Function.EditWins(Convert.ToInt32(WinsNumericUpDown.Value));
        }

        private void LossesButton_Click(object sender, RoutedEventArgs e)
        {
            Function.EditLosses(Convert.ToInt32(LossesNumericUpDown.Value));
        }

        private void KillsButton_Click(object sender, RoutedEventArgs e)
        {
            Function.EditKills(Convert.ToInt32(KillsNumericUpDown.Value));
        }

        private void DeathsButton_Click(object sender, RoutedEventArgs e)
        {
            Function.EditDeaths(Convert.ToInt32(DeathsNumericUpDown.Value));
        }

        private void APIComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(APIComboBox.SelectedIndex > -1)
            {
                if (APIS != null)
                {
                    SelectAPI select = APIS[APIComboBox.SelectedIndex].SelectAPI;
                    API.ChangeAPI(select);
                    Function = new(API);
                    PrestigeIcons = Function.GetPresitgeIcons();
                    ConnexionButton.IsEnabled = true;
                }
            }
        }
    }
}
