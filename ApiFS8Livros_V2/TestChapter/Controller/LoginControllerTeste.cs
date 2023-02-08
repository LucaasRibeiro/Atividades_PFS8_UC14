using Chapter.Controllers;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestChapter.Controller
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Arange
            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dadoslogin = new LoginViewModel();

            dadoslogin.Email = "email@email.com";
            dadoslogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            var resultado = controller.Login(dadoslogin);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);

        }

        [Fact]
        public void LoginController_Retornar_Token()
        {
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "email@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "0";
            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);
            string issuerValidacao = "chapter.webapi";
            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "batata";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosLogin);

            string token = resultado.Value.ToString().Split("")[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(token);

            //Assert
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
        
    }
}
