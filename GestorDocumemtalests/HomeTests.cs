using System;
using System.Threading.Tasks;
using Xunit;
using GestorDocumental.Components.Pages;
using GestorDocumental.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GestorDocumental.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Radzen;
using Microsoft.JSInterop;
using BootstrapBlazor.Components;
using DialogService = Radzen.DialogService;
using GestorDocumental.Business.Services;


namespace GestorDocumental.Tests.Components
{

    public class HomeTests
    {
        private class FakeNavigationManager : Microsoft.AspNetCore.Components.NavigationManager
        {
            public FakeNavigationManager() => Initialize("http://localhost/", "http://localhost/");
            protected override void NavigateToCore(string uri, bool forceLoad) => Uri = ToAbsoluteUri(uri).ToString();
        }

        [Fact]
        public void ToggleVista_InitialTrue_TogglesToFalse()
        {
            // Arrange
            var home = new Home();
            bool initial = home.modoTarjetas;

            // Act
            home.ToggleVista();
            bool after = home.modoTarjetas;

            // Assert
            Assert.True(initial);
            Assert.False(after);
        }

        [Fact]
        public void ToggleSidebar_InitialFalse_TogglesToTrue()
        {
            // Arrange
            var home = new Home();
            bool initial = home.sidebarExpanded;

            // Act
            home.ToggleSidebar();
            bool after = home.sidebarExpanded;

            // Assert
            Assert.False(initial);
            Assert.True(after);
        }

        [Fact]
        public void VerNoVisibles_InitialFalse_TogglesToTrue()
        {
            // Arrange
            var home = new Home();
            bool initial = home.mostrarNoVisibles;

            // Act
            home.VerNoVisibles();
            bool after = home.mostrarNoVisibles;

            // Assert
            Assert.False(initial);
            Assert.True(after);
        }

        [Theory]
        [InlineData("application/pdf", "icons/pdf.png")]
        [InlineData("text/plain", "icons/plain.png")]
        [InlineData("application/msword", "icons/word.png")]
        [InlineData("application/vnd.openxmlformats-officedocument.wordprocessingml.document", "icons/word.png")]
        [InlineData("image/jpeg", "icons/imagen.png")]
        [InlineData("application/zip", "icons/archivo.png")]
        public void ObtenerIconoArchivo_ReturnsExpectedIcon(string mimeType, string expected)
        {
            // Arrange
            var home = new Home();

            // Act
            var icon = home.ObtenerIconoArchivo(mimeType);

            // Assert
            Assert.Equal(expected, icon);
        }

        [Fact]
        public void ObtenerIconoCarpeta_ReturnsCarpetaIcon()
        {
            // Arrange
            var home = new Home();

            // Act
            var icon = home.ObtenerIconoCarpeta();

            // Assert
            Assert.Equal("icons/carpeta.png", icon);
        }

    }

    public class CrearGrupoDialogTests
    {
        private class FakeGrupoService
        {
            public int CalledCourse { get; private set; }
            public List<Grupos> AvailableGrupos { get; set; } = new List<Grupos>();
            public Grupos AddedGrupo { get; private set; }

            public Task<List<Grupos>> ObtenerGruposPorCurso(int codigoCurso)
            {
                CalledCourse = codigoCurso;
                return Task.FromResult(AvailableGrupos);
            }

            public Task AgregarGrupo(Grupos grupo)
            {
                AddedGrupo = grupo;
                return Task.CompletedTask;
            }
        }

        private class FakeCursoService
        {
            public bool CursosUsuarioGrupoCalled { get; private set; }
            public bool UsuarioGrupoCalled { get; private set; }
            public Curso ReturnedCurso { get; set; } = new Curso { CodigoCurso = 1 };

            public Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso)
            {
                return Task.FromResult(ReturnedCurso);
            }

            public Task AgregarRelacionCursosUsuarioGrupo(int codigoCurso, string grupo)
            {
                CursosUsuarioGrupoCalled = true;
                return Task.CompletedTask;
            }

            public Task AgregarRelacionUsuarioGrupo(int codigoUsuario, string grupo)
            {
                UsuarioGrupoCalled = true;
                return Task.CompletedTask;
            }
        }
    }

}
