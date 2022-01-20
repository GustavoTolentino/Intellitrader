using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Newtonsoft.Json;
using ProjetoGustavo.Controllers.Domains;
using ProjetoGustavo.Controllers.Service;
using ProjetoGustavo.Models;
using Xunit;

namespace TestProjectGustavo
{
    public class UsuarioServiceTeste
    {
        [Theory]
        [InlineData("00d8a9df-d492-42c0-90da-789999c97deb", "xxxx")]
        public void findById(Guid id, String expectedName)
        {
            Moq.Mock<IUsuarioService> mock = new Moq.Mock<IUsuarioService>(MockBehavior.Strict);
            mock.Setup(x => x.getById(It.IsAny<Guid>())).Returns(new UsuarioResponse
            {
                Id = new Guid("00d8a9df-d492-42c0-90da-789999c97deb"),
                FirstName = "xxxx",
                Surname = "xxxxxx",
                Age = 30,
                CreationDate = DateTime.Now
            });

            var okResult = mock.Object.getById(id);

            Assert.Equal(((UsuarioResponse)okResult).FirstName, expectedName);
        }

        [Fact]
        public void GetAll()
        {
            Moq.Mock<IUsuarioService> mock = new Moq.Mock<IUsuarioService>(MockBehavior.Strict);
            mock.Setup(x => x.get()).Returns(getListUsuarioResponse());

            var okResult = mock.Object.get();

            Assert.Equal(((List<UsuarioResponse>)okResult).Count, 3);
        }

        public List<UsuarioResponse> getListUsuarioResponse()
        {
            List<UsuarioResponse> lstUsuarios = new List<UsuarioResponse>();
           
            lstUsuarios.Add(new UsuarioResponse
            {
                Id = new Guid("c65359ef-8d34-459e-832e-f53d741cc0de"),
                FirstName = "Pedro",
                Surname = "Costa",
                Age = 30,
                CreationDate = DateTime.Now
            });

            lstUsuarios.Add(new UsuarioResponse
            {
                Id = new Guid("c6d7900f-7528-45e7-b418-d574030d3de8"),
                FirstName = "Paulo",
                Surname = "Silva",
                Age = 30,
                CreationDate = DateTime.Now
            });

            lstUsuarios.Add(new UsuarioResponse
            {
                Id = new Guid("b09d8a43-7964-4597-9f90-2a1e66a0323d"),
                FirstName = "João",
                Surname = "Leão",
                Age = 30,
                CreationDate = DateTime.Now
            });

            return lstUsuarios;
        }
    }
}
