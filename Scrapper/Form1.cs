using Dapper.Contrib.Extensions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrapper
{

    public partial class Form1 : Form
    {
        HtmlWeb _web = new HtmlWeb();
        public List<ProductoFalabellaDto> ProductosDs { get; set; } = new List<ProductoFalabellaDto>();

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)) return;

                string folderPath = textBox1.Text;
                string[] htmlFiles = Directory.GetFiles(folderPath, "*.html");

                List<string> htmlFileList = htmlFiles.ToList();

                foreach (var htmlFile in htmlFileList)
                {
                    var doc = _web.Load(htmlFile);
                    var nodes = doc.DocumentNode.SelectNodes("//div[@pod-layout='4_GRID']");
                    foreach (var item in nodes)
                    {
                        var f = new ProductoFalabellaDto();

                        var titleTag = item.SelectSingleNode("a/div[2]/div[1]/b");
                        var marcaTag = item.SelectSingleNode("a/div[2]/div[1]/div/b");
                        var vendedorTag = item.SelectSingleNode("a/div[2]/div[1]/span/b");
                        var precioTag = item.SelectSingleNode("a/div[3]/div[1]/ol/li[1]/div/span");
                        var imgTag = item.SelectSingleNode("a/div[1]/div[1]/section/picture[1]/img");

                        f.Nombre = titleTag != null ? titleTag.InnerText : null;
                        f.Marca = marcaTag != null ? marcaTag.InnerText : null;
                        f.Vendedor = vendedorTag != null ? vendedorTag.InnerText : null;
                        f.UrlFoto = imgTag != null ? imgTag.Attributes["id"].Value : string.Empty;
                        f.UrlFoto = f.UrlFoto.Replace("testId-pod-image-", "");
                                                
                        if (precioTag != null)
                        {
                            var rr = precioTag.InnerText.Replace("S/", "");
                            f.Precio = precioTag != null ? decimal.TryParse(rr, out var r) ? r : 0 : 0;
                        }

                        ProductosDs.Add(f);
                    }
                }

                if (ProductosDs.Any())
                {
                    ProductosDs = ProductosDs.GroupBy(g => g.Nombre).Select(s => s.FirstOrDefault()).ToList();

                    var strjson = JsonConvert.SerializeObject(ProductosDs);
                    var ruta = Path.Combine(textBox1.Text, "resultado.json");
                    File.WriteAllText(ruta, strjson);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var cnx = new SqlConnection(textBox2.Text);
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();

                var ruta = Path.Combine(textBox1.Text, "resultado.json");
                var content = File.ReadAllText(ruta);
                var rr = new List<ProductoFalabellaDto>();
                rr = JsonConvert.DeserializeObject<List<ProductoFalabellaDto>>(content);

                var contador = 1;
                foreach (var f in rr)
                {
                    var ent = f.ToEntity();
                    ent.codigoproducto = $"P{contador:0000}";                    
                    contador++;
                    cnx.Insert(ent);
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
