using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Flody

            Stopwatch sh = new Stopwatch();
            sh.Start();
            Flody fl = new Flody();
            fl.Main();
            TimeSpan tsFlody = sh.Elapsed;
            listBox1.Items.Add("Flody             : " + tsFlody.TotalMilliseconds + "ms");
            sh.Reset();
            sh.Start();
            chart1.Series["Süre"].Points.AddXY("Flody", tsFlody.TotalMilliseconds);



            //Dijkstra

            Stopwatch st = new Stopwatch();
            st.Start();
            Dijkstra dj = new Dijkstra();
            dj.Main();
            TimeSpan tsDijkstra = st.Elapsed;
            listBox1.Items.Add("Dijkstra          : " + tsDijkstra.TotalMilliseconds + "ms");
            st.Reset();

            st.Start();

            chart1.Series["Süre"].Points.AddXY("Dijkstra", tsDijkstra.TotalMilliseconds);
            
            

            //Kruskal
            Stopwatch spw = new Stopwatch();
            spw.Start();
            KruskalAlgorithm kr = new KruskalAlgorithm();
            kr.Main();
            TimeSpan tsKruskalAlgorithm = spw.Elapsed;
            listBox1.Items.Add("Kruskal          : " + tsKruskalAlgorithm.TotalMilliseconds + "ms");
            spw.Reset();
            spw.Start();
            chart1.Series["Süre"].Points.AddXY("Kruskal", tsKruskalAlgorithm.TotalMilliseconds);

            
            //FloydWarshall

            Stopwatch wh = new Stopwatch();
            wh.Start();
            FloydWarshallAlgo fw = new FloydWarshallAlgo();
            fw.Main();
            TimeSpan tsFloydWarshallAlgo = wh.Elapsed;
            listBox1.Items.Add("FloydWarshall: " + tsFloydWarshallAlgo.TotalMilliseconds + "ms");
            wh.Reset();
            wh.Start();
            chart1.Series["Süre"].Points.AddXY("FloydWarshall", tsFloydWarshallAlgo.TotalMilliseconds);
            
            //BFS

            Stopwatch wt = new Stopwatch();
            wt.Start();
            BFS bs = new BFS();
            bs.Main();
            TimeSpan tsBFS = wh.Elapsed;
            listBox1.Items.Add("BreadthFirst  : " + tsBFS.TotalMilliseconds + "ms");
            wt.Reset();
            wt.Start();
            chart1.Series["Süre"].Points.AddXY("BFS", tsBFS.TotalMilliseconds);
            
            //DFS

            Stopwatch sp = new Stopwatch();
            sp.Start();
            DFS ds = new DFS();
            ds.Main();
            TimeSpan tsDFS = wh.Elapsed;
            listBox1.Items.Add("DepthFirst     : " + tsDFS.TotalMilliseconds + "ms");
            sp.Reset();
            sp.Start();
            chart1.Series["Süre"].Points.AddXY("DFS", tsDFS.TotalMilliseconds);

            chart1.Series["Süre"].Color = Color.Purple;

            //PriorityQueue

            Stopwatch so = new Stopwatch();
            so.Start();
            PriorityQueue pq = new PriorityQueue();
            pq.heapSort();
            TimeSpan tsPriorityQueue = so.Elapsed;
            listBox1.Items.Add("PriorityQueue: " + tsPriorityQueue.TotalMilliseconds + "ms");
            so.Reset();
            so.Start();
            chart1.Series["Süre"].Points.AddXY("PriorityQueue", tsPriorityQueue.TotalMilliseconds);


            

            //BellmanFord
            Stopwatch sw = new Stopwatch();
            sw.Start();
            BellmanFord bf = new BellmanFord();
            bf.Main();
            TimeSpan tsBellmanFord = sw.Elapsed;
            listBox1.Items.Add("Bellman Ford: " + tsBellmanFord.TotalMilliseconds + " ms");
            sw.Reset();

            sw.Start();


            chart1.Series["Süre"].Points.AddXY("Bellman Ford", tsBellmanFord.TotalMilliseconds);
            chart1.Series["Süre"].Color = Color.DarkMagenta;

        }
    }
}
