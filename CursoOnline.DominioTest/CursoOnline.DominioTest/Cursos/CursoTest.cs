using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            // Arrange
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950,
            };

            // Act
            var curso = new Curso(
                cursoEsperado.Nome,
                cursoEsperado.CargaHoraria,
                cursoEsperado.PublicoAlvo,
                cursoEsperado.Valor);

            // Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string value)
        {
            // Arrange
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950,
            };

            // Act


            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                new Curso(
                    value,
                    cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor);
            });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        [InlineData(300)]
        public void NaoDeveCursoTerUmaCargaHorariaInvalida(double value)
        {
            // Arrange
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = value,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950,
            };

            // Assert


            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                new Curso(
                    cursoEsperado.Nome,
                    cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor);
            });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorInvalido(double value)
        {
            // Arrange
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = value,
            };

            // Assert


            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                new Curso(
                    cursoEsperado.Nome,
                    cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor);
            });
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    internal class Curso
    {
        public Curso(
            string nome,
            double cargaHoraria,
            PublicoAlvo publicoAlvo,
            double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome e obrigatorio");
            }

            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horario de um curso nao pode ser menor do que 1h");
            }

            if (cargaHoraria >= 300)
            {
                throw new ArgumentException("Carga horario de um curso nao pode ser maior do que 300h");
            }

            if (valor <= 0)
            {
                throw new ArgumentException("O valor de um curso nao pode ser menor do que 0");
            }

            this.Nome = nome;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
