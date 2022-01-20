using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoGustavo.Controllers;
using ProjetoGustavo.Controllers.Domains;
using ProjetoGustavo.Controllers.Service;
using ProjetoGustavo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProjectGustavo
{
    public class UsuarioControllerTeste
    {
        private UsuarioController _controller;

        public UsuarioControllerTeste()
        {
            _controller = new UsuarioController();
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get();
            Assert.IsType<List<UsuarioResponse>>(okResult);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.Get();
            Assert.NotNull(okResult);
        }

        // GETBYID
        [Theory]
        // GUID Valido
        [InlineData("737188E0-D55F-486A-9F8C-747E6713927F", "Gustavo")]
        // GUID invalido
        [InlineData("e7143a13-217f-490d-9115-e9fb2199ba3a", null)]
        public void GetById_WhenCalled_ReturnsSomething(Guid id, String expectedId)
        {
            var okResult = _controller.Get(id);
            Assert.Equal(((UsuarioResponse)okResult).FirstName, expectedId);
        }

        [Theory]
        // GUID Valido
        [InlineData("34DA4C62-95F0-43D2-945D-4710D8E6F1A1", "Pedro")]
        public void updateById_WhenCalled_UpdateRecordAndRevert(Guid id, String NameExpected)
        {
            var expected = new UsuarioRequest { FirstName = "Pedro", Surname = "Teste", Age = 20 };

            _controller.Put(id, expected);
            var okResult = _controller.Get(id);

            Assert.Equal(((UsuarioResponse)okResult).FirstName, NameExpected);

            var revert = new UsuarioRequest { FirstName = "Matheus", Surname = "da Silva", Age = 19 };
            _controller.Put(id, revert);
        }
        [Theory]
        [MemberData(nameof(UserList))]
        public void postDelete_WhenCalled_InsertANewUserAndAfterDelete(string firstName, string surname, int age)
        {
            UsuarioRequest user = new UsuarioRequest { FirstName = firstName, Surname = surname, Age = age };
            Guid id = _controller.Post(user);
            Assert.NotEqual(id, new Guid("00000000-0000-0000-0000-000000000000"));
            _controller.Delete(id);
            UsuarioResponse responseUser = _controller.Get(id);
            Assert.Equal(new Guid("00000000-0000-0000-0000-000000000000"), responseUser.Id);
        }
        public static IEnumerable<object[]> UserList =>
        new List<object[]>
        {
            new object[] { "Nome", "Sobrenome", 33 },
            new object[] { "Nome2", "Sobrenome2", 34 }
        };
    }
}
