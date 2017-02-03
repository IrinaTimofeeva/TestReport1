using System;
using System.Windows.Forms;
using Stimulsoft.Report;
using System.IO;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Json;

namespace TestReport1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            if (File.Exists("..\\..\\testReport.mrt"))
            {
                report.Load("..\\..\\testReport.mrt");
                string json = "[ {'date': '3 февраля 2017', 'weather' : { 'morning' : -7, 'day': -7, 'night' : -5 } }," +
                               " {'date': '4 февраля 2017', 'weather' : { 'morning': -5, 'day': -4, 'night': -6 }}," +
                               " {'date': '5 февраля 2017', 'weather' : { 'morning': -7, 'day': -8, 'night': -14 }}," +
                               " {'date': '6 февраля 2017', 'weather' : { 'morning' : -17, 'day' : -18, 'night' : -23 }}, " +
                               " {'date' : '7 февраля 2017','weather' : { 'morning' : -20, 'day' : -16, 'night' : -14}}," +
                               " {'date' : '8 февраля 2017', 'weather' : { 'morning' : -13, 'day' : -9, 'night' : -6}}," +
                               " {'date' : '9 февраля 2017', 'weather' : { 'morning' : -5, 'day': -3, 'night': -3 }}]";
                dynamic weather = JsonConvert.DeserializeObject(json);
                int step = 7;
                string name = "";
                for (int i = 0; i < 7; i++)
                {
                    name = "Table1_Cell" + step.ToString();
                    (report.GetComponentByName(name) as StiText).Text = weather[i].date;
                    step += 9;
                }
                step = 2;
                string[] dayArray = { "Утро", "День", "Ночь" };
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        name = "Table1_Cell" + step.ToString();
                        (report.GetComponentByName(name) as StiText).Text = dayArray[j];
                        step += 3;
                    }
                }
                step = 3;
                for (int i = 0; i < 7; i++)
                {
                    name = "Table1_Cell" + step.ToString();
                    (report.GetComponentByName(name) as StiText).Text = weather[i].weather.morning.ToString();
                    step += 3;
                    name = "Table1_Cell" + step.ToString();
                    (report.GetComponentByName(name) as StiText).Text = weather[i].weather.day.ToString();
                    step += 3;
                    name = "Table1_Cell" + step.ToString();
                    (report.GetComponentByName(name) as StiText).Text = weather[i].weather.night.ToString();
                    step += 3;
                }
            }
            (report.GetComponentByName("Text1") as StiText).Text = DateTime.Today.ToString();
            (report.GetComponentByName("Text2") as StiText).Text = "Тестовый отчет";
            (report.GetComponentByName("Text3") as StiText).Text = "Автор: Андреева Ирина";
            report.Compile();
            report.Show();
        }
    }
}