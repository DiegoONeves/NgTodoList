using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NgTodoList.Domain.Tests
{
    [TestClass]
    public class Dado_um_novo_usuário
    {
        [TestMethod]
        [TestCategory("User - Novo Usuário")]
        [ExpectedException(typeof(Exception))]
        public void O_nome_deve_ser_válido()
        {
            var user = new User("", "diegoneves@hotmail.com", "123");

        }
        [TestMethod]
        [TestCategory("User - Novo Usuário")]
        [ExpectedException(typeof(Exception))]
        public void O_email_não_deve_ser_vazio()
        {
            var user = new User("Diego Neves", "", "123");

        }

        [TestMethod]
        [TestCategory("User - Novo Usuário")]
        [ExpectedException(typeof(Exception))]
        public void O_email_deve_ser_válido()
        {
            var user = new User("Diego Neves", "teste", "123");

        }

        [TestMethod]
        [TestCategory("User - Novo Usuário")]
        [ExpectedException(typeof(Exception))]
        public void A_senha_deve_ser_válida()
        {
            var user = new User("Diego Neves", "diegoneves@hotmail.com", "123");

        }

        [TestMethod]
        [TestCategory("User - Novo Usuário")]
        public void O_usuário_é_válido()
        {
            var user = new User("Diego Neves", "diegoneves@hotmail.com", "123456");

            Assert.AreNotEqual(null, user);

        }
    }

    [TestClass]
    public class Ao_Alterar_A_Senha
    {
        private User user = new User("Diego Neves", "diegoneves1989@gmail.com", "123456");
        [TestMethod]
        [TestCategory("User - Alterar Senha")]
        [ExpectedException(typeof(Exception))]
        public void O_email_deve_ser_válido()
        {

            user.ChangePassword("", "123456", "123456", "123456");
 
        }

        [TestMethod]
        [TestCategory("User - Alterar Senha")]
        [ExpectedException(typeof(Exception))]
        public void A_nova_senha_deve_ser_válida()
        {
            user.ChangePassword("diegoneves1989@gmail.com", "123", "123456", "12345656");
        }

        [TestMethod]
        [TestCategory("User - Alterar Senha")]
        [ExpectedException(typeof(Exception))]
        public void Usuário_e_senha_devem_ser_válidos()
        {
            user.ChangePassword("diegoneves1989@gmail.com", "123", "123456", "12345656");
        }

        [TestMethod]
        [TestCategory("User - Alterar Senha")]
        [ExpectedException(typeof(Exception))]
        public void A_confirmação_de_senha_deve_ser_igual_a_nova_senha()
        {
            user.ChangePassword("diegoneves1989@gmail.com", "123", "1234560", "123456561");
        }

        [TestMethod]
        [TestCategory("User - Alterar Senha")]
        public void A_senha_deve_ser_encriptada()
        {
            var password = "minhasenhasegura";
            user.ChangePassword("diegoneves1989@gmail.com", "123456", password, password);

            Assert.AreNotEqual(password, user.Password);
        }

    }
}
