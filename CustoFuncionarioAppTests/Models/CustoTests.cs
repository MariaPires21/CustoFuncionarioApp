using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustoFuncionarioApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustoFuncionarioApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Drawing;

namespace CustoFuncionarioApp.Models.Tests
{
    [TestClass()]
    public class CustoTests
    {
        private Custo _custo;

        [TestInitialize]
        public void Inicializar()
        {
            _custo = new Custo();
        }

        [TestMethod()]
        [DataRow(0.0, 0.0)]
        [DataRow(1410.00, 7.5)]
        [DataRow(1412.00, 7.5)]
        [DataRow(1737.27, 9.0)]
        [DataRow(3800.00, 12.0)]
        [DataRow(6995.75, 14.0)]
        [DataRow(10995.50, 0.0)]
        public void DeveRetornar_INSS_AliquotaTest(double valor, double esperado)
        {
            _custo.SalarioBruto = (decimal)valor;

            // ACT
            var obtido = _custo.getINSS_Aliquota();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido);
        }

        [TestMethod()]
        [DataRow(0.0, 0.0)]
        [DataRow(1410.00, 105.75)]
        [DataRow(1412.00, 105.90)]
        [DataRow(1737.27, 156.35)]
        [DataRow(3800.00, 456)]
        [DataRow(6995.75, 979.41)]
        [DataRow(10995.50, 0.0)]
        public void DeveRetornar_getINSS_ValorTest(double valor, double esperado)
        {
            _custo.SalarioBruto = (decimal)valor;

            var obtido = _custo.getINSS_Valor();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(0.0, 0.0)]
        [DataRow(1410.00, 112.8)]
        [DataRow(1412.00, 112.96)]
        [DataRow(1737.27, 138.98)]
        [DataRow(3800.00, 304.0)]
        [DataRow(6995.75, 559.66)]
        [DataRow(10995.50, 879.64)]
        public void getFGTSTest(double valor, double esperado)
        {
            _custo.SalarioBruto = (decimal)valor;

            var obtido = _custo.getFGTS();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(0.0, 0.0)]
        [DataRow(1410.00, 1410.00)]
        [DataRow(1412.00, 1412.00)]
        [DataRow(1737.27, 1737.27)]
        [DataRow(3800.00, 3800.00)]
        [DataRow(6995.75, 6995.75)]
        [DataRow(10995.50, 10995.50)]
        public void get13oSalarioTest(double valor, double esperado)
        {
            _custo.SalarioBruto = (decimal)valor;

            var obtido = _custo.get13oSalario();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido);
        }

        [TestMethod()]
        [DataRow(0.0, 0.0)]
        [DataRow(1410.00, 1880.00)]
        [DataRow(1412.00, 1882.66)]
        [DataRow(1737.27, 2316.36)]
        [DataRow(3800.00, 5066.66)]
        [DataRow(6995.75, 9327.66)]
        [DataRow(10995.50, 14660.66)]
        public void getFeriasTest(double valor, double esperado)
        {
            _custo.SalarioBruto = (decimal)valor;

            var obtido = _custo.getFerias();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido, 2);
        }

        [TestMethod()]
        [DataRow(1410.00, 105.75, 2)]
        [DataRow(1412.00, 105.90, 2)]
        [DataRow(1737.27, 156.35, 2.42)]
        [DataRow(3800.00, 456.0, 3.30)]
        [DataRow(6995.75, 979.41, 3.88)]
        [DataRow(10995.50, 0.0, 0.0)]
        public void getPercentualDespesaTest(double salarioBruto, double valorDespesa, double esperado)
        {
            decimal CustoTotal = _custo.getCustoTotal();
            _custo.SalarioBruto = (decimal)salarioBruto;

            var obtido = _custo.getPercentualDespesa((decimal)valorDespesa);
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido, 2);
        }


        [TestMethod()]
        [DataRow(1410.00, 124.45, 45.87, 50, 5138.87)]
        [DataRow(1412.00,100.50 , 75.20, 200.54, 5301.77)]
        public void getCustoTotalTest(double valorSalario, double valorPlano, double valorSeguro, double valorOutro, double esperado)
        {
            _custo.SalarioBruto = (decimal)valorSalario;
            decimal valorInss = _custo.getINSS_Valor();
            decimal valorFgts = _custo.getFGTS();
            decimal valor13o = _custo.get13oSalario();
            _custo.PlanoSaude = (decimal)valorPlano;
            _custo.SeguroVida = (decimal)valorSeguro;
            _custo.OutrosBeneficios = (decimal)valorOutro;

            var obtido = _custo.getCustoTotal();
            var Esperado = (decimal)esperado;

            // ASSERT 
            Assert.AreEqual(Esperado, obtido, 2);
        }
    }
}