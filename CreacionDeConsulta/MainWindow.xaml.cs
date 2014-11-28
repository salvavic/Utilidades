using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace CreacionDeConsulta
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            int i = 0;
            string sql = "select top 1 * from " +  txbNombreTabla.Text; 

            string nombreTablaDestino = txbNombreTablaDestino.Text; 

            command.Connection = new SqlConnection("Server=" + txbServidor.Text + ";Database=" + txbBaseDatos.Text + ";Trusted_Connection=True;");
            command.CommandText = sql;
            command.Connection.Open();
            string resultado = "";
            using (var reader = command.ExecuteReader())
            {
                var table = reader.GetSchemaTable();
                foreach (DataRow myField in table.Rows)
                {
                    string nombreCampo = "[" + myField[0].ToString() + "]";
                    string resultadoCampo = myField[0].ToString();
                    resultadoCampo = resultadoCampo.Replace(" ", "_");
                    resultadoCampo = resultadoCampo.Replace("-", "_");
                    resultadoCampo = resultadoCampo.Replace("(", "");
                    resultadoCampo = resultadoCampo.Replace(")", "");

                    if (resultado == "")
                    {
                        resultado = txbNombreTabla.Text + "." + nombreCampo + " as " + nombreTablaDestino + "_" + resultadoCampo;
                    }
                    else
                        resultado = resultado + ", " + txbNombreTabla.Text + "." + nombreCampo + " as " + nombreTablaDestino + "_" + resultadoCampo;
                }
            }

            command.Connection.Close();

            txbResultadoSql.Text = "SELECT " + resultado + " from " + txbNombreTabla.Text;

        }
    }
}
