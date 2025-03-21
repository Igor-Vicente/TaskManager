using FluentAssertions;
using TaskManager.Domain.Abstractions;

namespace TaskManager.Domain.Tests.Abstractions
{
    public class NotificadorTests
    {
        [Fact(DisplayName = "Adicionar uma notificação")]
        [Trait("Domain", "Abstractions")]
        public void AdicionarNotificacao_DeveEstarNaLista_QuandoAdicinarUmaNotificacao()
        {
            //arrange
            var notificador = new Notificador();

            //act
            notificador.AdicionarNotificacao("N3");

            //assert
            notificador.ObterNotificacoes().Should().HaveCount(1).And.BeEquivalentTo("N3");
        }

        [Fact(DisplayName = "Adicionar muitas notificações")]
        [Trait("Domain", "Abstractions")]
        public void AdicionarNotificacao_DevemEstarNaLista_QuandoAdicionarMuitasNotificacoes()
        {
            //arrange
            var notificador = new Notificador();
            var notificacoes = new List<string>()
            {
                "N1",
                "N2"
            };

            //act
            notificador.AdicionarNotificacao(notificacoes);

            //assert
            notificador.ObterNotificacoes().Should().HaveCount(2);
            notificador.ObterNotificacoes().Should().Contain("N1");
            notificador.ObterNotificacoes().Should().Contain("N2");
        }


        [Fact(DisplayName = "Existe notificação")]
        [Trait("Domain", "Abstractions")]
        public void TemNotificacoes_DeveRetornarTrue_QuandoExisteNotificacao()
        {
            //arrange
            var notificador = new Notificador();
            notificador.AdicionarNotificacao("N5");

            // act && assert
            notificador.TemNotificacoes().Should().BeTrue();
        }
    }
}
