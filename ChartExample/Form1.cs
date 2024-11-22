using System.Windows.Forms.DataVisualization.Charting;

namespace ChartExample
{
    public partial class Form1 : Form
    {
        Chart chart;
        Series series;

        Random random = new();

        public Form1()
        {
            InitializeComponent();
            InitChart();
            Task.Run(() => {

                while (true) { 
                
                    UpdateChart();
                    Thread.Sleep(1000);
                
                }
            
            });
        }

        List<int> list = new List<int>();

        
        private void UpdateChart()
        {
            int chartSize = 5;
            list.Add(random.Next(1, 100));
            var skip = list.Count - chartSize;
            if (skip > 0)
            {
                list.RemoveRange(0, skip);
            }

            for (int i = 0; i < chartSize; i++)
            {
                series.Points[i].YValues[0] = list[i];
            }

            //chart.Invoke(() =>
            //{
            //    int chartSize = 5;
            //    for (int i = 0; i < chartSize; i++)
            //    {
            //        series.Points[i].YValues[0] = list1[i];
            //    }
            //});

            

            //series.Points.AddXY(i++, random.Next(1, 100));





            //series.Points.Clear();
            //var i = 0;
            //foreach (var item in list)
            //{
            //    series.Points.AddXY(i++, item);
            //}

            //chart.Invoke(() =>
            //{
            //    series.Points.Clear();
            //    var i = 0;
            //    foreach (var item in list)
            //    {
            //        series.Points.AddXY(i++, item);
            //    }
            //});



            //if (series.Points.Count>3)
            //{
            //    series.Points.Remove(series.Points.First());
            //}
            //series.Points.AddXY(series.Points.Count + 1, random.Next(1, 100));

            // Добавляем данные в серию
            //series.Points.AddXY(0, 1);
            //series.Points.AddXY(1, 2);
            //series.Points.AddXY(2, 3);
            //series.Points.AddXY(3, 4);
            //series.Points.AddXY(4, 5);
            //series.Points.AddXY(5, 6);

        }


        private void InitChart()
        {
            // Создаем объект Chart
            chart = new Chart();
            chart.Dock = DockStyle.Fill;

            // Добавляем область графика
            ChartArea chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);

            // Добавляем серию данных
            series = new Series("DataSeries");
            series.ChartType = SeriesChartType.Line;
            chart.Series.Add(series);

            // Добавляем данные в серию
            series.Points.AddXY(0, 1);
            series.Points.AddXY(1, 2);
            series.Points.AddXY(2, 3);
            series.Points.AddXY(3, 4);
            series.Points.AddXY(4, 5);
            //series.Points.AddXY(1, 10);
            //series.Points.AddXY(2, 20);
            //series.Points.AddXY(3, 30);
            //series.Points.AddXY(4, 25);
            //series.Points.AddXY(5, 15);

            // Настройка внешнего вида графика
            series.Color = System.Drawing.Color.Blue;
            series.BorderWidth = 2;

            // Добавляем легенду
            //Legend legend = new Legend();
            //chart.Legends.Add(legend);

            // Добавляем график на форму
            this.Controls.Add(chart);
        }
    }
}
